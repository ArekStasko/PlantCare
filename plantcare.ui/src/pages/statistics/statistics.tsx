import { AlertColor, Box, Card, CircularProgress, Typography } from '@mui/material';
import React, { useMemo, useState } from 'react';
import { useGetHumidityMeasurementsQuery } from '../../common/RTK/getHumidityMeasurements/getHumidityMeasurements';
import { useParams } from 'react-router';
import DateService from '../../common/services/DateService';
import MeasurementsChart from './components/measurementsChart';
import CustomAlert from '../../common/components/customAlert/customAlert';
import styles from './statistics.styles';
import { DatePicker, LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { Dayjs } from 'dayjs';
import dateService from '../../common/services/DateService';
import { useGetModulesQuery } from '../../common/RTK/getModules/getModules';
import { PlantDetails } from '../dashboard/components/PlantDetails';

export const Statistics = () => {
  let { moduleId } = useParams();
  const [isLoading, setIsLoading] = useState(false);
  const [startOfDay, setStartOfDay] = useState(DateService.getStartOfCurrentDay());
  const [endOfDay, setEndOfDay] = useState(DateService.getEndOfCurrentDay());

  const {
    data: modules,
    isLoading: modulesLoading,
  } = useGetModulesQuery();

  const {
    data: humidityMeasurements,
    isLoading: humidityMeasurementsLoading,
    refetch: refetchMeasurements
  } = useGetHumidityMeasurementsQuery({
    moduleId: moduleId!,
    fromDate: startOfDay,
    toDate: endOfDay
  });

  const plant = useMemo(
    () => modules?.find((m) => m.id?.toString() === moduleId)?.plant,
    [modules]
  );

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
        <Card variant="outlined" sx={styles.plantDetailsWrapper}>
          {modulesLoading ? (
            <Box sx={styles.loader}>
              <CircularProgress />
            </Box>
          ) : (
            <PlantDetails
              plant={plant}
              isModuleLoading={isLoading || modulesLoading}
            />
          )}
        </Card>
        <Card variant="outlined" sx={styles.statisticsWrapper}>
          {humidityMeasurementsLoading ? (
            <Box sx={styles.loader}>
              <CircularProgress />
            </Box>
          ) : (
            <>
              <Box sx={styles.datePickerWrapper}>
                <Typography variant="h5">Humidity Moisture Statistics</Typography>
                <DatePicker
                  label="Measurements Day"
                  onAccept={(value) => refetchMeasurementsWithNewDate(value as Dayjs)}
                />
              </Box>
              <Box sx={styles.statisticsChartWrapper}>
                {humidityMeasurements && humidityMeasurements.length == 0 ? (
                  <>
                    <CustomAlert
                      type={'warning' as AlertColor}
                      message={
                        "You don't have any registered humidity measurements for this period of time"
                      }
                    />
                  </>
                ) : (
                  humidityMeasurements && (
                    <>
                      <MeasurementsChart humidityMeasurements={humidityMeasurements!} />
                    </>
                  )
                )}
              </Box>
            </>
          )}
        </Card>
      </Box>
    </LocalizationProvider>
  );
};

export default Statistics;
