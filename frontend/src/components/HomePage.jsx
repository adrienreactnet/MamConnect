import { useEffect, useState } from "react";
import { getAuth } from "../services/authService";

export default function HomePage() {
    const auth = getAuth();
    const user = auth ? auth.user : null;

    const [hasLoggedIn, setHasLoggedIn] = useState(
        () => sessionStorage.getItem("hasLoggedIn") === "true",
    );

    useEffect(() => {
        if (user) {
            sessionStorage.setItem("hasLoggedIn", "true");
            setHasLoggedIn(true);
        }
    }, [user]);

    if (!user) {
        return <p>{hasLoggedIn ? "Vous êtes déconnecté" : "Bienvenue !"}</p>;
    }

    return (
        <div>
            {Object.entries(user).map(([key, value]) => (
                <p key={key}>
                    {key}: {value}
                </p>
            ))}
        </div>
    );
}