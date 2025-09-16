// src/components/AddParent.jsx
import React, { useState, useEffect } from "react";
import { addParent } from "../services/parentService";
import { fetchChildren } from "../services/childService";

export default function AddParent({ onParentAdded }) {
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");
    const [email, setEmail] = useState("");
    const [children, setChildren] = useState([]);
    const [selectedChildren, setSelectedChildren] = useState([]);
    const [error, setError] = useState("");
    const [success, setSuccess] = useState("");

    useEffect(() => {
        (async () => {
            try {
                const data = await fetchChildren();
                setChildren(data);
            } catch (err) {
                setError(err.message);
            }
        })();
    }, []);

    const toggleChild = (id) => {
        setSelectedChildren(prev =>
            prev.includes(id) ? prev.filter(c => c !== id) : [...prev, id]
        );
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError("");
        setSuccess("");
        try {
            await addParent({ firstName, lastName, phoneNumber, email, childrenIds: selectedChildren });
            setFirstName("");
            setLastName("");
            setPhoneNumber("");
            setEmail("");
            setSelectedChildren([]);
            setSuccess("Parent ajouté !");
            if (onParentAdded) {
                onParentAdded();
            }
        } catch (err) {
            setError(err.message);
        }
    };

    return (
        <div>
            <h2>Ajouter un parent</h2>
            <form onSubmit={handleSubmit}>
                <input type="text" value={firstName} onChange={(e) => setFirstName(e.target.value)} placeholder="Prénom" />
                <input type="text" value={lastName} onChange={(e) => setLastName(e.target.value)} placeholder="Nom" />
                <input type="tel" value={phoneNumber} onChange={(e) => setPhoneNumber(e.target.value)} placeholder="Téléphone" />
                <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} placeholder="Email" />
                <div>
                    <p>Enfants :</p>
                    {children.map((child) => (
                        <label key={child.id} style={{ display: "block" }}>
                            <input
                                type="checkbox"
                                checked={selectedChildren.includes(child.id)}
                                onChange={() => toggleChild(child.id)}
                            />
                            {child.firstName}
                        </label>
                    ))}
                </div>
                <button type="submit">Ajouter</button>
            </form>
            {error && <p style={{ color: "red" }}>{error}</p>}
            {success && <p style={{ color: "green" }}>{success}</p>}
        </div>
    );
}