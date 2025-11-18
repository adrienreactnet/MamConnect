import { useEffect, useState } from "react";
import {
    Box,
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    IconButton,
    Stack,
    TextField,
    Typography,
} from "@mui/material";
import { Add, Delete, Edit } from "@mui/icons-material";
import {
    createVaccine,
    deleteVaccine,
    getVaccines,
    updateVaccine,
} from "../services/vaccinesService";
import DataTable from "./DataTable";

const INITIAL_FORM = {
    name: "",
    ageInMonths: "",
};

export default function VaccinesPage() {
    const [vaccines, setVaccines] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const [dialogOpen, setDialogOpen] = useState(false);
    const [formValues, setFormValues] = useState(INITIAL_FORM);
    const [formError, setFormError] = useState("");
    const [editingId, setEditingId] = useState(null);

    const loadVaccines = async () => {
        setLoading(true);
        try {
            const data = await getVaccines();
            setVaccines(data);
            setError("");
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        loadVaccines();
    }, []);

    const closeDialog = () => {
        setDialogOpen(false);
        setFormValues(INITIAL_FORM);
        setFormError("");
        setEditingId(null);
    };

    const openAddDialog = () => {
        setEditingId(null);
        setFormValues(INITIAL_FORM);
        setFormError("");
        setDialogOpen(true);
    };

    const openEditDialog = (vaccine) => {
        setEditingId(vaccine.id);
        setFormValues({
            name: vaccine.name,
            ageInMonths: String(vaccine.ageInMonths ?? ""),
        });
        setFormError("");
        setDialogOpen(true);
    };

    const handleSubmit = async () => {
        const trimmedName = formValues.name.trim();
        const ageInput = typeof formValues.ageInMonths === "string"
            ? formValues.ageInMonths.trim()
            : String(formValues.ageInMonths ?? "").trim();
        const parsedAge = Number(ageInput);

        if (trimmedName.length === 0 || ageInput.length === 0) {
            setFormError("Le nom et l'age en mois sont obligatoires.");
            return;
        }

        if (!Number.isInteger(parsedAge) || parsedAge < 0) {
            setFormError("L'age en mois doit etre un entier positif ou nul.");
            return;
        }

        try {
            const payload = {
                name: trimmedName,
                ageInMonths: parsedAge,
            };

            if (editingId === null) {
                await createVaccine(payload);
            } else {
                await updateVaccine(editingId, payload);
            }
            await loadVaccines();
            closeDialog();
        } catch (err) {
            setFormError(err.message);
        }
    };

    const handleDelete = async (id) => {
        if (!window.confirm("Supprimer ce vaccin ?")) {
            return;
        }

        try {
            await deleteVaccine(id);
            await loadVaccines();
        } catch (err) {
            setError(err.message);
        }
    };

    return (
        <Box mt={4}>
            <Stack direction="row" alignItems="center" justifyContent="space-between" mb={2}>
                <Typography variant="h5">Calendrier vaccinal</Typography>
                <Button variant="contained" startIcon={<Add />} onClick={openAddDialog}>
                    Ajouter un vaccin
                </Button>
            </Stack>

            {error && (
                <Typography color="error" mb={2}>
                    {error}
                </Typography>
            )}

            <DataTable
                columns={[
                    {
                        id: "name",
                        label: "Nom du vaccin",
                    },
                    {
                        id: "ageInMonths",
                        label: "Age (mois)",
                    },
                ]}
                rows={vaccines}
                getRowId={(vaccine) => vaccine.id}
                emptyMessage="Aucun vaccin enregistre."
                loading={loading}
                stickyHeader
                rowActions={(vaccine) => (
                    <>
                        <IconButton
                            aria-label="modifier"
                            onClick={() => openEditDialog(vaccine)}
                            size="small"
                        >
                            <Edit fontSize="small" />
                        </IconButton>
                        <IconButton aria-label="supprimer" onClick={() => handleDelete(vaccine.id)} size="small">
                            <Delete fontSize="small" />
                        </IconButton>
                    </>
                )}
            />

            <Dialog open={dialogOpen} onClose={closeDialog} fullWidth maxWidth="xs">
                <DialogTitle>{editingId === null ? "Ajouter un vaccin" : "Modifier un vaccin"}</DialogTitle>
                <DialogContent>
                    <Stack spacing={2} mt={1}>
                        <TextField
                            label="Nom du vaccin"
                            value={formValues.name}
                            onChange={(event) => setFormValues({ ...formValues, name: event.target.value })}
                            fullWidth
                        />
                        <TextField
                            label="Age (mois)"
                            type="number"
                            inputProps={{ min: 0, step: 1 }}
                            value={formValues.ageInMonths}
                            onChange={(event) =>
                                setFormValues({ ...formValues, ageInMonths: event.target.value })
                            }
                            fullWidth
                        />
                        {formError && (
                            <Typography color="error" variant="body2">
                                {formError}
                            </Typography>
                        )}
                    </Stack>
                </DialogContent>
                <DialogActions>
                    <Button onClick={closeDialog}>Annuler</Button>
                    <Button onClick={handleSubmit} variant="contained">
                        {editingId === null ? "Ajouter" : "Enregistrer"}
                    </Button>
                </DialogActions>
            </Dialog>
        </Box>
    );
}
