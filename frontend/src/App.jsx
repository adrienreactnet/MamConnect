import { useEffect, useState } from "react";
import ChildrenList from "./components/ChildrenList";
import HomePage from "./components/HomePage";
import AddChild from "./components/AddChild";
import ReportsList from "./components/ReportsList";
import AddReports from "./components/AddReports";

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
    default:
      return { page: "home" };
  }
}

function App() {
  const [route, setRoute] = useState(getPageFromHash());

  useEffect(() => {
    const onHashChange = () => setRoute(getPageFromHash());
    window.addEventListener("hashchange", onHashChange);
    return () => window.removeEventListener("hashchange", onHashChange);
  }, []);

  return (
    <div className="App">
      <h1>MamConnect Front</h1>
      <nav>
        <a href="#home">Accueil</a> | <a href="#children/list">Enfants</a> | <a href="#reports/list">Rapports</a>
      </nav>

      {route.page === "children" && (
        <nav>
          <a href="#children/list">Liste</a> | <a href="#children/add">Ajouter</a>
        </nav>
      )}
      {route.page === "reports" && (
        <nav>
         <a href="#reports/list">Liste</a> | <a href="#reports/add">Ajouter</a>
        </nav>
      )}

      {route.page === "home" && <HomePage />}
      {route.page === "children" && route.subPage === "list" && <ChildrenList />}
      {route.page === "children" && route.subPage === "add" && <AddChild />}
      {route.page === "reports" && route.subPage === "list" && <ReportsList />}
      {route.page === "reports" && route.subPage === "add" && <AddReports />}
    </div>
  );
}

export default App;