import { AlertColor, Box, Card, CircularProgress } from '@mui/material';
import React from 'react';
import { useGetHumidityMeasurementsQuery } from '../../common/slices/getHumidityMeasurements/getHumidityMeasurements';
import { useParams } from 'react-router';
import DateService from '../../common/services/DateService';
import MeasurementsChart from './components/measurementsChart';
import CustomAlert from '../../common/compontents/customAlert/customAlert';
import styles from './statistics.styles';

export const Statistics = () => {
  let { moduleId } = useParams();

  const { data: humidityMeasurements, isLoading: humidityMeasurementsLoading } =
    useGetHumidityMeasurementsQuery({
      moduleId: moduleId!,
      fromDate: DateService.getStartOfCurrentDay(),
      toDate: DateService.getEndOfCurrentDay()
    });

  return (
    <Box sx={styles.statisticsContainer}>
      <Card variant="outlined" sx={styles.plantDetailsWrapper}></Card>
      {humidityMeasurementsLoading ? (
        <>
          <CircularProgress />
        </>
      ) : (
        <Card variant="outlined" sx={styles.statisticsWrapper}>
          {humidityMeasurements!.length == 0 ? (
            <>
              <CustomAlert
                type={'warning' as AlertColor}
                message={
                  "You don't have any registered humidity measurements for this period of time"
                }
              />
            </>
          ) : (
            <>
              <MeasurementsChart humidityMeasurements={humidityMeasurements!} />
            </>
          )}
        </Card>
      )}
    </Box>
  );
};

export default Statistics;
