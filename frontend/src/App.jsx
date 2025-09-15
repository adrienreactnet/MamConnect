import { useEffect, useState } from "react";
import { AppBar, Toolbar, Tabs, Tab, Box, Snackbar } from "@mui/material";
import ChildrenList from "./components/ChildrenList";
import HomePage from "./components/HomePage";
import AddChild from "./components/AddChild";
import ReportsList from "./components/ReportsList";
import AddReports from "./components/AddReports";
import LoginPage from "./components/LoginPage";
import AccountMenu from "./components/AccountMenu";
import AssistantsPage from "./components/AssistantsPage";
import { getAuth } from "./services/authService";

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
    case "assistants":
      return { page: "assistants" };
    default:
      return { page: "home" };
  }
}

function App() {
  const [route, setRoute] = useState(getPageFromHash());
  const [auth, setAuth] = useState(getAuth());
  const [showLogout, setShowLogout] = useState(false);

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
            {auth?.user.role === "Admin" && (
              <Tab label="Assistantes" value="assistants" href="#assistants" />
            )}
            <Tab label="Rapports" value="reports" href="#reports/list" />
          </Tabs>
          <Box ml="auto">
            <AccountMenu auth={auth} setAuth={setAuth} />
          </Box>
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
        {route.page === "login" && <LoginPage setAuth={setAuth} />}
        {route.page === "assistants" && <AssistantsPage />}
      </Box>
      <Snackbar open={showLogout} autoHideDuration={4000} message="Vous êtes déconnecté" onClose={() => setShowLogout(false)} />
    </div>
  );
}

export default App;