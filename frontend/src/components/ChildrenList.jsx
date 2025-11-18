// src/components/ChildrenList.jsx
import React, { useEffect, useState } from "react";
import IconButton from "@mui/material/IconButton";
import Dialog from "@mui/material/Dialog";
import DialogContent from "@mui/material/DialogContent";
import { Edit, Delete, Check, Add } from "@mui/icons-material";
import AddChild from "./AddChild";
import { fetchChildren, updateChild, deleteChild } from "../services/childService";
import DataTable from "./DataTable";

const todayIsoDate = new Date().toISOString().split("T")[0];

export default function ChildrenList() {
    const [children, setChildren] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const [editingChild, setEditingChild] = useState(null);
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [birthDate, setBirthDate] = useState("");
    const [birthDateError, setBirthDateError] = useState("");
    const [open, setOpen] = useState(false);
    const [allergies, setAllergies] = useState("");

    const loadChildren = async () => {
        setLoading(true);
        try {
            const data = await fetchChildren();
            setChildren(data);
            setBirthDateError("");
            setError("");
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        loadChildren();
    }, []);

    const startEditing = (child) => {
        setEditingChild(child.id);
        setFirstName(child.firstName ?? "");
        setLastName(child.lastName ?? "");
        setBirthDate(child.birthDate || "");
        setBirthDateError("");
        setError("");
        setAllergies(child.allergies ?? "");
    };

    const resetEditingState = () => {
        setEditingChild(null);
        setFirstName("");
        setLastName("");
        setBirthDate("");
        setBirthDateError("");
        setError("");
        setAllergies("");
    };

    const handleUpdate = async (id) => {
        const trimmedFirstName = firstName.trim();
        const trimmedLastName = lastName.trim();
        const trimmedAllergies = allergies.trim();
        if (trimmedFirstName.length === 0) {
            setError("Le prenom est requis.");
            return;
        }

        if (trimmedLastName.length === 0) {
            setError("Le nom est requis.");
            return;
        }

        if (birthDate === "") {
            setBirthDateError("La date de naissance doit etre renseignee.");
            return;
        }

        if (birthDate > todayIsoDate) {
            setBirthDateError("La date de naissance ne peut pas depasser la date du jour.");
            return;
        }

        try {
            const existingChild = children.find((child) => child.id === id);
            const assistantId = existingChild ? existingChild.assistantId ?? null : null;
            await updateChild(id, {
                firstName: trimmedFirstName,
                lastName: trimmedLastName,
                birthDate,
                assistantId,
                allergies: trimmedAllergies.length > 0 ? trimmedAllergies : null,
            });
            await loadChildren();
            resetEditingState();
        } catch (err) {
            setError(err.message);
        }
    };

    const handleDelete = async (id) => {
        try {
            await deleteChild(id);
            await loadChildren();
        } catch (err) {
            setError(err.message);
        }
    };

    const handleBirthDateChange = (value) => {
        if (value !== "" && value > todayIsoDate) {
            setBirthDateError("La date de naissance ne peut pas depasser la date du jour.");
            return;
        }
        setBirthDateError("");
        setBirthDate(value);
    };

    return (
        <div>
            <h2>Liste des enfants</h2>
            <IconButton aria-label="add" onClick={() => setOpen(true)}>
                <Add />
            </IconButton>
            <DataTable
                columns={[
                    {
                        id: "firstName",
                        label: "Prenom",
                        render: (child) =>
                            editingChild === child.id ? (
                                <input
                                    type="text"
                                    value={firstName}
                                    onChange={(event) => setFirstName(event.target.value)}
                                />
                            ) : (
                                child.firstName
                            ),
                    },
                    {
                        id: "lastName",
                        label: "Nom",
                        render: (child) =>
                            editingChild === child.id ? (
                                <input
                                    type="text"
                                    value={lastName}
                                    onChange={(event) => setLastName(event.target.value)}
                                />
                            ) : (
                                child.lastName
                            ),
                    },
                    {
                        id: "birthDate",
                        label: "Date de naissance",
                        render: (child) =>
                            editingChild === child.id ? (
                                <input
                                    type="date"
                                    value={birthDate}
                                    onChange={(event) => handleBirthDateChange(event.target.value)}
                                    max={todayIsoDate}
                                />
                            ) : (
                                child.birthDate ? new Date(child.birthDate).toLocaleDateString() : ""
                            ),
                    },
                    {
                        id: "allergies",
                        label: "Allergies",
                        render: (child) =>
                            editingChild === child.id ? (
                                <input
                                    type="text"
                                    value={allergies}
                                    onChange={(event) => setAllergies(event.target.value)}
                                    placeholder="Allergies (optionnel)"
                                />
                            ) : (
                                child.allergies ?? ""
                            ),
                    },
                    {
                        id: "actions",
                        label: "Actions",
                        align: "center",
                        render: (child) =>
                            editingChild === child.id ? (
                                <IconButton aria-label="validate" onClick={() => handleUpdate(child.id)}>
                                    <Check />
                                </IconButton>
                            ) : (
                                <>
                                    <IconButton aria-label="edit" onClick={() => startEditing(child)}>
                                        <Edit />
                                    </IconButton>
                                    <IconButton aria-label="delete" onClick={() => handleDelete(child.id)}>
                                        <Delete />
                                    </IconButton>
                                </>
                            ),
                    },
                ]}
                rows={children}
                getRowId={(child) => child.id}
                emptyMessage="Aucun enfant trouve."
                loading={loading}
                stickyHeader
            />
            {birthDateError && <p style={{ color: "red" }}>{birthDateError}</p>}
            {error && <p style={{ color: "red" }}>{error}</p>}

            <Dialog open={open} onClose={() => setOpen(false)}>
                <DialogContent>
                    <AddChild
                        onChildAdded={() => {
                            loadChildren();
                            setOpen(false);
                        }}
                    />
                </DialogContent>
            </Dialog>
        </div>
    );
}
