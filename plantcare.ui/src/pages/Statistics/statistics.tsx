import { AlertColor, Box, Card, CircularProgress, Paper, Tooltip, Typography } from '@mui/material';
import React, { useState } from 'react';
import { useGetHumidityMeasurementsQuery } from '../../common/slices/getHumidityMeasurements/getHumidityMeasurements';
import { useParams } from 'react-router';
import DateService from '../../common/services/DateService';
import MeasurementsChart from './components/measurementsChart';
import CustomAlert from '../../common/compontents/customAlert/customAlert';
import styles from './statistics.styles';
import { DatePicker, LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { Dayjs } from 'dayjs';
import dateService from '../../common/services/DateService';
import { useGetPlantsQuery } from '../../common/slices/getPlants/getPlants';
import Vegetable from '../../app/images/Vegetable.png';
import Decorative from '../../app/images/Decorative.png';
import Fruit from '../../app/images/Fruit.png';
import MemoryIcon from '@mui/icons-material/Memory';
import { PlantType } from '../../common/models/plantTypes';

export const Statistics = () => {
  let { moduleId } = useParams();
  const [startOfDay, setStartOfDay] = useState(DateService.getStartOfCurrentDay());
  const [endOfDay, setEndOfDay] = useState(DateService.getEndOfCurrentDay());

  const { data: plants, isLoading: plantsLoading } = useGetPlantsQuery();

  const {
    data: humidityMeasurements,
    isLoading: humidityMeasurementsLoading,
    refetch: refetchMeasurements
  } = useGetHumidityMeasurementsQuery({
    moduleId: moduleId!,
    fromDate: startOfDay,
    toDate: endOfDay
  });

  const plant = plants && plants.find((p) => p.moduleId === moduleId);

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
          {plant && (
            <>
              <Box sx={styles.plantTitleWrapper}>
                <Typography variant="h4">{plant.name} Details</Typography>
                <Paper sx={styles.typeCard}>
                  <Typography variant="h6">{PlantType[+plant.type]}</Typography>
                  <Box
                    component="img"
                    sx={{
                      height: 35,
                      width: 35,
                      maxHeight: { xs: 35, md: 35 },
                      maxWidth: { xs: 35, md: 35 },
                      borderRadius: 2
                    }}
                    alt="Plant_Type"
                    src={plant.type === 0 ? Vegetable : plant.type === 1 ? Fruit : Decorative}
                  />
                </Paper>
              </Box>
              <Box sx={styles.plantDescriptionWrapper}>
                <Paper sx={styles.titleCard}>
                  <Typography sx={{ ml: 5 }} variant="h5">
                    {plant.name}
                  </Typography>
                </Paper>
                <Paper sx={styles.descriptionCard}>
                  <Typography>{plant.description}</Typography>
                </Paper>
              </Box>
              <Box sx={styles.moduleIdWrapper}>
                <Tooltip placement="top-end" title="Module ID" arrow>
                  <Paper sx={styles.moduleIdCard}>
                    <MemoryIcon
                      sx={{
                        height: 35,
                        width: 35,
                        maxHeight: { xs: 35, md: 35 },
                        maxWidth: { xs: 35, md: 35 }
                      }}
                    />
                    <Typography variant="h6">{plant.moduleId}</Typography>
                  </Paper>
                </Tooltip>
              </Box>
            </>
          )}
        </Card>
        {humidityMeasurementsLoading && plantsLoading ? (
          <>
            <CircularProgress />
          </>
        ) : (
          <Card variant="outlined" sx={styles.statisticsWrapper}>
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
          </Card>
        )}
      </Box>
    </LocalizationProvider>
  );
};

export default Statistics;
