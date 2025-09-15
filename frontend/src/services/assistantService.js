// src/services/assistantService.js
import { getAuth } from "./authService";

const API_BASE_URL = "http://localhost:5293";

export async function fetchAssistants() {
    const token = getAuth()?.token;
    const response = await fetch(`${API_BASE_URL}/assistants`, {
        headers: {
            Authorization: `Bearer ${token}`,
        },
    });

    if (!response.ok) {
        throw new Error("Erreur lors du chargement des assistantes");
    }

    return await response.json();
}

export async function addAssistant(assistant) {
    const token = getAuth()?.token;
    const response = await fetch(`${API_BASE_URL}/assistants`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(assistant),
    });

    if (!response.ok) {
        throw new Error("Erreur lors de l'ajout de l'assistante");
    }

    return await response.json();
}

export async function updateAssistant(id, payload) {
    const token = getAuth()?.token;
    const response = await fetch(`${API_BASE_URL}/assistants/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(payload),
    });

    if (!response.ok) {
        throw new Error("Erreur lors de la mise à jour de l'assistante");
    }

    return true;
}

export async function deleteAssistant(id) {
    const token = getAuth()?.token;
    const response = await fetch(`${API_BASE_URL}/assistants/${id}`, {
        method: "DELETE",
        headers: {
            Authorization: `Bearer ${token}`,
        },
    });

    if (!response.ok) {
        throw new Error("Erreur lors de la suppression de l'assistante");
    }

    return true;
}
