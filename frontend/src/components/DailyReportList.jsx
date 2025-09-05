// src/components/DailyReportList.jsx
import React, { useEffect, useState } from "react";
import { fetchReports } from "../services/dailyReportService";

export default function DailyReportList({ childId, refresh }) {
    const [reports, setReports] = useState([]);
    const [error, setError] = useState("");

    useEffect(() => {
        if (!childId) return;
        fetchReports(childId)
            .then(setReports)
            .catch((err) => setError(err.message));
    }, [childId, refresh]);

    if (error) return <p style={{ color: "red" }}>{error}</p>;

    return (
        <div>
            <h2>Comptes rendus</h2>
            <ul>
                {reports.map((report) => (
                    <li key={report.id}>{report.content}</li>
                ))}
            </ul>
        </div>
    );
}