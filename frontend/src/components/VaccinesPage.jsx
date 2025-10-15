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
    agesInMonths: "",
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
            agesInMonths: vaccine.agesInMonths,
        });
        setFormError("");
        setDialogOpen(true);
    };

    const handleSubmit = async () => {
        const trimmedName = formValues.name.trim();
        const trimmedAges = formValues.agesInMonths.trim();

        if (trimmedName.length === 0 || trimmedAges.length === 0) {
            setFormError("Le nom et les âges en mois sont obligatoires");
            return;
        }

        try {
            if (editingId === null) {
                await createVaccine({
                    name: trimmedName,
                    agesInMonths: trimmedAges,
                });
            } else {
                await updateVaccine(editingId, {
                    id: editingId,
                    name: trimmedName,
                    agesInMonths: trimmedAges,
                });
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
                            <TableCell>Âges (mois)</TableCell>
                            <TableCell align="right">Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {vaccines.map((vaccine) => (
                            <TableRow key={vaccine.id}>
                                <TableCell>{vaccine.name}</TableCell>
                                <TableCell>{vaccine.agesInMonths}</TableCell>
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
                            label="Âges en mois"
                            helperText="Séparer les valeurs par une virgule (ex : 2,4,11)"
                            value={formValues.agesInMonths}
                            onChange={(event) =>
                                setFormValues({ ...formValues, agesInMonths: event.target.value })
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