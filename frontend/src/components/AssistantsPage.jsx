// src/components/AssistantsPage.jsx
import React, { useEffect, useState } from "react";
import IconButton from "@mui/material/IconButton";
import Dialog from "@mui/material/Dialog";
import DialogContent from "@mui/material/DialogContent";
import { Edit, Delete, Check, Add } from "@mui/icons-material";
import AddAssistant from "./AddAssistant";
import {
    fetchAssistants,
    updateAssistant,
    deleteAssistant,
} from "../services/assistantService";

export default function AssistantsPage() {
    const [assistants, setAssistants] = useState([]);
    const [error, setError] = useState("");
    const [editingAssistant, setEditingAssistant] = useState(null);
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");
    const [email, setEmail] = useState("");
    const [open, setOpen] = useState(false);

    const loadAssistants = async () => {
        try {
            const data = await fetchAssistants();
            setAssistants(data);
            setError("");
        } catch (err) {
            setError(err.message);
        }
    };

    useEffect(() => {
        loadAssistants();
    }, []);

    const startEditing = (assistant) => {
        setEditingAssistant(assistant.id);
        setFirstName(assistant.firstName);
        setLastName(assistant.lastName);
        setPhoneNumber(assistant.phoneNumber || "");
        setEmail(assistant.email || "");
    };

    const handleUpdate = async (id) => {
        try {
            await updateAssistant(id, {
                id,
                firstName,
                lastName,
                phoneNumber,
                email,
            });
            await loadAssistants();
            setEditingAssistant(null);
            setFirstName("");
            setLastName("");
            setPhoneNumber("");
            setEmail("");
        } catch (err) {
            setError(err.message);
        }
    };

    const handleDelete = async (id) => {
        try {
            await deleteAssistant(id);
            await loadAssistants();
        } catch (err) {
            setError(err.message);
        }
    };

    return (
        <div>
            <h2>Liste des assistantes</h2>
            <IconButton aria-label="add" onClick={() => setOpen(true)}>
                <Add />
            </IconButton>
            {assistants.length === 0 && <p>Aucune assistante trouvée.</p>}
            <ul>
                {assistants.map((assistant) => (
                    <li key={assistant.id}>
                        {editingAssistant === assistant.id ? (
                            <>
                                <input
                                    type="text"
                                    value={firstName}
                                    onChange={(e) => setFirstName(e.target.value)}
                                />
                                <input
                                    type="text"
                                    value={lastName}
                                    onChange={(e) => setLastName(e.target.value)}
                                />
                                <input
                                    type="tel"
                                    value={phoneNumber}
                                    onChange={(e) => setPhoneNumber(e.target.value)}
                                />
                                <input
                                    type="email"
                                    value={email}
                                    onChange={(e) => setEmail(e.target.value)}
                                />
                                <IconButton
                                    aria-label="validate"
                                    onClick={() => handleUpdate(assistant.id)}
                                >
                                    <Check />
                                </IconButton>
                            </>
                        ) : (
                            <>
                                {assistant.firstName} {assistant.lastName}
                                <IconButton
                                    aria-label="edit"
                                    onClick={() => startEditing(assistant)}
                                >
                                    <Edit />
                                </IconButton>
                                <IconButton
                                    aria-label="delete"
                                    onClick={() => handleDelete(assistant.id)}
                                >
                                    <Delete />
                                </IconButton>
                            </>
                        )}
                    </li>
                ))}
            </ul>
            {error && <p style={{ color: "red" }}>{error}</p>}

            <Dialog open={open} onClose={() => setOpen(false)}>
                <DialogContent>
                    <AddAssistant
                        onAssistantAdded={() => {
                            loadAssistants();
                            setOpen(false);
                        }}
                    />
                </DialogContent>
            </Dialog>
        </div>
    );
}