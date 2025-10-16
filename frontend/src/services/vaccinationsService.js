import apiFetch from "./apiFetch";

const API_BASE_URL = "http://localhost:5293/api/vaccinations";

async function parseJsonSafely(response) {
    try {
        return await response.json();
    } catch {
        return null;
    }
}

function buildError(message, fallback) {
    return new Error(message ?? fallback);
}

export async function getChildVaccinationSchedule(childId) {
    const response = await apiFetch(`${API_BASE_URL}/children/${childId}`);
    const data = await parseJsonSafely(response);

    if (!response.ok) {
        throw buildError(data?.detail, "Impossible de charger le calendrier vaccinal de l'enfant.");
    }

    return data;
}

export async function updateChildVaccine(childId, vaccineId, payload) {
    const response = await apiFetch(`${API_BASE_URL}/children/${childId}/vaccines/${vaccineId}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
    });

    const data = await parseJsonSafely(response);

    if (!response.ok) {
        throw buildError(data?.detail, "La mise à jour du statut vaccinal a échoué.");
    }

    return data;
}

export async function getVaccinationOverview() {
    const response = await apiFetch(`${API_BASE_URL}/overview`);
    const data = await parseJsonSafely(response);

    if (!response.ok) {
        throw buildError(data?.detail, "Impossible de charger l'aperçu vaccinal.");
    }

    return data;
}
