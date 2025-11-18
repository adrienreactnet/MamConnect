import React, { useMemo } from "react";
import Box from "@mui/material/Box";
import CircularProgress from "@mui/material/CircularProgress";
import Paper from "@mui/material/Paper";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Typography from "@mui/material/Typography";
import { styled } from "@mui/material/styles";

const StyledTableContainer = styled(TableContainer)(({ theme }) => ({
    borderRadius: theme.shape.borderRadius,
    boxShadow: theme.shadows[1],
    marginTop: theme.spacing(2),
}));

const StyledHeadCell = styled(TableCell)(({ theme }) => ({
    fontWeight: 600,
    textTransform: "uppercase",
    color: theme.palette.text.secondary,
    backgroundColor: theme.palette.grey[100],
    borderBottom: `1px solid ${theme.palette.divider}`,
    padding: theme.spacing(1.5, 2),
}));

const StyledBodyCell = styled(TableCell)(({ theme }) => ({
    padding: theme.spacing(1.5, 2),
    borderBottom: `1px solid ${theme.palette.divider}`,
}));

const EmptyStateCell = styled(TableCell)(({ theme }) => ({
    padding: theme.spacing(4),
    textAlign: "center",
    color: theme.palette.text.secondary,
}));

export default function DataTable({
    columns,
    rows,
    getRowId,
    emptyMessage = "Aucune donnee disponible.",
    loading = false,
    stickyHeader = false,
    rowActions,
    actionsHeader = "Actions",
}) {
    const safeRows = useMemo(() => (Array.isArray(rows) ? rows : []), [rows]);

    const effectiveColumns = useMemo(() => {
        if (!rowActions) {
            return columns;
        }

        return [
            ...columns,
            {
                id: "__actions",
                label: actionsHeader,
                align: "right",
                isActionColumn: true,
            },
        ];
    }, [actionsHeader, columns, rowActions]);

    const renderRowId = (row, index) => {
        if (typeof getRowId === "function") {
            return getRowId(row, index);
        }
        if (row && (row.id || row.key)) {
            return row.id ?? row.key;
        }
        return index;
    };

    const hasRows = safeRows.length > 0;
    const columnCount = effectiveColumns.length;

    return (
        <StyledTableContainer component={Paper}>
            <Table stickyHeader={stickyHeader}>
                <TableHead>
                    <TableRow>
                        {effectiveColumns.map((column) => (
                            <StyledHeadCell key={column.id} align={column.align ?? "left"}>
                                {column.label}
                            </StyledHeadCell>
                        ))}
                    </TableRow>
                </TableHead>
                <TableBody>
                    {loading ? (
                        <TableRow>
                            <EmptyStateCell colSpan={columnCount}>
                                <Box display="flex" alignItems="center" justifyContent="center" gap={2}>
                                    <CircularProgress size={24} />
                                    <Typography variant="body2">Chargement...</Typography>
                                </Box>
                            </EmptyStateCell>
                        </TableRow>
                    ) : hasRows ? (
                        safeRows.map((row, index) => (
                            <TableRow key={renderRowId(row, index)}>
                                {effectiveColumns.map((column) => (
                                    <StyledBodyCell key={column.id} align={column.align ?? "left"}>
                                        {column.isActionColumn && typeof rowActions === "function"
                                            ? rowActions(row, index)
                                            : typeof column.render === "function"
                                                ? column.render(row, index)
                                                : row[column.id]}
                                    </StyledBodyCell>
                                ))}
                            </TableRow>
                        ))
                    ) : (
                        <TableRow>
                            <EmptyStateCell colSpan={columnCount}>
                                <Typography variant="body2">{emptyMessage}</Typography>
                            </EmptyStateCell>
                        </TableRow>
                    )}
                </TableBody>
            </Table>
        </StyledTableContainer>
    );
}
