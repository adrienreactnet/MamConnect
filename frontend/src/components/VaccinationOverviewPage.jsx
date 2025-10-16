import { useCallback, useEffect, useMemo, useState } from "react";
import {
    Alert,
    Box,
    Button,
    CircularProgress,
    Grid,
    Paper,
    Snackbar,
    Stack,
    Typography,
} from "@mui/material";
import RefreshIcon from "@mui/icons-material/Refresh";
import VaccinationStatusTable from "./VaccinationStatusTable";
import VaccinationStatusDialog from "./VaccinationStatusDialog";
import { fetchChildren } from "../services/childService";
import {
    getChildVaccinationSchedule,
    getVaccinationOverview,
    updateChildVaccine,
} from "../services/vaccinationsService";

function normalizeSchedule(schedule) {
    return {
        childId: schedule.childId,
        firstName: schedule.firstName,
        birthDate: schedule.birthDate,
        vaccines: schedule.vaccines.map((entry) => ({
            vaccineId: entry.vaccineId,
            vaccineName: entry.vaccineName,
            ageInMonths: entry.ageInMonths,
            status: entry.status,
            scheduledDate: entry.scheduledDate ?? null,
            administrationDate: entry.administrationDate ?? null,
            comments: entry.comments ?? "",
        })),
    };
}

export default function VaccinationOverviewPage() {
    const [schedules, setSchedules] = useState([]);
    const [overview, setOverview] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const [dialogOpen, setDialogOpen] = useState(false);
    const [selectedEntry, setSelectedEntry] = useState(null);
    const [saving, setSaving] = useState(false);
    const [snackbar, setSnackbar] = useState({ open: false, message: "", severity: "success" });

    const loadAllData = useCallback(async () => {
        setLoading(true);
        try {
            const [children, overviewData] = await Promise.all([
                fetchChildren(),
                getVaccinationOverview(),
            ]);

            const schedulesResponses = await Promise.all(
                children.map((child) => getChildVaccinationSchedule(child.id)),
            );

            const normalizedSchedules = schedulesResponses
                .map(normalizeSchedule)
                .sort((a, b) => a.firstName.localeCompare(b.firstName, "fr"));

            setSchedules(normalizedSchedules);
            setOverview(overviewData);
            setError("");
        } catch (err) {
            setError(err.message ?? "Impossible de charger les données vaccinales.");
        } finally {
            setLoading(false);
        }
    }, []);

    useEffect(() => {
        loadAllData();
    }, [loadAllData]);

    const entries = useMemo(() => {
        return schedules.flatMap((schedule) =>
            schedule.vaccines.map((entry) => ({
                childId: schedule.childId,
                childName: schedule.firstName,
                birthDate: schedule.birthDate,
                vaccineId: entry.vaccineId,
                vaccineName: entry.vaccineName,
                ageInMonths: entry.ageInMonths,
                vaccineDisplayName: `${entry.vaccineName} (${entry.ageInMonths} mois)`,
                status: entry.status,
                scheduledDate: entry.scheduledDate,
                administrationDate: entry.administrationDate,
                comments: entry.comments,
            })),
        );
    }, [schedules]);

    const handleEditEntry = (entry) => {
        setSelectedEntry(entry);
        setDialogOpen(true);
    };

    const handleDialogClose = () => {
        if (saving) {
            return;
        }

        setDialogOpen(false);
        setSelectedEntry(null);
    };

    const handleUpdate = async (payload) => {
        if (!selectedEntry) {
            return;
        }

        setSaving(true);

        try {
            const updatedSchedule = await updateChildVaccine(
                selectedEntry.childId,
                selectedEntry.vaccineId,
                payload,
            );

            const normalized = normalizeSchedule(updatedSchedule);
            setSchedules((current) => {
                const others = current.filter((schedule) => schedule.childId !== normalized.childId);
                return [...others, normalized].sort((a, b) => a.firstName.localeCompare(b.firstName, "fr"));
            });

            const overviewData = await getVaccinationOverview();
            setOverview(overviewData);

            setSnackbar({
                open: true,
                message: "Statut vaccinal mis à jour avec succès.",
                severity: "success",
            });
            setDialogOpen(false);
            setSelectedEntry(null);
        } catch (err) {
            setSnackbar({
                open: true,
                message: err.message ?? "La mise à jour a échoué.",
                severity: "error",
            });
        } finally {
            setSaving(false);
        }
    };

    const handleSnackbarClose = () => {
        setSnackbar((state) => ({ ...state, open: false }));
    };

    return (
        <Box mt={4}>
            <Stack direction="row" alignItems="center" justifyContent="space-between" mb={3}>
                <Typography variant="h5">Aperçu des vaccinations</Typography>
                <Button
                    startIcon={<RefreshIcon />}
                    onClick={loadAllData}
                    disabled={loading || saving}
                >
                    Actualiser
                </Button>
            </Stack>

            {error && (
                <Alert severity="error" sx={{ mb: 3 }}>
                    {error}
                </Alert>
            )}

            {loading ? (
                <Box display="flex" justifyContent="center" mt={6}>
                    <CircularProgress />
                </Box>
            ) : (
                <>
                    {overview && (
                        <Grid container spacing={2} mb={3}>
                            <Grid item xs={12} sm={6} md={4}>
                                <Paper sx={{ p: 2 }}>
                                    <Typography variant="subtitle2" color="text.secondary">
                                        Enfants suivis
                                    </Typography>
                                    <Typography variant="h4">{overview.totalChildren}</Typography>
                                </Paper>
                            </Grid>
                            <Grid item xs={12} sm={6} md={4}>
                                <Paper sx={{ p: 2 }}>
                                    <Typography variant="subtitle2" color="text.secondary">
                                        Vaccins total
                                    </Typography>
                                    <Typography variant="h4">{overview.totalVaccinations}</Typography>
                                </Paper>
                            </Grid>
                            <Grid item xs={12} sm={6} md={4}>
                                <Paper sx={{ p: 2 }}>
                                    <Typography variant="subtitle2" color="text.secondary">
                                        Vaccins administrés
                                    </Typography>
                                    <Typography variant="h4">{overview.completedVaccinations}</Typography>
                                </Paper>
                            </Grid>
                            <Grid item xs={12} sm={6} md={4}>
                                <Paper sx={{ p: 2 }}>
                                    <Typography variant="subtitle2" color="text.secondary">
                                        Vaccins planifiés
                                    </Typography>
                                    <Typography variant="h4">{overview.scheduledVaccinations}</Typography>
                                </Paper>
                            </Grid>
                            <Grid item xs={12} sm={6} md={4}>
                                <Paper sx={{ p: 2 }}>
                                    <Typography variant="subtitle2" color="text.secondary">
                                        Vaccins en retard
                                    </Typography>
                                    <Typography variant="h4">{overview.overdueVaccinations}</Typography>
                                </Paper>
                            </Grid>
                            <Grid item xs={12} sm={6} md={4}>
                                <Paper sx={{ p: 2 }}>
                                    <Typography variant="subtitle2" color="text.secondary">
                                        Enfants concernés par un retard
                                    </Typography>
                                    <Typography variant="h4">{overview.childrenWithOverdueVaccinations}</Typography>
                                </Paper>
                            </Grid>
                        </Grid>
                    )}

                    <VaccinationStatusTable
                        entries={entries}
                        loading={loading}
                        onEditEntry={handleEditEntry}
                    />
                </>
            )}

            <VaccinationStatusDialog
                open={dialogOpen}
                entry={selectedEntry}
                onClose={handleDialogClose}
                onSave={handleUpdate}
                isSaving={saving}
            />

            <Snackbar
                open={snackbar.open}
                autoHideDuration={4000}
                onClose={handleSnackbarClose}
                anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
            >
                <Alert onClose={handleSnackbarClose} severity={snackbar.severity} sx={{ width: "100%" }}>
                    {snackbar.message}
                </Alert>
            </Snackbar>
        </Box>
    );
}