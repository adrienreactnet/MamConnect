import { useState } from "react";
import { login } from "../services/authService";

export default function LoginPage() {
    const [phoneNumber, setPhoneNumber] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState(null);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError(null);
        try {
            const auth = await login(phoneNumber, password);
            console.log("Logged in", auth);
        } catch {
            setError("Échec de l'authentification");
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label>Numéro de téléphone</label>
                <input value={phoneNumber} onChange={e => setPhoneNumber(e.target.value)} />
            </div>
            <div>
                <label>Mot de passe</label>
                <input type="password" value={password} onChange={e => setPassword(e.target.value)} />
            </div>
            <button type="submit">Se connecter</button>
            {error && <p>{error}</p>}
        </form>
    );
}