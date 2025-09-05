// src/components/DailyReportPage.jsx
import React, { useState } from "react";
import DailyReportList from "./DailyReportList";
import DailyReportForm from "./DailyReportForm";

export default function DailyReportPage({ childId }) {
    const [refresh, setRefresh] = useState(0);

    const handleCreated = () => setRefresh((r) => r + 1);

    return (
        <div>
            <DailyReportForm childId={childId} onReportCreated={handleCreated} />
            <DailyReportList childId={childId} refresh={refresh} />
        </div>
    );
}