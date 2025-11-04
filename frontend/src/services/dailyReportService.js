// src/services/dailyReportService.js
import { apiRequest } from "./apiClient";

export async function fetchReports() {
    return await apiRequest("/reports", {
        defaultErrorMessage: "Erreur lors du chargement des comptes rendus",
    });
}

export async function fetchChildReports(childId) {
    return await apiRequest(`/reports/children/${childId}`, {
        defaultErrorMessage: "Erreur lors du chargement des comptes rendus",
    });
}

export async function createReport(childId, content) {
    return await apiRequest(`/reports/children/${childId}`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({ content }),
        defaultErrorMessage: "Erreur lors de la création du compte rendu",
    });
}
