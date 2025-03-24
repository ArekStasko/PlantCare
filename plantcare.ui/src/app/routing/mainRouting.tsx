import { Route, Routes } from 'react-router-dom';
import RoutingConstants from './routingConstants';
import React from 'react';
import BaseLayout from '../../common/Layouts/baseLayout/baseLayout';
import Dashboard from '../../pages/Dashboard/dashboard';
import CreatePlaceWizard from '../../pages/createPlace/CreatePlaceWizard';
import UpdatePlace from '../../pages/UpdatePlace/updatePlace';
import UpdatePlant from '../../pages/UpdatePlant/updatePlant';
import Statistics from '../../pages/Statistics/statistics';
import AuthPage from '../../pages/authPage/authPage';
import AddModule from '../../pages/AddModule/addModule';
import CreatePlantWizard from "../../pages/createPlant/CreatePlantWizard";

export const MainRouting = () => {
  return (
    <Routes>
      <Route path={RoutingConstants.auth} element={<AuthPage />} />
      <Route
        path={RoutingConstants.root}
        element={
          <BaseLayout>
            <Dashboard />
          </BaseLayout>
        }
      />
      <Route
        path={RoutingConstants.createPlant}
        element={
          <BaseLayout>
            <CreatePlantWizard />
          </BaseLayout>
        }
      />
      <Route
        path={`${RoutingConstants.updatePlant}/:id`}
        element={
          <BaseLayout>
            <UpdatePlant />
          </BaseLayout>
        }
      />
      <Route
        path={`${RoutingConstants.plantStatistics}/:moduleId`}
        element={
          <BaseLayout>
            <Statistics />
          </BaseLayout>
        }
      />
      <Route
        path={RoutingConstants.createPlace}
        element={
          <BaseLayout>
            <CreatePlaceWizard />
          </BaseLayout>
        }
      />
      <Route
        path={`${RoutingConstants.updatePlace}/:id`}
        element={
          <BaseLayout>
            <UpdatePlace />
          </BaseLayout>
        }
      />
      <Route
        path={RoutingConstants.addModule}
        element={
          <BaseLayout>
            <AddModule />
          </BaseLayout>
        }
      />
    </Routes>
  );
};
export default MainRouting;
