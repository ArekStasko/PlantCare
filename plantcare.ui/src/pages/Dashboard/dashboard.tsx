import React from 'react';
import { Box, Typography } from '@mui/material';
import { useGetPlacesQuery } from '../../common/RTK/getPlaces/getPlaces';
import styles from './dashboard.styles';
import CustomBackdrop from '../../common/compontents/customBackdrop/backdrop';
import PlacesAccordion from './components/PlacesAccordion';
import { useGetPlantsQuery } from '../../common/RTK/getPlants/getPlants';
import NoDataDialog from '../../common/compontents/NoDataAlert/noDataDialog';

const Dashboard = () => {
  const { data: places, isLoading: placesLoading } = useGetPlacesQuery();
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
