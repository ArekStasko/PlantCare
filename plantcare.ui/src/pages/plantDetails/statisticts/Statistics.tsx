import { AlertColor, Box, Button, Card, CircularProgress, Typography } from '@mui/material';
import styles from './statistics.styles';
import SyncIcon from '@mui/icons-material/Sync';
import { DatePicker } from '@mui/x-date-pickers';
import { Dayjs } from 'dayjs';
import CustomAlert from '../../../common/components/customAlert/customAlert';
import React, { useState } from 'react';
import { useGetHumidityMeasurementsQuery } from '../../../common/RTK/getHumidityMeasurements/getHumidityMeasurements';
import DateService from '../../../common/services/DateService';
import dateService from '../../../common/services/DateService';
import GeneralHumidityMeasurementsChart from './telemetry/GeneralHumidityMeasurementsChart';

export interface StatisticsProps {
  moduleId: String;
}

const Statistics = ({ moduleId }: StatisticsProps) => {
  const [startOfDay, setStartOfDay] = useState(DateService.getStartOfCurrentDay());
  const [endOfDay, setEndOfDay] = useState(DateService.getEndOfCurrentDay());

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

  return (
    <Card variant="outlined" sx={styles.statisticsWrapper}>
      <>
        <Box sx={styles.measurementsBar}>
          <Typography variant="h5">Humidity Moisture Statistics</Typography>
          <Box sx={styles.measurementsBarActions}>
            <Button onClick={() => refetchHumidityMeasurements()}>
              <SyncIcon />
            </Button>
            <DatePicker
              label="Measurements Day"
              disabled={isHumidityMeasurementsFetching}
              disableFuture
              disableHighlightToday
              onAccept={(value) => refetchMeasurementsOnDateChange(value as Dayjs)}
            />
          </Box>
        </Box>
        {isHumidityMeasurementsFetching ? (
          <Box sx={styles.loader}>
            <CircularProgress />
          </Box>
        ) : (
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
                  <GeneralHumidityMeasurementsChart humidityMeasurements={humidityMeasurements!} />
                </>
              )
            )}
          </Box>
        )}
      </>
    </Card>
  );
};

export default Statistics;
