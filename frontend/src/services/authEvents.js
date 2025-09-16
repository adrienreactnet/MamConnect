export const LOGOUT_EVENT_NAME = "auth:logout";

export function dispatchLogoutEvent() {
    if (typeof window === "undefined" || typeof CustomEvent !== "function") {
        return;
    }

    window.dispatchEvent(new CustomEvent(LOGOUT_EVENT_NAME));
}