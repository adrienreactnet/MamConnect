import { useEffect, useState } from "react";
import ChildrenList from "./components/ChildrenList";
import DailyReportPage from "./components/DailyReportPage";
import HomePage from "./components/HomePage";

function getPageFromHash() {
  switch (window.location.hash) {
    case "#reports":
      return "reports";
    case "#children":
      return "children";
    case "#home":
      return "home";
    default:
      return "home";
  }
}

function App() {
  const [page, setPage] = useState(getPageFromHash());

  useEffect(() => {
    const onHashChange = () => setPage(getPageFromHash());
    window.addEventListener("hashchange", onHashChange);
    return () => window.removeEventListener("hashchange", onHashChange);
  }, []);

  return (
    <div className="App">
      <h1>MamConnect Front</h1>
      <nav>
        <a href="#home">Accueil</a> | <a href="#children">Enfants</a> | <a href="#reports">Rapports</a>
      </nav>
      {page === "home" && <HomePage />}
      {page === "children" && <ChildrenList />}
      {page === "reports" && <DailyReportPage childId={1} />}
    </div>
  );
}

export default App;