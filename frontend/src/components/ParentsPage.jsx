// src/components/ParentsPage.jsx
import React, { useEffect, useState } from "react";
import IconButton from "@mui/material/IconButton";
import Dialog from "@mui/material/Dialog";
import DialogContent from "@mui/material/DialogContent";
import { Add } from "@mui/icons-material";
import AddParent from "./AddParent";
import { fetchParents } from "../services/parentService";
import { fetchChildren } from "../services/childService";

export default function ParentsPage() {
    const [parents, setParents] = useState([]);
    const [children, setChildren] = useState([]);
    const [error, setError] = useState("");
    const [open, setOpen] = useState(false);

    const load = async () => {
        try {
            const [p, c] = await Promise.all([fetchParents(), fetchChildren()]);
            setParents(p);
            setChildren(c);
            setError("");
        } catch (err) {
            setError(err.message);
        }
    };

    useEffect(() => {
        load();
    }, []);

    const childName = (id) => children.find(c => c.id === id)?.firstName || "";

    return (
        <div>
            <h2>Liste des parents</h2>
            <IconButton aria-label="add" onClick={() => setOpen(true)}>
                <Add />
            </IconButton>
            {parents.length === 0 && <p>Aucun parent trouvé.</p>}
            <ul>
                {parents.map((parent) => (
                    <li key={parent.id}>
                        {parent.firstName} {parent.lastName}
                        {parent.childrenIds && parent.childrenIds.length > 0 && (
                            <ul>
                                {parent.childrenIds.map((id) => (
                                    <li key={id}>{childName(id)}</li>
                                ))}
                            </ul>
                        )}
                    </li>
                ))}
            </ul>
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