import { Box, Button, CircularProgress, Paper, Tooltip, Typography } from '@mui/material';
import styles from './details.styles';
import Vegetable from '../../../app/images/Vegetable.png';
import Fruit from '../../../app/images/Fruit.png';
import Decorative from '../../../app/images/Decorative.png';
import MemoryIcon from '@mui/icons-material/Memory';
import BatteryChargingFullIcon from '@mui/icons-material/BatteryChargingFull';
import React, { useState } from 'react';
import { Module, Plant, PlantType } from '@arekstasko/plantcare-api-client';
import { useGetBatteryLevelQuery } from '../../../common/RTK/Module/Module';
import { HumidityRange } from './HumidityRange';
import { useGetDistributorQuery } from '../../../common/RTK/Distributor/Distributor';

export type PlantDetailsProps = {
  plant?: Plant;
  module?: Module;
  isLoading: boolean;
};

export const Details = ({ plant, module, isLoading }: PlantDetailsProps) => {
  const { data: batteryLevel, isFetching: isBatteryLevelFetching } = useGetBatteryLevelQuery(
    +module!.id!,
    {
      skip: !modulex
    }
  );

  const { data: distributor, isFetching: isDistributorFetching } = useGetDistributorQuery(
    +plant?.id!,
    {
      skip: !plant
    }
  );

  const [openHumidityRange, setOpenHumidityRange] = useState(false);

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
            src={
              plant.type === PlantType._1
                ? Vegetable
                : plant.type === PlantType._2
                  ? Fruit
                  : Decorative
            }
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
      <Box sx={styles.details_paper}>
        <Tooltip placement="top-end" title="Module ID" arrow>
          <Paper sx={styles.details_card}>
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
      <Box sx={styles.details_paper}>
        <Tooltip placement="top-end" title="Battery Level" arrow>
          <Paper sx={styles.details_card}>
            <BatteryChargingFullIcon
              sx={{
                height: 35,
                width: 35,
                maxHeight: { xs: 35, md: 35 },
                maxWidth: { xs: 35, md: 35 }
              }}
            />
            {isBatteryLevelFetching ? (
              <CircularProgress />
            ) : (
              <Typography variant="h6">{batteryLevel}%</Typography>
            )}
          </Paper>
        </Tooltip>
      </Box>
      <Box sx={styles.details_paper}>
        <Paper sx={styles.details_card}>
          <Tooltip placement="top-end" title="Change humidity range values">
            <Button onClick={() => setOpenHumidityRange(!openHumidityRange)}>Humidity Range</Button>
          </Tooltip>
        </Paper>
      </Box>
      <Box sx={styles.details_paper}>
        <Paper sx={styles.details_card}>
          {isDistributorFetching ? (
            <CircularProgress />
          ) : distributor ? (
            <Tooltip placement="top-end" title="Run hydration">
              <Button onClick={() => console.log('run hydration action')}>Hydrate</Button>
            </Tooltip>
          ) : (
            <Tooltip placement="top-end" title="Add distributor">
              <Button onClick={() => console.log('add distributor action')}>Add distributor</Button>
            </Tooltip>
          )}
        </Paper>
      </Box>
      <HumidityRange
        onOpenChange={(v) => setOpenHumidityRange(v)}
        open={openHumidityRange}
        id={plant.id!}
        min={plant.minHumidity}
        max={plant.maxHumidity}
      />
    </>
  ) : (
    <>
      <CircularProgress />
    </>
  );
};
