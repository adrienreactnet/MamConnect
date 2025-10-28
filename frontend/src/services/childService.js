// src/services/childService.js
import apiFetch from "./apiFetch";

const API_BASE_URL = "http://localhost:5293";

export async function fetchChildren() {
    const response = await apiFetch(`${API_BASE_URL}/children`);

    if (!response.ok) {
        throw new Error("Erreur lors du chargement des enfants");
    }

    return await response.json();
}

export async function fetchChildrenWithRelations() {
    const response = await apiFetch(`${API_BASE_URL}/children/with-relations`);

    if (!response.ok) {
        throw new Error("Erreur lors du chargement des enfants avec relations");
    }

    return await response.json();
}


export async function addChild(child) {
    const response = await apiFetch(`${API_BASE_URL}/children`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(child),
    });

    if (!response.ok) {
        if (response.status === 409) {
            let message = "Un enfant portant ce prénom existe déjà.";
            try {
                const payload = await response.json();
                if (payload?.message) {
                    message = payload.message;
                }
            } catch (err) { }
            throw new Error(message);
        }
        throw new Error("Erreur lors de l'ajout de l'enfant");
    }

    return await response.json();
}

export async function updateChild(id, payload) {
    const response = await apiFetch(`${API_BASE_URL}/children/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
    });

    if (!response.ok) {
        throw new Error("Erreur lors de la mise à jour de l'enfant");
    }

    return true;
}

export async function deleteChild(id) {
    const response = await apiFetch(`${API_BASE_URL}/children/${id}`, {
        method: "DELETE",
    });

    if (!response.ok) {
        throw new Error("Erreur lors de la suppression de l'enfant");
    }

    return true;
}
