import { AlertColor, Box, Card, CircularProgress, Paper, Switch, Tooltip, Typography } from "@mui/material";
import React, { useMemo, useState } from "react";
import { useGetHumidityMeasurementsQuery } from '../../common/RTK/getHumidityMeasurements/getHumidityMeasurements';
import { useParams } from 'react-router';
import DateService from '../../common/services/DateService';
import MeasurementsChart from './components/measurementsChart';
import CustomAlert from '../../common/compontents/customAlert/customAlert';
import styles from './statistics.styles';
import { DatePicker, LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { Dayjs } from 'dayjs';
import dateService from '../../common/services/DateService';
import { useGetPlantsQuery } from '../../common/RTK/getPlants/getPlants';
import Vegetable from '../../app/images/Vegetable.png';
import Decorative from '../../app/images/Decorative.png';
import Fruit from '../../app/images/Fruit.png';
import MemoryIcon from '@mui/icons-material/Memory';
import { PlantType } from '../../common/models/plantTypes';
import { useSetModuleStatusMutation } from "../../common/RTK/setModuleStatus/setModuleStatus";
import { useGetModulesQuery } from "../../common/RTK/getModules/getModules";
import { SetModuleStatusRequest } from "../../common/RTK/setModuleStatus/setModuleStatusRequest";
import { PlantDetails } from "../Dashboard/components/PlantDetails";

export const Statistics = () => {
  let { moduleId } = useParams();
  const [startOfDay, setStartOfDay] = useState(DateService.getStartOfCurrentDay());
  const [endOfDay, setEndOfDay] = useState(DateService.getEndOfCurrentDay());

  const { data: plants, isLoading: plantsLoading } = useGetPlantsQuery();
  const {data: modules, isLoading: modulesLoading } = useGetModulesQuery();
  const [setModuleStatus, {isLoading: setModuleLoading}] = useSetModuleStatusMutation();

  const {
    data: humidityMeasurements,
    isLoading: humidityMeasurementsLoading,
    refetch: refetchMeasurements
  } = useGetHumidityMeasurementsQuery({
    moduleId: moduleId!,
    fromDate: startOfDay,
    toDate: endOfDay
  });

  const plant = useMemo(() => plants?.find((p) => p.moduleId?.toString() === moduleId), [plants])
  const module = useMemo(() => modules?.find((m) => m.id?.toString() === moduleId), [modules])

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

  const handleModuleStatusChange = async () => {
    if(!module) return;
    const moduleStatusRequest = {
      moduleId: +module.id,
      status: !module.isMonitoring
    } as SetModuleStatusRequest;
    await setModuleStatus(moduleStatusRequest)
  }

  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <Box sx={styles.statisticsContainer}>
        <Card variant="outlined" sx={styles.plantDetailsWrapper}>
          {setModuleLoading ? (
            <CircularProgress />
          ) : (
            <PlantDetails
              plant={plant}
              module={module}
              onModuleStatusChanged={() => handleModuleStatusChange()}
            />
          )}
        </Card>
        {humidityMeasurementsLoading && plantsLoading && modulesLoading ? (
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
