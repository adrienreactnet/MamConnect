// src/services/childService.js
import { apiRequest } from "./apiClient";

export async function fetchChildren() {
    return await apiRequest("/children", {
        defaultErrorMessage: "Erreur lors du chargement des enfants",
    });
}

export async function fetchChildrenWithRelations() {
    return await apiRequest("/children/with-relations", {
        defaultErrorMessage: "Erreur lors du chargement des enfants avec relations",
    });
}

export async function addChild(child) {
    return await apiRequest("/children", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(child),
        defaultErrorMessage: "Erreur lors de l'ajout de l'enfant",
        resolveErrorMessage: (payload, response) => {
            if (response.status === 409) {
                return payload?.message ?? "Un enfant portant ce prénom existe déjà.";
            }

            return undefined;
        },
    });
}

export async function updateChild(id, payload) {
    await apiRequest(`/children/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
        expectJson: false,
        defaultErrorMessage: "Erreur lors de la mise à jour de l'enfant",
    });

    return true;
}

export async function deleteChild(id) {
    await apiRequest(`/children/${id}`, {
        method: "DELETE",
        expectJson: false,
        defaultErrorMessage: "Erreur lors de la suppression de l'enfant",
    });

    return true;
}
