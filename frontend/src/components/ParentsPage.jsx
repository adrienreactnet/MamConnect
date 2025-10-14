// src/components/ParentsPage.jsx
import React, { useEffect, useMemo, useState } from "react";
import IconButton from "@mui/material/IconButton";
import Dialog from "@mui/material/Dialog";
import DialogContent from "@mui/material/DialogContent";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import Typography from "@mui/material/Typography";
import { Add } from "@mui/icons-material";
import AddParent from "./AddParent";
import { fetchParents } from "../services/parentService";
import { fetchChildren } from "../services/childService";

export default function ParentsPage() {
    const [parents, setParents] = useState([]);
    const [children, setChildren] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const [open, setOpen] = useState(false);

    const load = async () => {
        setLoading(true);
        try {
            const [p, c] = await Promise.all([fetchParents(), fetchChildren()]);
            setParents(p);
            setChildren(c);
            setError("");
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        load();
    }, []);

    const parentRows = useMemo(() => {
        const rows = [];

        children
            .slice()
            .sort((a, b) => a.firstName.localeCompare(b.firstName))
            .forEach((child) => {
                const relatedParents = parents
                    .filter((parent) => parent.childrenIds?.includes(child.id))
                    .slice()
                    .sort((a, b) => {
                        const lastNameComparison = a.lastName.localeCompare(b.lastName);
                        if (lastNameComparison !== 0) {
                            return lastNameComparison;
                        }
                        return a.firstName.localeCompare(b.firstName);
                    });

                if (relatedParents.length === 0) {
                    rows.push({
                        key: `child-${child.id}-no-parent`,
                        childName: child.firstName,
                        parentName: "—",
                        phoneNumber: "—",
                        email: "—",
                    });
                    return;
                }

                relatedParents.forEach((parent) => {
                    rows.push({
                        key: `child-${child.id}-parent-${parent.id}`,
                        childName: child.firstName,
                        parentName: `${parent.lastName} ${parent.firstName}`,
                        phoneNumber: parent.phoneNumber || "—",
                        email: parent.email || "—",
                    });
                });
            });

        return rows;
    }, [children, parents]);

    return (
        <div>
            <Typography variant="h4" component="h2" gutterBottom>
                Liste des parents
            </Typography>
            <IconButton aria-label="add" onClick={() => setOpen(true)}>
                <Add />
            </IconButton>
            {!loading && parentRows.length === 0 && <p>Aucun parent trouvé.</p>}
            {loading && <p>Chargement...</p>}
            {!loading && parentRows.length > 0 && (
                <TableContainer component={Paper}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Enfant</TableCell>
                                <TableCell>Parent</TableCell>
                                <TableCell>Téléphone</TableCell>
                                <TableCell>Email</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {parentRows.map((row) => (
                                <TableRow key={row.key}>
                                    <TableCell>{row.childName}</TableCell>
                                    <TableCell>{row.parentName}</TableCell>
                                    <TableCell>{row.phoneNumber}</TableCell>
                                    <TableCell>{row.email}</TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            )}
            {error && <p style={{ color: "red" }}>{error}</p>}

            <Dialog open={open} onClose={() => setOpen(false)}>
                <DialogContent>
                    <AddParent
                        onParentAdded={() => {
                            load();
                            setOpen(false);
                        }}
                    />
                </DialogContent>
            </Dialog>
        </div>
    );
}
