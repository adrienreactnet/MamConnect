// src/components/ChildrenRelationsPage.jsx
import React, { useEffect, useState } from "react";
import {
    Alert,
    Box,
    CircularProgress,
    Paper,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Typography,
} from "@mui/material";
import { fetchChildrenWithRelations } from "../services/childService";

export default function ChildrenRelationsPage() {
    const [relations, setRelations] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        const loadRelations = async () => {
            try {
                const data = await fetchChildrenWithRelations();
                setRelations(data);
                setError("");
            } catch (err) {
                setError(err.message || "Erreur lors du chargement des données.");
            } finally {
                setLoading(false);
            }
        };

        loadRelations();
    }, []);

    if (loading) {
        return (
            <Box display="flex" justifyContent="center" alignItems="center" minHeight="200px">
                <CircularProgress />
            </Box>
        );
    }

    if (error) {
        return <Alert severity="error">{error}</Alert>;
    }

    if (relations.length === 0) {
        return <Typography>Aucune relation trouvée.</Typography>;
    }

    return (
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
    );
}
