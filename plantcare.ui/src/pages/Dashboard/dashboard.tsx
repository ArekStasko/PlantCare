import React from 'react';
import { Box, Typography } from '@mui/material';
import { useGetPlacesQuery } from '../../common/slices/getPlaces/getPlaces';
import styles from './dashboard.styles';
import CustomBackdrop from '../../common/compontents/customBackdrop/backdrop';
import PlacesAccordion from './components/PlacesAccordion';
import { useGetPlantsQuery } from '../../common/slices/getPlants/getPlants';

const Dashboard = () => {
  const { data: places, isLoading: placesLoading } = useGetPlacesQuery();
  const { data: plants, isLoading: plantsLoading } = useGetPlantsQuery();

  return placesLoading || plantsLoading ? (
    <CustomBackdrop isLoading={placesLoading || plantsLoading} />
  ) : (
    <Box sx={styles.dashboardWrapper}>
      {places ? (
        <PlacesAccordion places={places!} plants={plants!} />
      ) : (
        <Box>
          <Typography>you dont have any data</Typography>
        </Box>
      )}
    </Box>
  );
};

export default Dashboard;
