// src/components/AddChild.jsx
import React, { useState } from "react";
import { addChild } from "../services/childService";
import { fileToDataUrl } from "../utils/fileUtils";

const todayIsoDate = new Date().toISOString().split("T")[0];
const MAX_HEADSHOT_BYTES = 2 * 1024 * 1024; // 2MB

export default function AddChild({ onChildAdded }) {
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [birthDate, setBirthDate] = useState("");
    const [birthDateError, setBirthDateError] = useState("");
    const [error, setError] = useState("");
    const [success, setSuccess] = useState("");
    const [allergies, setAllergies] = useState("");
    const [headshotPreview, setHeadshotPreview] = useState("");
    const [headshotUrl, setHeadshotUrl] = useState("");
    const [headshotError, setHeadshotError] = useState("");

    const handleBirthDateChange = (event) => {
        const selectedDate = event.target.value;
        if (selectedDate !== "" && selectedDate > todayIsoDate) {
            setBirthDateError("La date de naissance ne peut pas depasser la date du jour.");
            return;
        }

        setBirthDateError("");
        setBirthDate(selectedDate);
    };

    const handleHeadshotChange = async (event) => {
        setHeadshotError("");
        const input = event.target;
        const file = input.files && input.files[0];
        if (!file) {
            return;
        }
        if (file.size > MAX_HEADSHOT_BYTES) {
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
        setHeadshotPreview("");
        setHeadshotUrl("");
        setHeadshotError("");
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        setError("");
        setSuccess("");
        setBirthDateError("");

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
            await addChild({
                firstName: trimmedFirstName,
                lastName: trimmedLastName,
                birthDate,
                allergies: trimmedAllergies.length > 0 ? trimmedAllergies : null,
                headshotUrl: headshotUrl.length > 0 ? headshotUrl : null,
            });
            setFirstName("");
            setLastName("");
            setBirthDate("");
            setBirthDateError("");
            setAllergies("");
            clearHeadshot();
            setSuccess("Enfant ajoute !");
            if (onChildAdded) {
                onChildAdded();
            }
        } catch (err) {
            setError(err.message);
        }
    };

    return (
        <div>
            <h2>Ajouter un enfant</h2>
            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    value={firstName}
                    onChange={(event) => setFirstName(event.target.value)}
                    placeholder="Prenom"
                />
                <input
                    type="text"
                    value={lastName}
                    onChange={(event) => setLastName(event.target.value)}
                    placeholder="Nom"
                />
                <input
                    type="date"
                    value={birthDate}
                    onChange={handleBirthDateChange}
                    max={todayIsoDate}
                />
                <input
                    type="text"
                    value={allergies}
                    onChange={(event) => setAllergies(event.target.value)}
                    placeholder="Allergies (optionnel)"
                />
                <div style={{ marginTop: "1rem" }}>
                    <p style={{ margin: "0 0 0.5rem" }}>Photo de l'enfant (optionnel)</p>
                    <label
                        htmlFor="new-child-headshot"
                        style={{
                            display: "inline-block",
                            padding: "0.5rem 1rem",
                            border: "1px solid #1976d2",
                            borderRadius: "4px",
                            color: "#1976d2",
                            cursor: "pointer",
                            fontSize: "0.9rem",
                        }}
                    >
                        Choisir une photo
                    </label>
                    <input
                        id="new-child-headshot"
                        type="file"
                        accept="image/png, image/jpeg, image/webp"
                        style={{ display: "none" }}
                        onChange={handleHeadshotChange}
                    />
                    {headshotPreview ? (
                        <div style={{ marginTop: "0.75rem" }}>
                            <img
                                src={headshotPreview}
                                alt="Portrait de l'enfant"
                                style={{ width: "96px", height: "96px", borderRadius: "50%", objectFit: "cover" }}
                            />
                            <div>
                                <button type="button" onClick={clearHeadshot} style={{ marginTop: "0.5rem" }}>
                                    Retirer la photo
                                </button>
                            </div>
                        </div>
                    ) : (
                        <p style={{ marginTop: "0.5rem", color: "#6b6b6b", fontSize: "0.85rem" }}>
                            Formats png/jpg/webp, 2 Mo maximum.
                        </p>
                    )}
                    {headshotError && <p style={{ color: "red" }}>{headshotError}</p>}
                </div>
                {birthDateError && <p style={{ color: "red" }}>{birthDateError}</p>}
                <button type="submit">Ajouter</button>
            </form>
            {error && <p style={{ color: "red" }}>{error}</p>}
            {success && <p style={{ color: "green" }}>{success}</p>}
        </div>
    );
}
