// src/components/DailyReportForm.jsx
import React, { useState } from "react";
import { createReport } from "../services/dailyReportService";

export default function DailyReportForm({ childId, onReportCreated }) {
    const [content, setContent] = useState("");
    const [error, setError] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await createReport(childId, content);
            setContent("");
            setError("");
            if (onReportCreated) onReportCreated();
        } catch (err) {
            setError(err.message);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <textarea
                value={content}
                onChange={(e) => setContent(e.target.value)}
                placeholder="Contenu du compte rendu"
            />
            <button type="submit">Ajouter</button>
            {error && <p style={{ color: "red" }}>{error}</p>}
        </form>
    );
}