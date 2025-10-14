// src/components/ChildrenList.jsx
import React, { useEffect, useState } from "react";
import IconButton from "@mui/material/IconButton";
import Dialog from "@mui/material/Dialog";
import DialogContent from "@mui/material/DialogContent";
import { Edit, Delete, Check, Add } from "@mui/icons-material";
import AddChild from "./AddChild";
import { fetchChildren, updateChild, deleteChild } from "../services/childService";

export default function ChildrenList() {
  const [children, setChildren] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [editingChild, setEditingChild] = useState(null);
  const [firstName, setFirstName] = useState("");
  const [birthDate, setBirthDate] = useState("");
  const [open, setOpen] = useState(false);

  const loadChildren = async () => {
    setLoading(true);
    try {
      const data = await fetchChildren();
      setChildren(data);
      setError("");
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
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
      <IconButton aria-label="add" onClick={() => setOpen(true)}>
        <Add />
      </IconButton>
      {!loading && children.length === 0 && <p>Aucun enfant trouvé.</p>}
      {loading ? (
        <p>Chargement...</p>
      ) : (
        <table style={{ width: "100%", borderCollapse: "collapse" }}>
          <thead>
            <tr>
              <th style={{ textAlign: "left", padding: "8px", borderBottom: "1px solid #ccc" }}>
                Nom de l'enfant
              </th>
              <th style={{ textAlign: "left", padding: "8px", borderBottom: "1px solid #ccc" }}>
                Date de naissance
              </th>
              <th style={{ textAlign: "center", padding: "8px", borderBottom: "1px solid #ccc" }}>
                Actions
              </th>
            </tr>
          </thead>
          <tbody>
            {children.map((child) => (
              <tr key={child.id}>
                <td style={{ padding: "8px", borderBottom: "1px solid #eee" }}>
                  {editingChild === child.id ? (
                    <input
                      type="text"
                      value={firstName}
                      onChange={(event) => setFirstName(event.target.value)}
                    />
                  ) : (
                    child.firstName
                  )}
                </td>
                <td style={{ padding: "8px", borderBottom: "1px solid #eee" }}>
                  {editingChild === child.id ? (
                    <input
                      type="date"
                      value={birthDate}
                      onChange={(event) => setBirthDate(event.target.value)}
                    />
                  ) : (
                    child.birthDate ? new Date(child.birthDate).toLocaleDateString() : ""
                  )}
                </td>
                <td style={{ padding: "8px", borderBottom: "1px solid #eee", textAlign: "center" }}>
                  {editingChild === child.id ? (
                    <IconButton aria-label="validate" onClick={() => handleUpdate(child.id)}>
                      <Check />
                    </IconButton>
                  ) : (
                    <>
                      <IconButton aria-label="edit" onClick={() => startEditing(child)}>
                        <Edit />
                      </IconButton>
                      <IconButton aria-label="delete" onClick={() => handleDelete(child.id)}>
                        <Delete />
                      </IconButton>
                    </>
                  )}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
      {error && <p style={{ color: "red" }}>{error}</p>}

      <Dialog open={open} onClose={() => setOpen(false)}>
        <DialogContent>
          <AddChild
            onChildAdded={() => {
              loadChildren();
              setOpen(false);
            }}
          />
        </DialogContent>
      </Dialog>
    </div>
  );
}
