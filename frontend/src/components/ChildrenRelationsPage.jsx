// src/components/ChildrenRelationsPage.jsx
import React, { useCallback, useEffect, useState } from "react";
import {
    Alert,
    Box,
    Button,
    CircularProgress,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    Paper,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    TextField,
    Typography,
} from "@mui/material";
import { addChild, fetchChildrenWithRelations } from "../services/childService";

export default function ChildrenRelationsPage() {
    const [relations, setRelations] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const [isAddDialogOpen, setIsAddDialogOpen] = useState(false);
    const [firstName, setFirstName] = useState("");
    const [birthDate, setBirthDate] = useState("");
    const [addError, setAddError] = useState("");
    const [isSubmitting, setIsSubmitting] = useState(false);

    const loadRelations = useCallback(async () => {
        setLoading(true);
        try {
            const data = await fetchChildrenWithRelations();
            setRelations(data);
            setError("");
        } catch (err) {
            setError(err.message || "Erreur lors du chargement des données.");
        } finally {
            setLoading(false);
        }
    }, []);

    useEffect(() => {
        loadRelations();
    }, [loadRelations]);

    const handleDialogClose = () => {
        setIsAddDialogOpen(false);
        setFirstName("");
        setBirthDate("");
        setAddError("");
    };

    const handleAddChild = async (event) => {
        event.preventDefault();
        setAddError("");
        setIsSubmitting(true);

        try {
            await addChild({ firstName, birthDate });
            handleDialogClose();
            await loadRelations();
        } catch (err) {
            setAddError(err.message || "Erreur lors de l'ajout de l'enfant.");
        } finally {
            setIsSubmitting(false);
        }
    };

    if (loading) {
        return (
            <Box display="flex" justifyContent="center" alignItems="center" minHeight="200px">
                <CircularProgress />
            </Box>
        );
    }

    return (
        <Box>
            <Box display="flex" justifyContent="space-between" alignItems="center" mb={2}>
                <Typography variant="h5">Relations enfants</Typography>
                <Button variant="contained" onClick={() => setIsAddDialogOpen(true)}>
                    Ajouter un enfant
                </Button>
            </Box>

            {error && (
                <Alert severity="error" sx={{ mb: 2 }}>
                    {error}
                </Alert>
            )}

            {relations.length === 0 ? (
                <Typography>Aucune relation trouvée.</Typography>
            ) : (
                <TableContainer component={Paper}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Enfant</TableCell>
                                <TableCell>Assistante</TableCell>
                                <TableCell>Parent 1</TableCell>
                                <TableCell>Parent 2</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {relations.map((relation, index) => {
                                const parents = relation.parentNames || [];
                                const parent1 = parents[0] || "";
                                const parent2 = parents.length > 1 ? parents[1] : "";

                                return (
                                    <TableRow key={`${relation.childFirstName}-${index}`}>
                                        <TableCell>{relation.childFirstName}</TableCell>
                                        <TableCell>{relation.assistantName || ""}</TableCell>
                                        <TableCell>{parent1}</TableCell>
                                        <TableCell>{parent2}</TableCell>
                                    </TableRow>
                                );
                            })}
                        </TableBody>
                    </Table>
                </TableContainer>
            )}

            <Dialog open={isAddDialogOpen} onClose={handleDialogClose} fullWidth maxWidth="sm">
                <form onSubmit={handleAddChild} noValidate>
                    <DialogTitle>Ajouter un enfant</DialogTitle>
                    <DialogContent>
                        {addError && (
                            <Alert severity="error" sx={{ mb: 2 }}>
                                {addError}
                            </Alert>
                        )}
                        <TextField
                            autoFocus
                            margin="dense"
                            id="child-first-name"
                            label="Prénom"
                            type="text"
                            fullWidth
                            value={firstName}
                            onChange={(event) => setFirstName(event.target.value)}
                            required
                        />
                        <TextField
                            margin="dense"
                            id="child-birth-date"
                            label="Date de naissance"
                            type="date"
                            fullWidth
                            InputLabelProps={{ shrink: true }}
                            value={birthDate}
                            onChange={(event) => setBirthDate(event.target.value)}
                            required
                        />
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={handleDialogClose}>Annuler</Button>
                        <Button
                            type="submit"
                            variant="contained"
                            disabled={isSubmitting || firstName.trim() === "" || birthDate === ""}
                        >
                            {isSubmitting ? "Ajout..." : "Ajouter"}
                        </Button>
                    </DialogActions>
                </form>
            </Dialog>
        </Box>
    );
}