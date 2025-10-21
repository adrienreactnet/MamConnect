import { useEffect, useState } from "react";
import {
    Alert,
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogContentText,
    DialogTitle,
    Stack,
    TextField,
} from "@mui/material";

const STATUS_LABELS = {
    Completed: "Administré",
    Pending: "En attente",
    ToSchedule: "À planifier",
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

        setScheduledDate(formatDateForInput(entry?.scheduledDate));
        setAdministrationDate(formatDateForInput(entry?.administrationDate));
        setComments(entry?.comments ?? "");
        setError("");
    }, [entry, open]);

    const vaccineLabel = entry
        ? entry.vaccineDisplayName ??
        `${entry.vaccineName}${entry.ageInMonths !== undefined && entry.ageInMonths !== null
            ? ` (${entry.ageInMonths} mois)`
            : ""
        }`
        : "";

    const currentStatusLabel = entry ? STATUS_LABELS[entry.status] ?? entry.status : "";

    const validate = () => {
        const today = todayIso();

        if (administrationDate && administrationDate > today) {
            return "La date d'administration ne peut pas être dans le futur.";
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
            scheduledDate: scheduledDate || null,
            administrationDate: administrationDate || null,
            comments: comments.trim() ? comments.trim() : null,
        };

        if (payload.administrationDate) {
            payload.status = "Completed";
        }

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
                    {entry && (
                        <Alert severity="info" variant="outlined">
                            Statut actuel&nbsp;: <strong>{currentStatusLabel}</strong>
                        </Alert>
                    )}
                    <TextField
                        label="Date préconisée"
                        type="date"
                        value={scheduledDate}
                        onChange={(event) => setScheduledDate(event.target.value)}
                        InputLabelProps={{ shrink: true }}
                        helperText="Optionnel. Utilisé pour le calcul automatique du statut."
                        disabled={isSaving}
                    />
                    <TextField
                        label="Date d'administration"
                        type="date"
                        value={administrationDate}
                        onChange={(event) => setAdministrationDate(event.target.value)}
                        InputLabelProps={{ shrink: true }}
                        helperText="Renseignez cette date pour marquer le vaccin comme administré. Les dates antérieures à la préconisation sont acceptées."
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