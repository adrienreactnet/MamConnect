// src/services/assistantService.js
import { apiRequest } from "./apiClient";

export async function fetchAssistants() {
    return await apiRequest("/assistants", {
        defaultErrorMessage: "Erreur lors du chargement des assistantes",
    });
}

export async function addAssistant(assistant) {
    return await apiRequest("/assistants", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(assistant),
        defaultErrorMessage: "Erreur lors de l'ajout de l'assistante",
    });
}

export async function updateAssistant(id, payload) {
    await apiRequest(`/assistants/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
        expectJson: false,
        defaultErrorMessage: "Erreur lors de la mise à jour de l'assistante",
    });

    return true;
}

export async function deleteAssistant(id) {
    await apiRequest(`/assistants/${id}`, {
        method: "DELETE",
        expectJson: false,
        defaultErrorMessage: "Erreur lors de la suppression de l'assistante",
    });

    return true;
}
