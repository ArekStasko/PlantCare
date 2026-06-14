import { Route, Routes } from 'react-router-dom';
import RoutingConstants from './routingConstants';
import React from 'react';
import BaseLayout from '../../common/layouts/baseLayout/baseLayout';
import Dashboard from '../../pages/dashboard/dashboard';
import CreatePlaceWizard from '../../pages/place/PlaceWizard';
import PlantDetails from '../../pages/plantDetails/PlantDetails';
import AuthPage from '../../pages/authPage/authPage';
import PlantWizard from '../../pages/plant/PlantWizard';
import AddModuleWizard from '../../pages/addModule/AddModuleWizard';
import DistributorWizard from '../../pages/distributor/DistributorWizard';

export const MainRouting = () => {
  return (
    <Routes>
      <Route path={RoutingConstants.authBasic} element={<AuthPage />} />
      <Route
        path={RoutingConstants.root}
        element={
          <BaseLayout>
            <Dashboard />
          </BaseLayout>
        }
      />
      <Route
        path={RoutingConstants.plant}
        element={
          <BaseLayout>
            <PlantWizard />
          </BaseLayout>
        }
      />
      <Route
        path={`${RoutingConstants.plantDetails}/:plantId/:moduleId`}
        element={
          <BaseLayout>
            <PlantDetails />
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
        path={RoutingConstants.addModule}
        element={
          <BaseLayout>
            <AddModuleWizard />
          </BaseLayout>
        }
      />
      <Route
        path={`${RoutingConstants.addDistributor}/:plantId/:moduleId`}
        element={
          <BaseLayout>
            <DistributorWizard />
          </BaseLayout>
        }
      />
    </Routes>
  );
};
export default MainRouting;
