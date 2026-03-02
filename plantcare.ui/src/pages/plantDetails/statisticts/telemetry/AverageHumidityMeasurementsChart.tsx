import { Box, CircularProgress, Typography } from '@mui/material';
import { HumidityMeasurementsChartProps } from './interfaces';
import styles from '../statistics.styles';
import { DatePicker } from '@mui/x-date-pickers';
import React from 'react';

const AverageHumidityMeasurementsChart = ({ moduleId }: HumidityMeasurementsChartProps) => {
  return (
    <>
      <Box sx={styles.measurementsBar}>
        <Typography variant="h6">Average Humidity Moisture</Typography>
        <Box sx={styles.measurementsBarActions}>
          <DatePicker
            label="From Day"
            disabled={false}
            disableFuture
            disableHighlightToday
            onAccept={(value) => console.log('from date accept')}
          />
          <DatePicker
            label="To Day"
            disabled={false}
            disableFuture
            disableHighlightToday
            onAccept={(value) => console.log('to date accept')}
          />
        </Box>
      </Box>
      <Box sx={styles.loader}>
        <CircularProgress />
      </Box>
    </>
  );
};

export default AverageHumidityMeasurementsChart;
