// src/services/dailyReportService.js
import apiFetch from "./apiFetch";

const API_BASE_URL = "http://localhost:5293";

export async function fetchReports() {
    const response = await apiFetch(`${API_BASE_URL}/reports`);

    if (!response.ok) {
        throw new Error("Erreur lors du chargement des comptes rendus");
    }

    return await response.json();
}

export async function fetchChildReports(childId) {
    const response = await apiFetch(`${API_BASE_URL}/reports/children/${childId}`);

    if (!response.ok) {
        throw new Error("Erreur lors du chargement des comptes rendus");
    }

    return await response.json();
}

export async function createReport(childId, content) {
    const response = await apiFetch(`${API_BASE_URL}/reports/children/${childId}`, {
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
