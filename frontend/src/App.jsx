import { useEffect, useMemo, useState } from "react";
import { AppBar, Toolbar, Tabs, Tab, Box, Snackbar } from "@mui/material";
import ChildrenList from "./components/ChildrenList";
import ReportsList from "./components/ReportsList";
import LoginPage from "./components/LoginPage";
import AccountMenu from "./components/AccountMenu";
import AssistantsPage from "./components/AssistantsPage";
import AssignChildren from "./components/AssignChildren";
import ParentsPage from "./components/ParentsPage";
import ChildrenRelationsPage from "./components/ChildrenRelationsPage";
import VaccinesPage from "./components/VaccinesPage";
import VaccinationOverviewPage from "./components/VaccinationOverviewPage";
import { getAuth } from "./services/authService";
import { LOGOUT_EVENT_NAME } from "./services/authEvents";

const LOGIN_PAGE = "login";

const NAV_ITEMS = [
  { page: "children", label: "Enfants", component: ChildrenList },
  { page: "reports", label: "Rapports", component: ReportsList },
  { page: "assistants", label: "Assistantes", component: AssistantsPage, roles: ["Admin"] },
  { page: "parents", label: "Parents", component: ParentsPage, roles: ["Admin"] },
  { page: "assign", label: "Affectations", component: AssignChildren, roles: ["Admin"] },
  { page: "relations", label: "Relations", component: ChildrenRelationsPage, roles: ["Admin"] },
  { page: "vaccines", label: "Calendrier vaccinal", component: VaccinesPage, roles: ["Admin"] },
  { page: "vaccinations", label: "Vaccinations", component: VaccinationOverviewPage, roles: ["Admin"] },
];

function getAccessibleNavItems(role) {
  return NAV_ITEMS.filter((item) => !item.roles || item.roles.includes(role));
}

function getPageFromHash(auth) {
  const hash = window.location.hash.slice(1);
  const [page] = hash.split("/");

  if (!auth) {
    return { page: LOGIN_PAGE };
  }

  if (page === LOGIN_PAGE) {
    return { page: LOGIN_PAGE };
  }

  const role = auth?.user?.role;
  const accessible = getAccessibleNavItems(role);
  if (accessible.some((item) => item.page === page)) {
    return { page };
  }

  const fallbackPage = accessible[0]?.page ?? LOGIN_PAGE;
  return { page: fallbackPage };
}

function App() {
  const [auth, setAuth] = useState(getAuth());
  const [route, setRoute] = useState(() => getPageFromHash(getAuth()));
  const [showLogout, setShowLogout] = useState(false);

  useEffect(() => {
    if (auth === null) {
      window.location.hash = `#${LOGIN_PAGE}`;
    }
  }, [auth]);

  useEffect(() => {
    const syncRouteWithHash = () => setRoute(getPageFromHash(auth));
    window.addEventListener("hashchange", syncRouteWithHash);
    syncRouteWithHash();
    return () => window.removeEventListener("hashchange", syncRouteWithHash);
  }, [auth]);

  // Quand mon appli démarre, je me mets à l’écoute d’un événement global LOGOUT_EVENT_NAME. Si quelqu’un déclenche cet événement n’importe où dans l’app, je vide l’utilisateur (setAuth(null)) et j’affiche un message (‘Vous êtes déconnecté’). Quand je quitte l’app, j’arrête d’écouter cet événement.
  useEffect(() => {
    const handleLogoutEvent = () => {
      setAuth(null);
      setShowLogout(true);
    };

    window.addEventListener(LOGOUT_EVENT_NAME, handleLogoutEvent);
    return () => window.removeEventListener(LOGOUT_EVENT_NAME, handleLogoutEvent);
  }, []);

  useEffect(() => {
    if (!auth) {
      return;
    }

    const accessible = getAccessibleNavItems(auth.user.role);
    const isRouteAccessible = accessible.some((item) => item.page === route.page);
    if (route.page === LOGIN_PAGE || !isRouteAccessible) {
      const targetPage = accessible[0]?.page ?? LOGIN_PAGE;
      if (targetPage && window.location.hash !== `#${targetPage}`) {
        window.location.hash = `#${targetPage}`;
      }
    }
  }, [auth, route.page]);

  const accessibleNavItems = useMemo(
    () => (auth ? getAccessibleNavItems(auth.user.role) : []),
    [auth]
  );
  const ActiveView = useMemo(() => NAV_ITEMS.find((item) => item.page === route.page)?.component, [route.page]);
  const isLoginPage = route.page === LOGIN_PAGE;

  return (
    <div className="App">
      <h1>MamConnect Front</h1>
      <AppBar position="static">
        <Toolbar>
          {auth && !isLoginPage && (
            <Tabs value={route.page} textColor="inherit">
              {accessibleNavItems.map((item) => (
                <Tab
                  key={item.page}
                  label={item.label}
                  value={item.page}
                  href={`#${item.page}`}
                />
              ))}
            </Tabs>
          )}
          <Box ml="auto">
            <AccountMenu auth={auth} setAuth={setAuth} />
          </Box>
        </Toolbar>
      </AppBar>

      <Box>
        {isLoginPage && <LoginPage setAuth={setAuth} />}
        {!isLoginPage && ActiveView && auth && <ActiveView />}
      </Box>
      <Snackbar open={showLogout} autoHideDuration={4000} message="Vous êtes déconnecté" onClose={() => setShowLogout(false)} />
    </div>
  );
}

export default App;
