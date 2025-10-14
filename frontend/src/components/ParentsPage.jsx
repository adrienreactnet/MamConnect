// src/components/ParentsPage.jsx
import React, { useEffect, useMemo, useState } from "react";
import IconButton from "@mui/material/IconButton";
import Dialog from "@mui/material/Dialog";
import DialogContent from "@mui/material/DialogContent";
import { Add } from "@mui/icons-material";
import AddParent from "./AddParent";
import { fetchParents } from "../services/parentService";
import { fetchChildren } from "../services/childService";

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
      {!loading && familyRows.length === 0 && <p>Aucun parent trouvé.</p>}

      {!loading && familyRows.length > 0 && (
        <table style={{ width: "100%", borderCollapse: "collapse", marginTop: "1rem" }}>
          <thead>
            <tr>
              <th
                style={{
                  textAlign: "left",
                  padding: "8px",
                  borderBottom: "1px solid #ccc",
                  textTransform: "uppercase",
                }}
              >
                Enfant
              </th>
              <th
                style={{
                  textAlign: "left",
                  padding: "8px",
                  borderBottom: "1px solid #ccc",
                  textTransform: "uppercase",
                }}
              >
                Parent
              </th>
              <th
                style={{
                  textAlign: "left",
                  padding: "8px",
                  borderBottom: "1px solid #ccc",
                  textTransform: "uppercase",
                }}
              >
                Téléphone
              </th>
              <th
                style={{
                  textAlign: "left",
                  padding: "8px",
                  borderBottom: "1px solid #ccc",
                  textTransform: "uppercase",
                }}
              >
                Email
              </th>
            </tr>
          </thead>
          <tbody>
            {familyRows.map((row) => (
              <tr key={row.key}>
                <td style={{ padding: "8px", borderBottom: "1px solid #eee", verticalAlign: "top" }}>
                  {row.childNames.map((name) => (
                    <div key={name}>{name}</div>
                  ))}
                </td>
                <td style={{ padding: "8px", borderBottom: "1px solid #eee", verticalAlign: "top" }}>
                  {row.parents.length > 0
                    ? row.parents.map((p) => <div key={`p-name-${p.id}`}>{p.name}</div>)
                    : "—"}
                </td>
                <td style={{ padding: "8px", borderBottom: "1px solid #eee", verticalAlign: "top" }}>
                  {row.parents.length > 0
                    ? row.parents.map((p) => <div key={`p-phone-${p.id}`}>{p.phoneNumber}</div>)
                    : "—"}
                </td>
                <td style={{ padding: "8px", borderBottom: "1px solid #eee", verticalAlign: "top" }}>
                  {row.parents.length > 0
                    ? row.parents.map((p) => <div key={`p-email-${p.id}`}>{p.email}</div>)
                    : "—"}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
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
