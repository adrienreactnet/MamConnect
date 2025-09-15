import { useEffect, useState } from "react";
import { AppBar, Toolbar, Tabs, Tab, Box } from "@mui/material";
import ChildrenList from "./components/ChildrenList";
import HomePage from "./components/HomePage";
import AddChild from "./components/AddChild";
import ReportsList from "./components/ReportsList";
import AddReports from "./components/AddReports";
import LoginPage from "./components/LoginPage";
import AdminPage from "./components/AdminPage";
import { getAuth, logout } from "./services/authService";

function getPageFromHash() {
  const hash = window.location.hash.slice(1); // Remove leading '#'
  const [page, subPage] = hash.split("/");

  switch (page) {
    case "children":
      return { page: "children", subPage: subPage || "list" };
    case "reports":
      return { page: "reports", subPage: subPage || "list" };
    case "home":
      return { page: "home" };
    case "login":
      return { page: "login" };
    case "admin":
      return { page: "admin" };
    default:
      return { page: "home" };
  }
}

function App() {
  const [route, setRoute] = useState(getPageFromHash());
  const auth = getAuth();

  useEffect(() => {
    const onHashChange = () => setRoute(getPageFromHash());
    window.addEventListener("hashchange", onHashChange);
    return () => window.removeEventListener("hashchange", onHashChange);
  }, []);

  return (
    <div className="App">
      <h1>MamConnect Front</h1>
      <AppBar position="static">
        <Toolbar>
          <Tabs
            value={route.page}
            textColor="inherit"
          >
            <Tab label="Accueil" value="home" href="#home" />
            <Tab label="Enfants" value="children" href="#children/list" />
            <Tab label="Rapports" value="reports" href="#reports/list" />
            {auth?.user.role === "Admin" && <Tab label="Admin" value="admin" href="#admin" />}
            {auth === null ? (
              <Tab label="Connexion" value="login" href="#login" />
            ) : (
              <Tab
                label="Déconnexion"
                value="logout"
                onClick={() => {
                  logout();
                  window.location.hash = "#home";
                }}
              />
            )}
          </Tabs>
        </Toolbar>
      </AppBar>

      {route.page === "children" && (
        <Tabs value={route.subPage}>
          <Tab label="Liste" value="list" href="#children/list" />
          <Tab label="Ajouter" value="add" href="#children/add" />
        </Tabs>
      )}
      {route.page === "reports" && (
        <Tabs value={route.subPage}>
          <Tab label="Liste" value="list" href="#reports/list" />
          <Tab label="Ajouter" value="add" href="#reports/add" />
        </Tabs>
      )}

      <Box>

        {route.page === "home" && <HomePage />}
        {route.page === "children" && route.subPage === "list" && <ChildrenList />}
        {route.page === "children" && route.subPage === "add" && <AddChild />}
        {route.page === "reports" && route.subPage === "list" && <ReportsList />}
        {route.page === "reports" && route.subPage === "add" && <AddReports />}
        {route.page === "login" && <LoginPage />}
        {route.page === "admin" && <AdminPage />}
      </Box>
    </div>
  );
}

export default App;