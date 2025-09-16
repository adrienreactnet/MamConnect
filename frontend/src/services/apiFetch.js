import { getAuth, logout } from "./authService";
import { dispatchLogoutEvent } from "./authEvents";

function triggerLogoutSideEffects() {
    logout();
    if (typeof window !== "undefined") {
        window.location.hash = "#login";
    }
    dispatchLogoutEvent();
}

export default async function apiFetch(url, options = {}) {
    const { headers: providedHeaders, ...restOptions } = options;
    const headers = { ...(providedHeaders || {}) };

    const auth = getAuth();
    if (auth?.token) {
        headers.Authorization = `Bearer ${auth.token}`;
    }

    const response = await fetch(url, {
        ...restOptions,
        headers,
    });

    if (response.status === 401) {
        triggerLogoutSideEffects();
    }

    return response;
}