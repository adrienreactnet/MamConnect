import { apiRequest } from "./apiClient";

const VACCINATIONS_BASE_PATH = "/api/vaccinations";

export async function getChildVaccinationSchedule(childId) {
    return await apiRequest(`${VACCINATIONS_BASE_PATH}/children/${childId}`, {
        defaultErrorMessage: "Impossible de charger le calendrier vaccinal de l'enfant.",
    });
}

export async function updateChildVaccine(childId, vaccineId, payload) {
    return await apiRequest(`${VACCINATIONS_BASE_PATH}/children/${childId}/vaccines/${vaccineId}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
        defaultErrorMessage: "La mise à jour du statut vaccinal a échoué.",
    });
}

export async function getVaccinationOverview() {
    return await apiRequest(`${VACCINATIONS_BASE_PATH}/overview`, {
        defaultErrorMessage: "Impossible de charger l'aperçu vaccinal.",
    });
}
