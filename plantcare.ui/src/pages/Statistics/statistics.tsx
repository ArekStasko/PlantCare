import { Box, Typography } from '@mui/material';
import React, { useEffect } from 'react';
import {
  GetHumidityMeasurementsApiParameters,
  useGetHumidityMeasurementsQuery
} from '../../common/slices/getHumidityMeasurements/getHumidityMeasurements';

export const Statistics = () => {
  const { data: humidityMeasurements, isLoading: humidityMeasurementsLoading } =
    useGetHumidityMeasurementsQuery({
      moduleId: '4B4A57E0-1F17-40B6-BD7B-851B2293D727',
      fromDate: null,
      toDate: null
    });

  useEffect(() => {
    console.log('HUMIDITY MEASUREMENTS :');
    console.log(humidityMeasurements);
  }, [humidityMeasurementsLoading]);

  return (
    <Box>
      <Typography>Statistics</Typography>
    </Box>
  );
};

export default Statistics;
