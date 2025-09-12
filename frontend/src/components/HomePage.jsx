import { getAuth } from "../services/authService";

export default function HomePage() {
    const auth = getAuth();
    const user = auth ? auth.user : null;

    if (!user) {
        return <p>Aucun utilisateur connecté</p>;
    }

    return (
        <div>
            {Object.entries(user).map(([key, value]) => (
                <p key={key}>{key}: {value}</p>
            ))}
        </div>
    );
}