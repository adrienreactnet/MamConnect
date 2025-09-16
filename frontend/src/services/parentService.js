// src/services/parentService.js
import apiFetch from "./apiFetch";

const API_BASE_URL = "http://localhost:5293";

export async function fetchParents() {
    const response = await apiFetch(`${API_BASE_URL}/parents`);

    if (!response.ok) {
        throw new Error("Erreur lors du chargement des parents");
    }

    return await response.json();
}

export async function addParent(parent) {
    const response = await apiFetch(`${API_BASE_URL}/parents`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(parent),
    });

    if (!response.ok) {
        throw new Error("Erreur lors de l'ajout du parent");
    }

    return await response.json();
}

export async function updateParent(id, payload) {
    const response = await apiFetch(`${API_BASE_URL}/parents/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
    });

    if (!response.ok) {
        throw new Error("Erreur lors de la mise à jour du parent");
    }

    return true;
}

export async function deleteParent(id) {
    const response = await apiFetch(`${API_BASE_URL}/parents/${id}`, {
        method: "DELETE",
    });

    if (!response.ok) {
        throw new Error("Erreur lors de la suppression du parent");
    }

    return true;
}

export async function setParentChildren(id, childrenIds) {
    const response = await apiFetch(`${API_BASE_URL}/parents/${id}/children`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(childrenIds),
    });

    if (!response.ok) {
        throw new Error("Erreur lors de l'association des enfants au parent");
    }

    return true;
}