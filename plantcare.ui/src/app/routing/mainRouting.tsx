import {Route, Routes} from "react-router-dom";
import RoutingConstants from "./routingConstants";
import {Typography} from "@mui/material";
import React from "react";
import BaseLayout from "../../common/Layouts/baseLayout/baseLayout";

export const MainRouting = () => (
    <Routes>
        <Route path={RoutingConstants.root} element={
            <BaseLayout>
                <Typography>Test</Typography>
            </BaseLayout>
        } />
    </Routes>
);

export default  MainRouting;