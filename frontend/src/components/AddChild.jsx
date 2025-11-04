// src/components/AddChild.jsx
import React, { useState } from "react";
import { addChild } from "../services/childService";

const todayIsoDate = new Date().toISOString().split("T")[0];

export default function AddChild({ onChildAdded }) {
    const [firstName, setFirstName] = useState("");
    const [birthDate, setBirthDate] = useState("");
    const [birthDateError, setBirthDateError] = useState("");
    const [error, setError] = useState("");
    const [success, setSuccess] = useState("");

    const handleBirthDateChange = (event) => {
        const selectedDate = event.target.value;
        if (selectedDate !== "" && selectedDate > todayIsoDate) {
            setBirthDateError("La date de naissance ne peut pas depasser la date du jour.");
            return;
        }
        setBirthDateError("");
        setBirthDate(selectedDate);
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        setError("");
        setSuccess("");
        setBirthDateError("");
        const trimmedFirstName = firstName.trim();
        if (trimmedFirstName.length === 0) {
            setError("Le prénom est requis.");
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
            await addChild({ firstName: trimmedFirstName, birthDate });
            setFirstName("");
            setBirthDate("");
            setBirthDateError("");
            setSuccess("Enfant ajoutǸ !");
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
                    placeholder="Prénom"
                />
                <input
                    type="date"
                    value={birthDate}
                    onChange={handleBirthDateChange}
                    max={todayIsoDate}
                />
                {birthDateError && <p style={{ color: "red" }}>{birthDateError}</p>}
                <button type="submit">Ajouter</button>
            </form>
            {error && <p style={{ color: "red" }}>{error}</p>}
            {success && <p style={{ color: "green" }}>{success}</p>}
        </div>
    );
}
