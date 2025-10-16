import { useMemo, useState } from "react";
import {
    Box,
    Chip,
    CircularProgress,
    FormControl,
    InputLabel,
    MenuItem,
    Paper,
    Select,
    Stack,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    TextField,
    Tooltip,
    Typography,
    IconButton,
} from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";

const STATUS_LABELS = {
    Completed: "Administré",
    Scheduled: "Planifié",
    Overdue: "En retard",
};

const STATUS_COLORS = {
    Completed: "success",
    Scheduled: "info",
    Overdue: "warning",
};

function formatDate(value) {
    if (!value) {
        return "—";
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
                            <MenuItem value="Completed">Administré</MenuItem>
                            <MenuItem value="Scheduled">Planifié</MenuItem>
                            <MenuItem value="Overdue">En retard</MenuItem>
                        </Select>
                    </FormControl>
                    <TextField
                        label="Rechercher (enfant, vaccin ou âge)"
                        value={search}
                        onChange={(event) => setSearch(event.target.value)}
                        fullWidth
                    />
                </Stack>
                <TableContainer>
                    <Table size="small">
                        <TableHead>
                            <TableRow>
                                <TableCell>Enfant</TableCell>
                                <TableCell>Vaccin</TableCell>
                                <TableCell>Âge (mois)</TableCell>
                                <TableCell>Statut</TableCell>
                                <TableCell>Date planifiée</TableCell>
                                <TableCell>Date d'administration</TableCell>
                                <TableCell>Commentaires</TableCell>
                                <TableCell align="right">Actions</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {loading ? (
                                <TableRow>
                                    <TableCell colSpan={8} align="center">
                                        <CircularProgress size={24} />
                                    </TableCell>
                                </TableRow>
                            ) : filteredEntries.length === 0 ? (
                                <TableRow>
                                    <TableCell colSpan={8} align="center">
                                        <Typography variant="body2">Aucun vaccin ne correspond à vos filtres.</Typography>
                                    </TableCell>
                                </TableRow>
                            ) : (
                                filteredEntries.map((entry) => (
                                    <TableRow key={`${entry.childId}-${entry.vaccineId}`} hover>
                                        <TableCell>{entry.childName}</TableCell>
                                        <TableCell>{entry.vaccineName}</TableCell>
                                        <TableCell>{entry.ageInMonths}</TableCell>
                                        <TableCell>
                                            <Chip
                                                label={getStatusLabel(entry.status)}
                                                color={STATUS_COLORS[entry.status]}
                                                variant="outlined"
                                                size="small"
                                            />
                                        </TableCell>
                                        <TableCell>{formatDate(entry.scheduledDate)}</TableCell>
                                        <TableCell>{formatDate(entry.administrationDate)}</TableCell>
                                        <TableCell>{entry.comments || "—"}</TableCell>
                                        <TableCell align="right">
                                            <Tooltip title="Mettre à jour le statut">
                                                <span>
                                                    <IconButton
                                                        onClick={() => onEditEntry(entry)}
                                                        size="small"
                                                        color="primary"
                                                    >
                                                        <EditIcon fontSize="small" />
                                                    </IconButton>
                                                </span>
                                            </Tooltip>
                                        </TableCell>
                                    </TableRow>
                                ))
                            )}
                        </TableBody>
                    </Table>
                </TableContainer>
            </Box>
        </Paper>
    );
}