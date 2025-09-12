const API_BASE_URL = "http://localhost:5293";

export async function login(phoneNumber, password) {
    const response = await fetch(`${API_BASE_URL}/auth/login`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({ phoneNumber, password }),
    });

    if (response.ok) {
        return await response.json();
    }

    if (response.status === 401) {
        const setPasswordResponse = await fetch(`${API_BASE_URL}/auth/set-password`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({ phoneNumber, newPassword: password }),
        });

        if (!setPasswordResponse.ok) {
            throw new Error("Authentication failed");
        }

        return await setPasswordResponse.json();
    }

    throw new Error("Authentication failed");
}
