import { useEffect, useState } from "react";
import { AppBar, Toolbar, Tabs, Tab, Box, Snackbar } from "@mui/material";
import ChildrenList from "./components/ChildrenList";
import AddChild from "./components/AddChild";
import ReportsList from "./components/ReportsList";
import AddReports from "./components/AddReports";
import LoginPage from "./components/LoginPage";
import AccountMenu from "./components/AccountMenu";
import AssistantsPage from "./components/AssistantsPage";
import AssignChildren from "./components/AssignChildren";
import ParentsPage from "./components/ParentsPage";
import ChildrenRelationsPage from "./components/ChildrenRelationsPage";
import { getAuth } from "./services/authService";
import { LOGOUT_EVENT_NAME } from "./services/authEvents";

function getPageFromHash() {
  const hash = window.location.hash.slice(1); // Remove leading '#'
  const [page, subPage] = hash.split("/");

  switch (page) {
    case "children":
      return { page: "children", subPage: subPage || "list" };
    case "reports":
      return { page: "reports", subPage: subPage || "list" };
    case "login":
      return { page: "login" };
    case "assistants":
      return { page: "assistants" };
    case "parents":
      return { page: "parents" };
    case "assign":
      return { page: "assign" };
    case "relations":
      return { page: "relations" };
    default:
      return { page: "login" };
  }
}

function App() {
  const [route, setRoute] = useState(getPageFromHash());
  const [auth, setAuth] = useState(getAuth());
  const [showLogout, setShowLogout] = useState(false);

  useEffect(() => {
    if (auth === null) {
      window.location.hash = "#login";
    }
  }, [auth]);

  useEffect(() => {
    const onHashChange = () => setRoute(getPageFromHash());
    window.addEventListener("hashchange", onHashChange);
    return () => window.removeEventListener("hashchange", onHashChange);
  }, []);

  // Quand mon appli démarre, je me mets à l’écoute d’un événement global LOGOUT_EVENT_NAME. Si quelqu’un déclenche cet événement n’importe où dans l’app, je vide l’utilisateur (setAuth(null)) et j’affiche un message (‘Vous êtes déconnecté’). Quand je quitte l’app, j’arrête d’écouter cet événement.
  useEffect(() => {
    const handleLogoutEvent = () => {
      setAuth(null);
      setShowLogout(true);
    };

    window.addEventListener(LOGOUT_EVENT_NAME, handleLogoutEvent);
    return () => window.removeEventListener(LOGOUT_EVENT_NAME, handleLogoutEvent);
  }, []);

  return (
    <div className="App">
      <h1>MamConnect Front</h1>
      <AppBar position="static">
        <Toolbar>
          {auth && (
            <Tabs
              value={route.page}
              textColor="inherit"
            >
              <Tab label="Enfants" value="children" href="#children/list" />
              {auth?.user.role === "Admin" && (
                <Tab label="Assistantes" value="assistants" href="#assistants" />
              )}
              {auth?.user.role === "Admin" && (
                <Tab label="Parents" value="parents" href="#parents" />
              )}
              {auth?.user.role === "Admin" && (
                <Tab label="Affectations" value="assign" href="#assign" />
              )}
              {auth?.user.role === "Admin" && (
                <Tab label="Relations" value="relations" href="#relations" />
              )}
              <Tab label="Rapports" value="reports" href="#reports/list" />
            </Tabs>
          )}
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

        {route.page === "children" && route.subPage === "list" && <ChildrenList />}
        {route.page === "children" && route.subPage === "add" && <AddChild />}
        {route.page === "reports" && route.subPage === "list" && <ReportsList />}
        {route.page === "reports" && route.subPage === "add" && <AddReports />}
        {route.page === "login" && <LoginPage setAuth={setAuth} />}
        {route.page === "assistants" && auth?.user.role === "Admin" && <AssistantsPage />}
        {route.page === "parents" && auth?.user.role === "Admin" && <ParentsPage />}
        {route.page === "assign" && auth?.user.role === "Admin" && <AssignChildren />}
        {route.page === "relations" && auth?.user.role === "Admin" && <ChildrenRelationsPage />}
      </Box>
      <Snackbar open={showLogout} autoHideDuration={4000} message="Vous êtes déconnecté" onClose={() => setShowLogout(false)} />
    </div>
  );
}

export default App;