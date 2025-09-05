// src/services/dailyReportService.js
const API_BASE_URL = "http://localhost:5293";

export async function fetchReports(childId) {
    const response = await fetch(`${API_BASE_URL}/children/${childId}/reports`);

    if (!response.ok) {
        throw new Error("Erreur lors du chargement des comptes rendus");
    }

    return await response.json();
}

export async function createReport(childId, content) {
    const response = await fetch(`${API_BASE_URL}/children/${childId}/reports`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({ content }),
    });

    if (!response.ok) {
        throw new Error("Erreur lors de la création du compte rendu");
    }

    return await response.json();
}
