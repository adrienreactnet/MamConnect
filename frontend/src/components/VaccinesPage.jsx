import { useEffect, useState } from "react";
import {
    Box,
    Button,
    CircularProgress,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    IconButton,
    Stack,
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableRow,
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
            setFormError("Le nom et l'âge en mois sont obligatoires.");
            return;
        }

        if (!Number.isInteger(parsedAge) || parsedAge < 0) {
            setFormError("L'âge en mois doit être un entier positif ou nul.");
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

            {loading ? (
                <Box display="flex" justifyContent="center" mt={4}>
                    <CircularProgress />
                </Box>
            ) : vaccines.length === 0 ? (
                <Typography>Aucun vaccin enregistré.</Typography>
            ) : (
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Nom du vaccin</TableCell>
                            <TableCell>Âge (mois)</TableCell>
                            <TableCell align="right">Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {vaccines.map((vaccine) => (
                            <TableRow key={vaccine.id}>
                                <TableCell>{vaccine.name}</TableCell>
                                <TableCell>{vaccine.ageInMonths}</TableCell>
                                <TableCell align="right">
                                    <IconButton aria-label="modifier" onClick={() => openEditDialog(vaccine)}>
                                        <Edit />
                                    </IconButton>
                                    <IconButton aria-label="supprimer" onClick={() => handleDelete(vaccine.id)}>
                                        <Delete />
                                    </IconButton>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            )}

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
                            label="Âge (mois)"
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