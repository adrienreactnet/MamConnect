import React from "react";
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

export default function DataTable({ columns, rows, getRowId, emptyMessage = "Aucune donnée disponible." }) {
    const renderRowId = (row, index) => {
        if (typeof getRowId === "function") {
            return getRowId(row, index);
        }
        if (row && (row.id || row.key)) {
            return row.id ?? row.key;
        }
        return index;
    };

    const hasRows = Array.isArray(rows) && rows.length > 0;

    return (
        <StyledTableContainer component={Paper}>
            <Table>
                <TableHead>
                    <TableRow>
                        {columns.map((column) => (
                            <StyledHeadCell key={column.id} align={column.align ?? "left"}>
                                {column.label}
                            </StyledHeadCell>
                        ))}
                    </TableRow>
                </TableHead>
                <TableBody>
                    {hasRows ? (
                        rows.map((row, index) => (
                            <TableRow key={renderRowId(row, index)}>
                                {columns.map((column) => (
                                    <StyledBodyCell key={column.id} align={column.align ?? "left"}>
                                        {typeof column.render === "function" ? column.render(row, index) : row[column.id]}
                                    </StyledBodyCell>
                                ))}
                            </TableRow>
                        ))
                    ) : (
                        <TableRow>
                            <EmptyStateCell colSpan={columns.length}>
                                <Typography variant="body2">{emptyMessage}</Typography>
                            </EmptyStateCell>
                        </TableRow>
                    )}
                </TableBody>
            </Table>
        </StyledTableContainer>
    );
}