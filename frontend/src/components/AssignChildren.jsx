import React, { useEffect, useState } from "react";
import { fetchAssistants } from "../services/assistantService";
import { fetchChildren, updateChild } from "../services/childService";

export default function AssignChildren() {
    const [assistants, setAssistants] = useState([]);
    const [children, setChildren] = useState([]);

    useEffect(() => {
        (async () => {
            const [a, c] = await Promise.all([
                fetchAssistants(),
                fetchChildren(),
            ]);
            setAssistants(a);
            setChildren(c);
        })();
    }, []);

    const handleDrop = async (assistantId, childId) => {
        const child = children.find((c) => c.id === Number(childId));
        if (!child) return;
        await updateChild(child.id, {
            ...child,
            assistantId,
        });
        setChildren((prev) =>
            prev.map((c) =>
                c.id === child.id ? { ...c, assistantId } : c,
            ),
        );
    };

    const renderChild = (child) => (
        <li
            key={child.id}
            draggable
            onDragStart={(e) =>
                e.dataTransfer.setData("text/plain", String(child.id))
            }
            style={{ cursor: "grab" }}
        >
            {child.firstName}
        </li>
    );

    return (
        <div>
            <h2>Associer les enfants aux assistantes</h2>
            <div style={{ display: "flex", gap: "2rem" }}>
                <div
                    onDragOver={(e) => e.preventDefault()}
                    onDrop={(e) => {
                        e.preventDefault();
                        const id = e.dataTransfer.getData("text/plain");
                        handleDrop(null, id);
                    }}
                >
                    <h3>Non assignés</h3>
                    <ul>
                        {children
                            .filter((c) => !c.assistantId)
                            .map(renderChild)}
                    </ul>
                </div>
                {assistants.map((a) => (
                    <div
                        key={a.id}
                        onDragOver={(e) => e.preventDefault()}
                        onDrop={(e) => {
                            e.preventDefault();
                            const id = e.dataTransfer.getData("text/plain");
                            handleDrop(a.id, id);
                        }}
                    >
                        <h3>
                            {a.firstName} {a.lastName}
                        </h3>
                        <ul>
                            {children
                                .filter((c) => c.assistantId === a.id)
                                .map(renderChild)}
                        </ul>
                    </div>
                ))}
            </div>
        </div>
    );
}