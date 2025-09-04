// src/services/childService.js
const API_BASE_URL = "http://localhost:5293";

export async function fetchChildren() {
    const response = await fetch(`${API_BASE_URL}/children`);

    if (!response.ok) {
        throw new Error("Erreur lors du chargement des enfants");
    }

    return await response.json();
}
