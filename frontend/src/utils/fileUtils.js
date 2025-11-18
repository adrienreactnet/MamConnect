// src/utils/fileUtils.js

/**
 * Converts a File into a base64 data URL for transport.
 * @param {File} file Browser file to convert.
 * @returns {Promise<string>} The resulting data URL.
 */
export function fileToDataUrl(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onload = () => {
            const result = typeof reader.result === "string" ? reader.result : "";
            resolve(result);
        };
        reader.onerror = () => {
            reject(new Error("Impossible de lire le fichier."));
        };
        reader.readAsDataURL(file);
    });
}
