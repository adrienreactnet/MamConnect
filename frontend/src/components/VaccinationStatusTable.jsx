import { useMemo, useState } from "react";
import {
    Box,
    Chip,
    CircularProgress,
    FormControl,
    IconButton,
    InputLabel,
    MenuItem,
    Paper,
    Select,
    Stack,
    TextField,
    Tooltip,
} from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import DataTable from "./DataTable";

const STATUS_LABELS = {
    Completed: "Administre",
    Pending: "En attente",
    ToSchedule: "A planifier",
    Overdue: "En retard",
};

const STATUS_COLORS = {
    Completed: "success",
    Pending: "info",
    ToSchedule: "warning",
    Overdue: "error",
};

function formatDate(value) {
    if (!value) {
        return "-";
    }

    try {
        return new Date(value).toLocaleDateString("fr-FR");
    } catch {
        return value;
    }
}

function getStatusLabel(status) {
    return STATUS_LABELS[status] ?? status;
}

export default function VaccinationStatusTable({ entries, loading, onEditEntry }) {
    const [statusFilter, setStatusFilter] = useState("all");
    const [search, setSearch] = useState("");

    const filteredEntries = useMemo(() => {
        return entries
            .filter((entry) => {
                if (statusFilter === "all") {
                    return true;
                }
                return entry.status === statusFilter;
            })
            .filter((entry) => {
                if (!search) {
                    return true;
                }

                const query = search.toLowerCase();
                return (
                    entry.childName.toLowerCase().includes(query) ||
                    entry.vaccineDisplayName.toLowerCase().includes(query)
                );
            })
            .sort((a, b) => {
                const ageCompare = a.ageInMonths - b.ageInMonths;
                if (ageCompare !== 0) {
                    return ageCompare;
                }

                const childCompare = a.childName.localeCompare(b.childName, "fr");
                if (childCompare !== 0) {
                    return childCompare;
                }

                return a.vaccineName.localeCompare(b.vaccineName, "fr");
            });
    }, [entries, search, statusFilter]);

    const columns = useMemo(
        () => [
            {
                id: "childName",
                label: "Enfant",
            },
            {
                id: "vaccineName",
                label: "Vaccin",
            },
            {
                id: "ageInMonths",
                label: "Age (mois)",
            },
            {
                id: "status",
                label: "Statut",
                render: (entry) => (
                    <Chip
                        label={getStatusLabel(entry.status)}
                        color={STATUS_COLORS[entry.status] ?? "default"}
                        variant="outlined"
                        size="small"
                    />
                ),
            },
            {
                id: "scheduledDate",
                label: "Date preconisee",
                render: (entry) => formatDate(entry.scheduledDate),
            },
            {
                id: "administrationDate",
                label: "Date d'administration",
                render: (entry) => formatDate(entry.administrationDate),
            },
            {
                id: "comments",
                label: "Commentaires",
                render: (entry) => entry.comments || "-",
            },
            {
                id: "actions",
                label: "Actions",
                align: "right",
                render: (entry) => (
                    <Tooltip title="Mettre a jour le statut">
                        <span>
                            <IconButton onClick={() => onEditEntry(entry)} size="small" color="primary">
                                <EditIcon fontSize="small" />
                            </IconButton>
                        </span>
                    </Tooltip>
                ),
            },
        ],
        [onEditEntry],
    );

    return (
        <Paper>
            <Box p={2}>
                <Stack direction={{ xs: "column", md: "row" }} spacing={2} mb={2}>
                    <FormControl sx={{ minWidth: 200 }}>
                        <InputLabel id="vaccination-status-filter-label">Statut</InputLabel>
                        <Select
                            labelId="vaccination-status-filter-label"
                            label="Statut"
                            value={statusFilter}
                            onChange={(event) => setStatusFilter(event.target.value)}
                        >
                            <MenuItem value="all">Tous les statuts</MenuItem>
                            <MenuItem value="Completed">Administre</MenuItem>
                            <MenuItem value="Pending">En attente</MenuItem>
                            <MenuItem value="ToSchedule">A planifier</MenuItem>
                            <MenuItem value="Overdue">En retard</MenuItem>
                        </Select>
                    </FormControl>
                    <TextField
                        label="Rechercher (enfant, vaccin ou age)"
                        value={search}
                        onChange={(event) => setSearch(event.target.value)}
                        fullWidth
                    />
                </Stack>

                {loading ? (
                    <Box display="flex" justifyContent="center" py={4}>
                        <CircularProgress size={24} />
                    </Box>
                ) : (
                    <DataTable
                        columns={columns}
                        rows={filteredEntries}
                        getRowId={(entry) => `${entry.childId}-${entry.vaccineId}`}
                        emptyMessage="Aucun vaccin ne correspond a vos filtres."
                    />
                )}
            </Box>
        </Paper>
    );
}
