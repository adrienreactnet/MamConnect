import { useEffect, useState } from "react";
import ChildrenList from "./components/ChildrenList";
import DailyReportPage from "./components/DailyReportPage";

function App() {
  const [page, setPage] = useState(
    window.location.hash === "#reports" ? "reports" : "children"
  );

  useEffect(() => {
    const onHashChange = () => {
      setPage(window.location.hash === "#reports" ? "reports" : "children");
    };
    window.addEventListener("hashchange", onHashChange);
    return () => window.removeEventListener("hashchange", onHashChange);
  }, []);

  return (
    <div className="App">
      <h1>MamConnect Front</h1>
      <nav>
        <a href="#children">Enfants</a> | <a href="#reports">Rapports</a>
      </nav>
      {page === "children" && <ChildrenList />}
      {page === "reports" && <DailyReportPage childId={1} />}
    </div>
  );
}

export default App;