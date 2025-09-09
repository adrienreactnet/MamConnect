import React from "react";
import { useEffect, useState } from "react";
import { fetchReports } from "../services/dailyReportService";


export default function ReportsList() {
    const [reports, setReports] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

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