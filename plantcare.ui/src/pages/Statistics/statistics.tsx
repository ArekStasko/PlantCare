import { AlertColor, Box, Card, CircularProgress } from '@mui/material';
import React, { useState } from 'react';
import { useGetHumidityMeasurementsQuery } from '../../common/slices/getHumidityMeasurements/getHumidityMeasurements';
import { useParams } from 'react-router';
import DateService from '../../common/services/DateService';
import MeasurementsChart from './components/measurementsChart';
import CustomAlert from '../../common/compontents/customAlert/customAlert';
import styles from './statistics.styles';
import { DatePicker, LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import dayjs, { Dayjs } from 'dayjs';
import dateService from '../../common/services/DateService';

export const Statistics = () => {
  let { moduleId } = useParams();
  const [startOfDay, setStartOfDay] = useState(DateService.getStartOfCurrentDay());
  const [endOfDay, setEndOfDay] = useState(DateService.getEndOfCurrentDay());

  const {
    data: humidityMeasurements,
    isLoading: humidityMeasurementsLoading,
    refetch: refetchMeasurements
  } = useGetHumidityMeasurementsQuery({
    moduleId: moduleId!,
    fromDate: startOfDay,
    toDate: endOfDay
  });

  const refetchMeasurementsWithNewDate = (value: Dayjs) => {
    const correctDate = value.toDate();
    const year = correctDate.getFullYear();
    const month = correctDate.getMonth() + 1;
    const day = correctDate.getDate();

    const fromDate = dateService.getStartOfGivenDay(year, month, day);
    const toDate = dateService.getEndOfGivenDay(year, month, day);

    setStartOfDay(fromDate);
    setEndOfDay(toDate);

    refetchMeasurements();
  };

  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <Box sx={styles.statisticsContainer}>
        <Card variant="outlined" sx={styles.plantDetailsWrapper}></Card>
        {humidityMeasurementsLoading ? (
          <>
            <CircularProgress />
          </>
        ) : (
          <Card variant="outlined" sx={styles.statisticsWrapper}>
            <DatePicker
              label="Measurements Day"
              onAccept={(value) => refetchMeasurementsWithNewDate(value as Dayjs)}
            />
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
    </LocalizationProvider>
  );
};

export default Statistics;
