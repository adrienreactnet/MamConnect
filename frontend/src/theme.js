import { createTheme } from "@mui/material/styles";

const theme = createTheme({
  palette: {
    primary: { main: "#1976d2" },
    secondary: { main: "#dc004e" },
    text: {
      primary: "#000000",   // 👈 noir pour le texte principal
      secondary: "#555555", // 👈 gris pour le texte secondaire (optionnel)
    },
  },
  typography: {
    fontFamily: "'Roboto', sans-serif",
  },
});

export default theme;
