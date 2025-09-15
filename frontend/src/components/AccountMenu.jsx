import { useState } from "react";
import { IconButton, Menu, MenuItem, Avatar, Tooltip } from "@mui/material";
import { logout } from "../services/authService";

export default function AccountMenu({ auth, setAuth }) {
  const [anchorEl, setAnchorEl] = useState(null);
  const open = Boolean(anchorEl);

  const handleOpen = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleLogout = () => {
    logout();
    setAuth(null);
    handleClose();
    window.location.hash = "#home";
  };

  if (!auth) {
    return (
      <Tooltip title="Connexion">
        <IconButton color="inherit" href="#login">
          <Avatar />
        </IconButton>
      </Tooltip>
    );
  }

  const displayName = `${auth.user.firstName ?? ""} ${auth.user.lastName ?? ""}`.trim();

  return (
    <>
      <Tooltip title={displayName || "Compte"}>
        <IconButton onClick={handleOpen} color="inherit">
          <Avatar>{auth.user.firstName ? auth.user.firstName[0] : ""}</Avatar>
        </IconButton>
      </Tooltip>
      <Menu anchorEl={anchorEl} open={open} onClose={handleClose}>
        <MenuItem onClick={handleLogout}>Déconnexion</MenuItem>
      </Menu>
    </>
  );
}