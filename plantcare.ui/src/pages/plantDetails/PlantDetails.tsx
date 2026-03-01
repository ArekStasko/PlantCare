import { Box, Card, CircularProgress, Typography } from '@mui/material';
import React from 'react';
import { useParams } from 'react-router';
import styles from './statisticts/statistics.styles';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { Details } from './components/Details';
import { useGetModuleQuery } from '../../common/RTK/getModule/getModule';
import { useGetPlantQuery } from '../../common/RTK/getPlant/getPlant';
import Statistics from './statisticts/Statistics';

export const PlantDetails = () => {
  let { moduleId, plantId } = useParams();

  const { data: plant, isFetching: isPlantFetching } = useGetPlantQuery(plantId!, {
    skip: !plantId
  });

  const { data: module, isFetching: isModuleFetching } = useGetModuleQuery(moduleId!, {
    skip: !moduleId
  });

  if (!plantId || !moduleId) {
    return (
      <Box>
        <Typography>Here will be common error page</Typography>
      </Box>
    );
  }

  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <Box sx={styles.statisticsContainer}>
        <Card variant="outlined" sx={styles.plantDetailsWrapper}>
          {isPlantFetching || isModuleFetching ? (
            <Box sx={styles.loader}>
              <CircularProgress />
            </Box>
          ) : (
            <Details
              plant={plant}
              module={module}
              isLoading={isPlantFetching || isModuleFetching}
            />
          )}
        </Card>
        <Statistics moduleId={moduleId} />
      </Box>
    </LocalizationProvider>
  );
};

export default PlantDetails;
