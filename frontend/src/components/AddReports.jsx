// src/components/ReportsPage.jsx
import React, { useEffect, useState } from "react";
import { fetchChildren } from "../services/childService";
import DailyReportForm from "./DailyReportForm";

export default function AddReports() {
    const [children, setChildren] = useState([]);
    const [error, setError] = useState("");

    useEffect(() => {
        fetchChildren()
            .then(setChildren)
            .catch((err) => setError(err.message));
    }, []);

    if (error) return <p style={{ color: "red" }}>{error}</p>;

    return (
        <div>
            <h2>Rapports quotidiens</h2>
            {children.map((child) => (
                <div key={child.id}>
                    <h3>{child.firstName}</h3>
                    <DailyReportForm childId={child.id} />
                </div>
            ))}
        </div>
    );
}