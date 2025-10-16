// src/services/vaccinesService.js
import apiFetch from "./apiFetch";

const API_BASE_URL = "http://localhost:5293/api";

export async function getVaccines() {
    const response = await apiFetch(`${API_BASE_URL}/vaccines`);

    if (!response.ok) {
        throw new Error("Erreur lors du chargement des vaccins");
    }

    return await response.json();
}

function buildPayload(data) {
    return {
        name: data.name,
        ageInMonths: Number(data.ageInMonths),
    };
}

export async function createVaccine(data) {
    const response = await apiFetch(`${API_BASE_URL}/vaccines`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(buildPayload(data)),
    });

    if (!response.ok) {
        throw new Error("Erreur lors de la création du vaccin");
    }

    return await response.json();
}

export async function updateVaccine(id, data) {
    const response = await apiFetch(`${API_BASE_URL}/vaccines/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(buildPayload(data)),
    });

    if (!response.ok) {
        throw new Error("Erreur lors de la mise à jour du vaccin");
    }

    return true;
}

export async function deleteVaccine(id) {
    const response = await apiFetch(`${API_BASE_URL}/vaccines/${id}`, {
        method: "DELETE",
    });

    if (!response.ok) {
        throw new Error("Erreur lors de la suppression du vaccin");
    }

    return true;
}