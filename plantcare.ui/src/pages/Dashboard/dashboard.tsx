import React from 'react';
import { Box, Typography } from '@mui/material';
import { useGetPlacesQuery } from '../../common/slices/getPlaces/getPlaces';
import styles from './dashboard.styles';
import CustomBackdrop from '../../common/compontents/customBackdrop/backdrop';
import PlacesAccordion from './components/PlacesAccordion';
import { useGetPlantsQuery } from '../../common/slices/getPlants/getPlants';
import NoDataDialog from '../../common/compontents/NoDataAlert/noDataDialog';
import { GetUserData } from '../../common/services/CookieService';

const Dashboard = () => {
  const { data: places, isLoading: placesLoading } = useGetPlacesQuery(GetUserData()!.id);
  const { data: plants, isLoading: plantsLoading } = useGetPlantsQuery();

  return placesLoading || plantsLoading ? (
    <CustomBackdrop isLoading={placesLoading || plantsLoading} />
  ) : (
    <>
      {places && places.length !== 0 ? (
        <Box sx={styles.dashboardWrapper}>
          <PlacesAccordion places={places!} plants={plants!} />
        </Box>
      ) : (
        <Box sx={styles.alertWrapper}>
          <NoDataDialog />
        </Box>
      )}
    </>
  );
};

export default Dashboard;
