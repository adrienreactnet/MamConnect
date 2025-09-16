import React from "react";
import { useEffect, useState } from "react";
import { fetchReports } from "../services/dailyReportService";
import AddReports from "./AddReports";


export default function ReportsList() {
    const [reports, setReports] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [showForm, setShowForm] = useState(false);

    useEffect(() => {
        const loadReports = async () => {
            try {
                const data = await fetchReports();
                setReports(data);
            } catch (error) {
                setError(error.message);
            } finally {
                setLoading(false);
            }
        };

        loadReports();
    }, []);

    if (loading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>Error: {error}</div>;
    }

    return (
        <div>
            <h2>Liste des rapports</h2>
            <button type="button" onClick={() => setShowForm((prev) => !prev)}>
                {showForm ? "Fermer le formulaire" : "Ajouter un rapport"}
            </button>
            {showForm && <AddReports />}
            {
                reports.map(element => (
                    <div key={element.id}>
                        <p>{element.content}</p>
                    </div>
                ))
            }
        </div>
    );
}