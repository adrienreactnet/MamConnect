// src/components/ChildrenRelationsPage.jsx
import React, { useCallback, useEffect, useMemo, useState } from "react";
import {
    Alert,
    Box,
    Button,
    CircularProgress,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    TextField,
    Typography,
} from "@mui/material";
import { addChild, fetchChildrenWithRelations } from "../services/childService";
import DataTable from "./DataTable";

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

    const columns = useMemo(
        () => [
            { id: "child", label: "Enfant" },
            { id: "assistant", label: "Assistante" },
            { id: "parent1", label: "Parent 1" },
            { id: "parent2", label: "Parent 2" },
        ],
        []
    );

    const rows = useMemo(
        () =>
            relations.map((relation, index) => {
                const parents = relation.parentNames ?? [];
                return {
                    id: `${relation.childFirstName}-${index}`,
                    child: relation.childFirstName,
                    assistant: relation.assistantName ?? "",
                    parent1: parents[0] ?? "",
                    parent2: parents[1] ?? "",
                };
            }),
        [relations]
    );

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

            <DataTable columns={columns} rows={rows} emptyMessage="Aucune relation trouvée." />

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