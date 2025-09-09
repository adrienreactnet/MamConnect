// src/components/ChildrenList.jsx
import React, { useEffect, useState } from "react";
import IconButton from "@mui/material/IconButton";
import { Edit, Delete, Check } from "@mui/icons-material";
import { fetchChildren, updateChild, deleteChild } from "../services/childService";

export default function ChildrenList() {
  const [children, setChildren] = useState([]);
  const [error, setError] = useState("");
  const [editingChild, setEditingChild] = useState(null);
  const [firstName, setFirstName] = useState("");
  const [birthDate, setBirthDate] = useState("");
  
  const loadChildren = async () => {
    try {
      const data = await fetchChildren();
      setChildren(data);
      setError("");
    } catch (err) {
      setError(err.message);
    }
  }

  useEffect(() => {
    loadChildren();
  }, []);

  const startEditing = (child) => {
    setEditingChild(child.id);
    setFirstName(child.firstName);
    setBirthDate(child.birthDate || "");
  };

  const handleUpdate = async (id) => {
    try {
      await updateChild(id, { firstName, birthDate });
      await loadChildren();
      setEditingChild(null);
      setFirstName("");
      setBirthDate("");
    } catch (err) {
      setError(err.message);
    }
  };

  const handleDelete = async (id) => {
    try {
      await deleteChild(id);
      await loadChildren();
    } catch (err) {
      setError(err.message);
    }
  };

  return (
    <div>
      <h2>Liste des enfants</h2>
      {children.length === 0 && <p>Aucun enfant trouvé.</p>}
      <ul>
        {children.map((child) => (
          <li key={child.id}>
            {editingChild === child.id ? (
              <>
                <input
                  type="text"
                  value={firstName}
                  onChange={(e) => setFirstName(e.target.value)}
                />
                <input
                  type="date"
                  value={birthDate}
                  onChange={(e) => setBirthDate(e.target.value)}
                />
                <IconButton aria-label="validate" onClick={() => handleUpdate(child.id)}>
                  <Check />
                </IconButton>
              </>
            ) : (
              <>
                {child.firstName}
                <IconButton aria-label="edit" onClick={() => startEditing(child)}>
                  <Edit />
                </IconButton>
                <IconButton aria-label="delete" onClick={() => handleDelete(child.id)}>
                  <Delete />
                </IconButton>
              </>
            )}
          </li>
        ))}
      </ul>
      {error && <p style={{ color: "red" }}>{error}</p>}
    </div>
  );
}
