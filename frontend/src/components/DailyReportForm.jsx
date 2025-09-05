// src/components/DailyReportForm.jsx
import React, { useState } from "react";
import { createReport } from "../services/dailyReportService";

export default function DailyReportForm({ childId, onReportCreated }) {
    const [content, setContent] = useState("");
    const [error, setError] = useState("");
    const [submitted, setSubmitted] = useState(false);

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await createReport(childId, content);
            setContent("");
            setError("");
            setSubmitted(true);
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
                disabled={submitted}
            />
            <button type="submit" disabled={submitted}>Soumettre</button>
            {error && <p style={{ color: "red" }}>{error}</p>}
            {submitted && <p style={{ color: "green" }}>Rapport envoyé&nbsp;!</p>}
        </form>
    );
}