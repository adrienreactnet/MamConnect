// src/components/ChildrenList.jsx
import React, { useEffect, useState } from "react";
import IconButton from "@mui/material/IconButton";
import Dialog from "@mui/material/Dialog";
import DialogContent from "@mui/material/DialogContent";
import { Edit, Delete, Check, Add } from "@mui/icons-material";
import AddChild from "./AddChild";
import { fetchChildren, updateChild, deleteChild } from "../services/childService";
import DataTable from "./DataTable";

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
      {loading ? (
        <p>Chargement...</p>
      ) : (
         <DataTable
          columns={[
            {
              id: "firstName",
              label: "Nom de l'enfant",
              render: (child) =>
                editingChild === child.id ? (
                  <input
                    type="text"
                    value={firstName}
                    onChange={(event) => setFirstName(event.target.value)}
                  />
                ) : (
                  child.firstName
                ),
            },
            {
              id: "birthDate",
              label: "Date de naissance",
              render: (child) =>
                editingChild === child.id ? (
                  <input
                    type="date"
                    value={birthDate}
                    onChange={(event) => setBirthDate(event.target.value)}
                  />
                ) : (
                  child.birthDate ? new Date(child.birthDate).toLocaleDateString() : ""
                ),
            },
            {
              id: "actions",
              label: "Actions",
              align: "center",
              render: (child) =>
                editingChild === child.id ? (
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
                ),
            },
          ]}
          rows={children}
          getRowId={(child) => child.id}
          emptyMessage="Aucun enfant trouvé."
        />
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
