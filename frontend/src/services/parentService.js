// src/services/parentService.js
import { getAuth } from "./authService";

const API_BASE_URL = "http://localhost:5293";

export async function fetchParents() {
    const token = getAuth()?.token;
    const response = await fetch(`${API_BASE_URL}/parents`, {
        headers: {
            Authorization: `Bearer ${token}`,
        },
    });

    if (!response.ok) {
        throw new Error("Erreur lors du chargement des parents");
    }

    return await response.json();
}

export async function addParent(parent) {
    const token = getAuth()?.token;
    const response = await fetch(`${API_BASE_URL}/parents`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(parent),
    });

    if (!response.ok) {
        throw new Error("Erreur lors de l'ajout du parent");
    }

    return await response.json();
}

export async function updateParent(id, payload) {
    const token = getAuth()?.token;
    const response = await fetch(`${API_BASE_URL}/parents/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(payload),
    });

    if (!response.ok) {
        throw new Error("Erreur lors de la mise à jour du parent");
    }

    return true;
}

export async function deleteParent(id) {
    const token = getAuth()?.token;
    const response = await fetch(`${API_BASE_URL}/parents/${id}`, {
        method: "DELETE",
        headers: {
            Authorization: `Bearer ${token}`,
        },
    });

    if (!response.ok) {
        throw new Error("Erreur lors de la suppression du parent");
    }

    return true;
}

export async function setParentChildren(id, childrenIds) {
    const token = getAuth()?.token;
    const response = await fetch(`${API_BASE_URL}/parents/${id}/children`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(childrenIds),
    });

    if (!response.ok) {
        throw new Error("Erreur lors de l'association des enfants au parent");
    }

    return true;
}
