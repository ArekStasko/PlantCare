import { Box, CircularProgress, Typography } from '@mui/material';
import { HumidityMeasurementsChartProps } from './interfaces';
import styles from '../statistics.styles';
import { DatePicker } from '@mui/x-date-pickers';
import React, { useState } from "react";
import { Dayjs } from "dayjs";
import dateService from "../../../../common/services/DateService";

const enum DateType {
  FROM,
  TO,
}

const AverageHumidityMeasurementsChart = ({ moduleId }: HumidityMeasurementsChartProps) => {
  const [fromDate, setFromDate] = useState<string | null>(null);
  const [toDate, setToDate] = useState<string | null>(null);

  const setDateValue = (value: Dayjs, type: DateType) => {
    const correctDate = value.toDate();
    const year = correctDate.getFullYear();
    const month = correctDate.getMonth() + 1;
    const day = correctDate.getDate();

    if(type === DateType.FROM){
      const fromDate = dateService.getStartOfGivenDay(year, month, day);
      setFromDate(fromDate);
      return;
    }

    const toDate = dateService.getEndOfGivenDay(year, month, day);
    setToDate(toDate);
  }

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
            onAccept={(value) => setDateValue(value as Dayjs, DateType.TO)}
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
