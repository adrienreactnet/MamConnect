// src/components/ChildrenList.jsx
import React, { useEffect, useState } from "react";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import DialogContent from "@mui/material/DialogContent";
import IconButton from "@mui/material/IconButton";
import Stack from "@mui/material/Stack";
import Typography from "@mui/material/Typography";
import { Edit, Delete, Check, Add } from "@mui/icons-material";
import AddChild from "./AddChild";
import { fetchChildren, updateChild, deleteChild } from "../services/childService";
import DataTable from "./DataTable";
import { fileToDataUrl } from "../utils/fileUtils";

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
    const [headshotUrl, setHeadshotUrl] = useState("");
    const [headshotPreview, setHeadshotPreview] = useState("");
    const [headshotError, setHeadshotError] = useState("");
    const HEADSHOT_MAX_BYTES = 2 * 1024 * 1024;

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
        setHeadshotUrl(child.headshotUrl ?? "");
        setHeadshotPreview(child.headshotUrl ?? "");
        setHeadshotError("");
    };

    const resetEditingState = () => {
        setEditingChild(null);
        setFirstName("");
        setLastName("");
        setBirthDate("");
        setBirthDateError("");
        setError("");
        setAllergies("");
        setHeadshotUrl("");
        setHeadshotPreview("");
        setHeadshotError("");
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

        if (headshotError) {
            setError("Veuillez corriger la photo avant d'enregistrer.");
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
                headshotUrl: headshotUrl.length > 0 ? headshotUrl : null,
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

    const handleHeadshotChange = async (event) => {
        setHeadshotError("");
        const input = event.target;
        const file = input.files && input.files[0];
        if (!file) {
            return;
        }
        if (file.size > HEADSHOT_MAX_BYTES) {
            setHeadshotError("La photo doit peser au maximum 2 Mo.");
            input.value = "";
            return;
        }

        try {
            const dataUrl = await fileToDataUrl(file);
            setHeadshotPreview(dataUrl);
            setHeadshotUrl(dataUrl);
        } catch (fileError) {
            setHeadshotError(fileError.message ?? "Impossible de traiter la photo.");
        } finally {
            input.value = "";
        }
    };

    const clearHeadshot = () => {
        setHeadshotUrl("");
        setHeadshotPreview("");
        setHeadshotError("");
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
                        id: "headshot",
                        label: "Photo",
                        render: (child) => {
                            const avatarSource = editingChild === child.id ? headshotPreview : child.headshotUrl ?? "";
                            return (
                                <Stack direction="row" spacing={2} alignItems="center">
                                    <Avatar
                                        src={avatarSource || undefined}
                                        alt={`${child.firstName ?? ""} ${child.lastName ?? ""}`}
                                        sx={{ width: 48, height: 48 }}
                                    >
                                        {(child.firstName ?? "?").slice(0, 1).toUpperCase()}
                                    </Avatar>
                                    {editingChild === child.id && (
                                        <Stack spacing={0.5}>
                                            <Button variant="outlined" size="small" component="label">
                                                Modifier
                                                <input
                                                    hidden
                                                    type="file"
                                                    accept="image/png, image/jpeg, image/webp"
                                                    onChange={handleHeadshotChange}
                                                />
                                            </Button>
                                            <Button
                                                variant="text"
                                                size="small"
                                                onClick={clearHeadshot}
                                                disabled={!headshotPreview}
                                                sx={{ alignSelf: "flex-start" }}
                                            >
                                                Retirer
                                            </Button>
                                            {headshotError && (
                                                <Typography variant="caption" color="error">
                                                    {headshotError}
                                                </Typography>
                                            )}
                                        </Stack>
                                    )}
                                </Stack>
                            );
                        },
                    },
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
