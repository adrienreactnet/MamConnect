// src/components/AddChild.jsx
import React, { useState } from "react";
import { addChild } from "../services/childService";

export default function AddChild({ onChildAdded }) {
    const [firstName, setFirstName] = useState("");
    const [birthDate, setBirthDate] = useState("");
    const [error, setError] = useState("");
    const [success, setSuccess] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError("");
        setSuccess("");
        try {
            await addChild({ firstName, birthDate });
            setFirstName("");
            setBirthDate("");
            setSuccess("Enfant ajouté !");
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
                    onChange={(e) => setFirstName(e.target.value)}
                    placeholder="Prénom"
                />
                <input
                    type="date"
                    value={birthDate}
                    onChange={(e) => setBirthDate(e.target.value)}
                />
                <button type="submit">Ajouter</button>
            </form>
            {error && <p style={{ color: "red" }}>{error}</p>}
            {success && <p style={{ color: "green" }}>{success}</p>}
        </div>
    );
}