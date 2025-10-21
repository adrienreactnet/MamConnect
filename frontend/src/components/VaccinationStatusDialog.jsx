import { useEffect, useMemo, useState } from "react";
import {
    Alert,
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogContentText,
    DialogTitle,
    FormControl,
    InputLabel,
    MenuItem,
    Select,
    Stack,
    TextField,
} from "@mui/material";

const STATUS_LABELS = {
    Completed: "Administré",
    Scheduled: "Planifié",
    Overdue: "En retard",
};

function formatDateForInput(date) {
    if (!date) {
        return "";
    }

    return date;
}

function todayIso() {
    return new Date().toISOString().slice(0, 10);
}

export default function VaccinationStatusDialog({
    open,
    entry,
    onClose,
    onSave,
    isSaving = false,
}) {
    const [status, setStatus] = useState(entry?.status ?? "Scheduled");
    const [scheduledDate, setScheduledDate] = useState(formatDateForInput(entry?.scheduledDate));
    const [administrationDate, setAdministrationDate] = useState(
        formatDateForInput(entry?.administrationDate),
    );
    const [comments, setComments] = useState(entry?.comments ?? "");
    const [error, setError] = useState("");

    useEffect(() => {
        if (!open) {
            return;
        }

        setStatus(entry?.status ?? "Scheduled");
        setScheduledDate(formatDateForInput(entry?.scheduledDate));
        setAdministrationDate(formatDateForInput(entry?.administrationDate));
        setComments(entry?.comments ?? "");
        setError("");
    }, [entry, open]);

    const statusOptions = useMemo(
        () => [
            { value: "Completed", label: STATUS_LABELS.Completed },
            { value: "Scheduled", label: STATUS_LABELS.Scheduled },
            { value: "Overdue", label: STATUS_LABELS.Overdue },
        ],
        [],
    );
    const vaccineLabel = entry
        ? entry.vaccineDisplayName ??
        `${entry.vaccineName}${entry.ageInMonths !== undefined && entry.ageInMonths !== null
            ? ` (${entry.ageInMonths} mois)`
            : ""
        }`
        : "";

    const validate = () => {
        if (!status) {
            return "Veuillez sélectionner un statut.";
        }

        const today = todayIso();

        if (status === "Completed") {
            if (!administrationDate) {
                return "La date d'administration est obligatoire pour un vaccin administré.";
            }

            if (administrationDate > today) {
                return "La date d'administration ne peut pas être dans le futur.";
            }

            if (scheduledDate && administrationDate < scheduledDate) {
                return "La date d'administration doit être postérieure ou égale à la date prévue.";
            }
        }

        if (status === "Scheduled") {
            if (!scheduledDate) {
                return "Veuillez indiquer une date de préconisation.";
            }

            if (scheduledDate < today) {
                return "La date préconisée doit être aujourd'hui ou plus tard.";
            }

            if (administrationDate) {
                return "Un vaccin préconisé ne peut pas avoir de date d'administration.";
            }
        }

        if (status === "Overdue") {
            if (!scheduledDate) {
                return "Veuillez indiquer la date de préconisation initiale.";
            }

            if (scheduledDate >= today) {
                return "Un vaccin en retard doit avoir une date préconisée passée.";
            }

            if (administrationDate) {
                return "Un vaccin en retard ne peut pas avoir de date d'administration.";
            }
        }

        if (comments.length > 512) {
            return "Les commentaires doivent contenir 512 caractères maximum.";
        }

        return "";
    };

    const handleSave = () => {
        const validationError = validate();
        if (validationError) {
            setError(validationError);
            return;
        }

        const payload = {
            status,
            scheduledDate: scheduledDate || null,
            administrationDate: administrationDate || null,
            comments: comments.trim() ? comments.trim() : null,
        };

        onSave(payload);
    };

    return (
        <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
            <DialogTitle>Mettre à jour le statut vaccinal</DialogTitle>
            <DialogContent>
                <Stack spacing={2} mt={1}>
                    <DialogContentText>
                        {entry ? `${entry.childName} — ${vaccineLabel}` : ""}
                    </DialogContentText>
                    <FormControl fullWidth>
                        <InputLabel id="vaccination-status-label">Statut</InputLabel>
                        <Select
                            labelId="vaccination-status-label"
                            label="Statut"
                            value={status}
                            onChange={(event) => setStatus(event.target.value)}
                            disabled={isSaving}
                        >
                            {statusOptions.map((option) => (
                                <MenuItem key={option.value} value={option.value}>
                                    {option.label}
                                </MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                    <TextField
                       label="Date préconisée"
                        type="date"
                        value={scheduledDate}
                        onChange={(event) => setScheduledDate(event.target.value)}
                        InputLabelProps={{ shrink: true }}
                        disabled={isSaving}
                    />
                    <TextField
                        label="Date d'administration"
                        type="date"
                        value={administrationDate}
                        onChange={(event) => setAdministrationDate(event.target.value)}
                        InputLabelProps={{ shrink: true }}
                        disabled={isSaving}
                    />
                    <TextField
                        label="Commentaires"
                        value={comments}
                        onChange={(event) => setComments(event.target.value)}
                        multiline
                        minRows={3}
                        disabled={isSaving}
                    />
                    {error && <Alert severity="error">{error}</Alert>}
                </Stack>
            </DialogContent>
            <DialogActions>
                <Button onClick={onClose} disabled={isSaving}>
                    Annuler
                </Button>
                <Button onClick={handleSave} variant="contained" disabled={isSaving}>
                    {isSaving ? "Enregistrement..." : "Enregistrer"}
                </Button>
            </DialogActions>
        </Dialog>
    );
}