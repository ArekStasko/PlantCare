import {Route, Routes} from "react-router-dom";
import RoutingConstants from "./routingConstants";
import React from "react";
import BaseLayout from "../../common/Layouts/baseLayout/baseLayout";
import Dashboard from "../../pages/Dashboard/dashboard";
import CreatePlace from "../../pages/CreatePlace/createPlace";
import CreatePlant from "../../pages/CreatePlant/createPlant";
import UpdatePlace from "../../pages/UpdatePlace/updatePlace";
import UpdatePlant from "../../pages/UpdatePlant/updatePlant";
import Statistics from "../../pages/Statistics/statistics";

export const MainRouting = () => (
    <Routes>
        <Route path={RoutingConstants.root} element={
            <BaseLayout>
                <Dashboard />
            </BaseLayout>
        } />
        <Route path={RoutingConstants.createPlant} element={
            <BaseLayout>
                <CreatePlant />
            </BaseLayout>
        } />
        <Route path={RoutingConstants.updatePlant} element={
            <BaseLayout>
                <UpdatePlant />
            </BaseLayout>
        } />
        <Route path={RoutingConstants.plantStatistics} element={
            <BaseLayout>
                <Statistics />
            </BaseLayout>
        } />
        <Route path={RoutingConstants.createPlace} element={
            <BaseLayout>
                <CreatePlace />
            </BaseLayout>
        } />
        <Route path={RoutingConstants.updatePlace} element={
            <BaseLayout>
                <UpdatePlace />
            </BaseLayout>
        } />
    </Routes>
);

export default  MainRouting;