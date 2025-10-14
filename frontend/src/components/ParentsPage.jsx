// src/components/ParentsPage.jsx
import React, { useEffect, useMemo, useState } from "react";
import IconButton from "@mui/material/IconButton";
import Dialog from "@mui/material/Dialog";
import DialogContent from "@mui/material/DialogContent";
import { Add } from "@mui/icons-material";
import AddParent from "./AddParent";
import { fetchParents } from "../services/parentService";
import { fetchChildren } from "../services/childService";
import DataTable from "./DataTable";

export default function ParentsPage() {
  const [parents, setParents] = useState([]);
  const [children, setChildren] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [open, setOpen] = useState(false);

  const load = async () => {
    setLoading(true);
    try {
      const [p, c] = await Promise.all([fetchParents(), fetchChildren()]);
      setParents(p);
      setChildren(c);
      setError("");
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    load();
  }, []);

  const familyRows = useMemo(() => {
    if (children.length === 0) return [];

    const parentById = new Map(parents.map((p) => [p.id, p]));
    const childToParentIds = new Map();

    parents.forEach((parent) => {
      parent.childrenIds?.forEach((childId) => {
        if (!childToParentIds.has(childId)) childToParentIds.set(childId, new Set());
        childToParentIds.get(childId).add(parent.id);
      });
    });

    const groups = new Map();

    children.forEach((child) => {
      const parentIdsSet = childToParentIds.get(child.id);

      if (!parentIdsSet || parentIdsSet.size === 0) {
        const key = `child-${child.id}-no-parent`;
        if (!groups.has(key)) groups.set(key, { key, parentIds: [], children: [] });
        groups.get(key).children.push(child);
        return;
      }

      const parentIds = Array.from(parentIdsSet).sort((a, b) => String(a).localeCompare(String(b)));
      const key = parentIds.join("|");
      if (!groups.has(key)) groups.set(key, { key, parentIds, children: [] });
      groups.get(key).children.push(child);
    });

    return Array.from(groups.values()).map((group) => {
      const sortedChildren = group.children.sort((a, b) => a.firstName.localeCompare(b.firstName));
      const sortedParents = group.parentIds
        .map((id) => parentById.get(id))
        .filter(Boolean)
        .sort((a, b) =>
          a.lastName === b.lastName
            ? a.firstName.localeCompare(b.firstName)
            : a.lastName.localeCompare(b.lastName)
        );

      return {
        key: group.key,
        childNames: sortedChildren.map((child) => child.firstName),
        parents: sortedParents.map((p) => ({
          id: p.id,
          name: `${p.lastName} ${p.firstName}`,
          phoneNumber: p.phoneNumber || "—",
          email: p.email || "—",
        })),
      };
    });
  }, [children, parents]);

  return (
    <div>
      <h2>Liste des parents</h2>
      <IconButton aria-label="add" onClick={() => setOpen(true)}>
        <Add />
      </IconButton>

      {loading && <p>Chargement...</p>}
      {!loading && (
        <DataTable
          columns={[
            {
              id: "children",
              label: "Enfant",
              render: (row) =>
                row.childNames.map((name) => (
                  <div key={`${row.key}-child-${name}`}>{name}</div>
                )),
            },
            {
              id: "parent",
              label: "Parent",
              render: (row) =>
                row.parents.length > 0
                  ? row.parents.map((parent) => (
                    <div key={`parent-name-${parent.id}`}>{parent.name}</div>
                  ))
                  : "—",
            },
            {
              id: "phone",
              label: "Téléphone",
              render: (row) =>
                row.parents.length > 0
                  ? row.parents.map((parent) => (
                    <div key={`parent-phone-${parent.id}`}>{parent.phoneNumber}</div>
                  ))
                  : "—",
            },
            {
              id: "email",
              label: "Email",
              render: (row) =>
                row.parents.length > 0
                  ? row.parents.map((parent) => (
                    <div key={`parent-email-${parent.id}`}>{parent.email}</div>
                  ))
                  : "—",
            },
          ]}
          rows={familyRows}
          getRowId={(row) => row.key}
          emptyMessage="Aucun parent trouvé."
        />
      )}

      {error && <p style={{ color: "red" }}>{error}</p>}

      <Dialog open={open} onClose={() => setOpen(false)}>
        <DialogContent>
          <AddParent
            onParentAdded={() => {
              load();
              setOpen(false);
            }}
          />
        </DialogContent>
      </Dialog>
    </div>
  );
}
