import { Alert, Box, CircularProgress, Typography } from '@mui/material';
import { HumidityMeasurementsChartProps } from './interfaces';
import styles from '../statistics.styles';
import { DatePicker } from '@mui/x-date-pickers';
import React, { useEffect, useState } from 'react';
import dayjs, { Dayjs } from 'dayjs';
import dateService from '../../../../common/services/DateService';
import CustomAlert from '../../../../common/components/customAlert/customAlert';
import { BarChart } from '@mui/x-charts';
import StatisticsService from '../../../../common/services/StatisticsService';
import { useGetAverageHumidityMeasurementsQuery } from '../../../../common/RTK/HumidityMeasurement/HumidityMeasurement';

const enum DateType {
  FROM,
  TO
}

const AverageHumidityMeasurementsChart = ({ moduleId }: HumidityMeasurementsChartProps) => {
  const [fromDate, setFromDate] = useState<string | null>(null);
  const [toDate, setToDate] = useState<string | null>(null);

  const {
    data: averageMeasurements,
    isFetching: isAverageMeasurementsFetching,
    isError: averageMeasurementsError
  } = useGetAverageHumidityMeasurementsQuery(
    {
      moduleId: moduleId!,
      fromDate: fromDate,
      toDate: toDate
    },
    { skip: fromDate === null || toDate === null || Date.parse(fromDate!) > Date.parse(toDate!) }
  );

  const setDateValue = (value: Dayjs, type: DateType) => {
    const correctDate = value.toDate();
    const year = correctDate.getFullYear();
    const month = correctDate.getMonth() + 1;
    const day = correctDate.getDate();

    if (type === DateType.FROM) {
      const fromDate = dateService.getStartOfGivenDay(year, month, day);
      setFromDate(fromDate);
      return;
    }

    const toDate = dateService.getEndOfGivenDay(year, month, day);
    setToDate(toDate);
  };

  const showInfoAlert =
    averageMeasurements === undefined &&
    !averageMeasurementsError &&
    !isAverageMeasurementsFetching;
  const showWarningAlert =
    averageMeasurements !== undefined &&
    averageMeasurements.length === 0 &&
    !averageMeasurementsError &&
    !isAverageMeasurementsFetching;

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
            onAccept={(value) => setDateValue(value as Dayjs, DateType.FROM)}
          />
          <DatePicker
            label="To Day"
            disabled={!fromDate}
            disableFuture
            disableHighlightToday
            minDate={dayjs(fromDate)}
            onAccept={(value) => setDateValue(value as Dayjs, DateType.TO)}
          />
        </Box>
      </Box>
      <Box sx={styles.measurementsBody}>
        {averageMeasurements && averageMeasurements.length > 0 && (
          <BarChart
            xAxis={[
              {
                scaleType: 'band',
                data: StatisticsService.getAverageHumidityMeasurementTime(averageMeasurements)
              }
            ]}
            series={[
              {
                data: StatisticsService.getAverageHumidityMeasurementValues(averageMeasurements),
                valueFormatter: (v) => `Average Humidity: ${v}%`
              }
            ]}
          />
        )}
        {isAverageMeasurementsFetching && (
          <Box sx={styles.loader}>
            <CircularProgress />
          </Box>
        )}
        {averageMeasurementsError && (
          <Box sx={styles.loader}>
            <CustomAlert type="error" message="Something went wrong. Please try again later." />
          </Box>
        )}
        {showInfoAlert && (
          <Box sx={styles.loader}>
            <CustomAlert
              type="info"
              message="Please select the date range to get average humidity measurements"
            />
          </Box>
        )}
        {showWarningAlert && (
          <Box sx={styles.loader}>
            <CustomAlert
              type="warning"
              message="You don't have measurements for this period of time"
            />
          </Box>
        )}
      </Box>
    </>
  );
};

export default AverageHumidityMeasurementsChart;
