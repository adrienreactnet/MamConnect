import React, { useEffect, useState } from "react";
import { DndContext, useDraggable, useDroppable } from "@dnd-kit/core";
import { fetchAssistants } from "../services/assistantService";
import { fetchChildren, updateChild } from "../services/childService";

function ChildItem({ child }) {
    const { attributes, listeners, setNodeRef, transform, isDragging } =
        useDraggable({
            id: String(child.id),
            data: { childId: child.id },
        });

    const style = {
        cursor: isDragging ? "grabbing" : "grab",
        touchAction: "none",
        opacity: isDragging ? 0.6 : 1,
        transform: transform
            ? `translate3d(${transform.x}px, ${transform.y}px, 0)`
            : undefined,
    };

    return (
        <li ref={setNodeRef} style={style} {...listeners} {...attributes}>
            {child.firstName}
        </li>
    );
}

function DroppableColumn({ id, title, childrenNodes }) {
    const { isOver, setNodeRef } = useDroppable({ id });

    const style = {
        padding: "0.5rem",
        border: "1px solid #ddd",
        borderRadius: "0.5rem",
        minWidth: "12rem",
        backgroundColor: isOver ? "#f3f4f6" : "transparent",
    };

    return (
        <div ref={setNodeRef} style={style}>
            <h3>{title}</h3>
            <ul style={{ listStyle: "none", padding: 0, margin: 0 }}>
                {childrenNodes}
            </ul>
        </div>
    );
}

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

    const handleDragEnd = ({ active, over }) => {
        if (!over || !active?.id) {
            return;
        }

        const targetAssistantId =
            over.id === "unassigned" ? null : Number(over.id);
        const childId = Number(active.id);

        if (Number.isNaN(childId)) {
            return;
        }

        void handleDrop(targetAssistantId, childId);
    };

    const renderChild = (child) => <ChildItem key={child.id} child={child} />;

    return (
        <DndContext onDragEnd={handleDragEnd}>
            <div>
                <h2>Associer les enfants aux assistantes</h2>
                <div style={{ display: "flex", gap: "2rem" }}>
                    <DroppableColumn
                        id="unassigned"
                        title="Non assignés"
                        childrenNodes={children
                            .filter((c) => !c.assistantId)
                            .map(renderChild)}
                    />
                    {assistants.map((a) => (
                        <DroppableColumn
                            key={a.id}
                            id={String(a.id)}
                            title={`${a.firstName} ${a.lastName}`}
                            childrenNodes={children
                                .filter((c) => c.assistantId === a.id)
                                .map(renderChild)}
                        />
                    ))}
                </div>
            </div>
        </DndContext>
    );
}
