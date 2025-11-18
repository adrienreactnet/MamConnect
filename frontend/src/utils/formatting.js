export function formatDate(value, locale = "fr-FR", fallback = "-") {
    if (!value) {
        return fallback;
    }

    try {
        return new Date(value).toLocaleDateString(locale);
    } catch {
        return fallback;
    }
}

export function formatText(value, fallback = "-") {
    if (value === null || value === undefined) {
        return fallback;
    }

    const text = String(value).trim();
    return text.length > 0 ? text : fallback;
}
