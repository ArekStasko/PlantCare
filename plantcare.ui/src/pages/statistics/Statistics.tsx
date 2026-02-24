import { AlertColor, Box, Card, CircularProgress, Typography } from '@mui/material';
import React, { useState } from 'react';
import { useGetHumidityMeasurementsQuery } from '../../common/RTK/getHumidityMeasurements/getHumidityMeasurements';
import { useParams } from 'react-router';
import DateService from '../../common/services/DateService';
import Measurements from './components/Measurements';
import CustomAlert from '../../common/components/customAlert/customAlert';
import styles from './statistics.styles';
import { DatePicker, LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { Dayjs } from 'dayjs';
import dateService from '../../common/services/DateService';
import { Details } from './components/Details';
import { useGetModuleQuery } from '../../common/RTK/getModule/getModule';
import { useGetPlantQuery } from '../../common/RTK/getPlant/getPlant';

export const Statistics = () => {
  let { moduleId, plantId } = useParams();
  const [startOfDay, setStartOfDay] = useState(DateService.getStartOfCurrentDay());
  const [endOfDay, setEndOfDay] = useState(DateService.getEndOfCurrentDay());

  const { data: plant, isFetching: isPlantFetching } = useGetPlantQuery(plantId!, {
    skip: !plantId
  });

  const { data: module, isFetching: isModuleFetching } = useGetModuleQuery(moduleId!, {
    skip: !moduleId
  });

  const {
    data: humidityMeasurements,
    isFetching: isHumidityMeasurementsFetching,
    refetch: refetchHumidityMeasurements
  } = useGetHumidityMeasurementsQuery({
    moduleId: moduleId!,
    fromDate: startOfDay,
    toDate: endOfDay
  });

  const refetchMeasurementsOnDateChange = (value: Dayjs) => {
    const correctDate = value.toDate();
    const year = correctDate.getFullYear();
    const month = correctDate.getMonth() + 1;
    const day = correctDate.getDate();

    const fromDate = dateService.getStartOfGivenDay(year, month, day);
    const toDate = dateService.getEndOfGivenDay(year, month, day);

    setStartOfDay(fromDate);
    setEndOfDay(toDate);

    refetchHumidityMeasurements();
  };

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
        <Card variant="outlined" sx={styles.statisticsWrapper}>
          {isHumidityMeasurementsFetching ? (
            <Box sx={styles.loader}>
              <CircularProgress />
            </Box>
          ) : (
            <>
              <Box sx={styles.datePickerWrapper}>
                <Typography variant="h5">Humidity Moisture Statistics</Typography>
                <DatePicker
                  label="Measurements Day"
                  onAccept={(value) => refetchMeasurementsOnDateChange(value as Dayjs)}
                />
              </Box>
              <Box sx={styles.statisticsChartWrapper}>
                {humidityMeasurements && humidityMeasurements.length === 0 ? (
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
                      <Measurements humidityMeasurements={humidityMeasurements!} />
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
