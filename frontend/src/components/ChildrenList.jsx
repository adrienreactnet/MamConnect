// src/components/ChildrenList.jsx
import React, { useEffect, useState } from "react";
import { fetchChildren } from "../services/childService";

export default function ChildrenList() {
  const [children, setChildren] = useState([]);
  const [error, setError] = useState("");

  useEffect(() => {
    fetchChildren()
      .then(setChildren)
      .catch((err) => setError(err.message));
  }, []);

  if (error) return <p style={{ color: "red" }}>{error}</p>;

  return (
    <div>
      <h2>Liste des enfants</h2>
      <ul>
        {children.map((child) => (
          <li key={child.id}>{child.firstName}</li>
        ))}
      </ul>
    </div>
  );
}
