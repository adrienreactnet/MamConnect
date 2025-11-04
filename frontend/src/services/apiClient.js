import apiFetch from "./apiFetch";

const DEFAULT_BASE_URL = "http://localhost:5293";
const rawBaseUrl = import.meta.env.VITE_API_BASE_URL;
const API_BASE_URL = (rawBaseUrl ?? DEFAULT_BASE_URL).replace(/\/+$/, "");

function isAbsoluteUrl(path) {
    return /^https?:\/\//i.test(path);
}

function buildUrl(path) {
    if (isAbsoluteUrl(path)) {
        return path;
    }

    const normalizedPath = path.startsWith("/") ? path : `/${path}`;
    return `${API_BASE_URL}${normalizedPath}`;
}

async function readJsonSafely(response) {
    if (response.status === 204 || response.status === 205) {
        return null;
    }

    const text = await response.text();
    if (!text) {
        return null;
    }

    try {
        return JSON.parse(text);
    } catch {
        return null;
    }
}

/**
 * Performs an HTTP request towards the backend API.
 * @param {string} path Relative path or absolute URL to request.
 * @param {object} options Additional options controlling the request and error handling.
 * @param {boolean} [options.expectJson=true] Indicates whether the caller expects a JSON payload.
 * @param {string} [options.defaultErrorMessage] Fallback error message when the response is not successful.
 * @param {(payload: any, response: Response) => string | null | undefined} [options.resolveErrorMessage]
 * Function invoked to compute a domain specific error message.
 * @returns {Promise<any>} The parsed JSON payload when `expectJson` is true; otherwise `undefined`.
 * @throws {Error} When the HTTP response indicates failure.
 */
export async function apiRequest(path, options = {}) {
    const {
        expectJson = true,
        defaultErrorMessage,
        resolveErrorMessage,
        ...fetchOptions
    } = options;

    const response = await apiFetch(buildUrl(path), fetchOptions);
    const payload = await readJsonSafely(response);

    if (!response.ok) {
        const fallbackMessage = defaultErrorMessage ?? "Une erreur réseau est survenue.";
        const resolvedMessage = resolveErrorMessage?.(payload, response);
        const message = resolvedMessage ?? payload?.message ?? payload?.detail ?? fallbackMessage;
        throw new Error(message);
    }

    if (!expectJson) {
        return undefined;
    }

    return payload;
}
