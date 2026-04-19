import { Box, CircularProgress, Paper, Tooltip, Typography } from '@mui/material';
import styles from '../statisticts/statistics.styles';
import { PlantType } from '../../../common/models/plantTypes';
import Vegetable from '../../../app/images/Vegetable.png';
import Fruit from '../../../app/images/Fruit.png';
import Decorative from '../../../app/images/Decorative.png';
import MemoryIcon from '@mui/icons-material/Memory';
import BatteryChargingFullIcon from '@mui/icons-material/BatteryChargingFull';
import React from 'react';
import { Module, Plant } from '@arekstasko/plantcare-api-client';
import { useGetBatteryLevelQuery } from "../../../common/RTK/Module/Module";

export type PlantDetailsProps = {
  plant?: Plant;
  module?: Module;
  isLoading: boolean;
};

export const Details = ({ plant, module, isLoading }: PlantDetailsProps) => {
  const { data: batteryLevel, isFetching: isBatteryLevelFetching } = useGetBatteryLevelQuery(+module!.id!, {
    skip: !module
  });

  return plant && module && !isLoading ? (
    <>
      <Box sx={styles.plantTitleWrapper}>
        <Typography variant="h4">{plant.name} Details</Typography>
        <Paper sx={styles.typeCard}>
          <Typography variant="h6">
            {plant.type ? PlantType[+plant.type] : 'Plant type not specified'}
          </Typography>
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
      <Box sx={styles.moduleIdWrapper}>
        <Tooltip placement="top-end" title="Module ID" arrow>
          <Paper sx={styles.moduleIdCard}>
            <BatteryChargingFullIcon
              sx={{
                height: 35,
                width: 35,
                maxHeight: { xs: 35, md: 35 },
                maxWidth: { xs: 35, md: 35 }
              }}
            />
            {
              isBatteryLevelFetching ? (
                <CircularProgress />
              ) : (
                <Typography variant="h6">{batteryLevel}%</Typography>
              )
            }
          </Paper>
        </Tooltip>
      </Box>
    </>
  ) : (
    <>
      <CircularProgress />
    </>
  );
};
