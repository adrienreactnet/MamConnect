// src/services/parentService.js
import { apiRequest } from "./apiClient";

export async function fetchParents() {
    return await apiRequest("/parents", {
        defaultErrorMessage: "Erreur lors du chargement des parents",
    });
}

export async function addParent(parent) {
    return await apiRequest("/parents", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(parent),
        defaultErrorMessage: "Erreur lors de l'ajout du parent",
    });
}

export async function updateParent(id, payload) {
    await apiRequest(`/parents/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
        expectJson: false,
        defaultErrorMessage: "Erreur lors de la mise à jour du parent",
    });

    return true;
}

export async function deleteParent(id) {
    await apiRequest(`/parents/${id}`, {
        method: "DELETE",
        expectJson: false,
        defaultErrorMessage: "Erreur lors de la suppression du parent",
    });

    return true;
}

export async function setParentChildren(id, childrenIds) {
    await apiRequest(`/parents/${id}/children`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(childrenIds),
        expectJson: false,
        defaultErrorMessage: "Erreur lors de l'association des enfants au parent",
    });

    return true;
}
