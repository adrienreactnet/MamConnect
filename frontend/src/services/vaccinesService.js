// src/services/vaccinesService.js
import { apiRequest } from "./apiClient";

export async function getVaccines() {
    return await apiRequest("/vaccines", {
        defaultErrorMessage: "Erreur lors du chargement des vaccins",
    });
}

function buildPayload(data) {
    return {
        name: data.name,
        ageInMonths: Number(data.ageInMonths),
    };
}

export async function createVaccine(data) {
    return await apiRequest("/vaccines", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(buildPayload(data)),
        defaultErrorMessage: "Erreur lors de la création du vaccin",
    });
}

export async function updateVaccine(id, data) {
    await apiRequest(`/vaccines/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(buildPayload(data)),
        expectJson: false,
        defaultErrorMessage: "Erreur lors de la mise à jour du vaccin",
    });

    return true;
}

export async function deleteVaccine(id) {
    await apiRequest(`/vaccines/${id}`, {
        method: "DELETE",
        expectJson: false,
        defaultErrorMessage: "Erreur lors de la suppression du vaccin",
    });

    return true;
}
