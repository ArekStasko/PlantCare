import { Box, CircularProgress, Paper, Switch, Tooltip, Typography } from '@mui/material';
import styles from '../../statistics/statistics.styles';
import { PlantType } from '../../../common/models/plantTypes';
import Vegetable from '../../../app/images/Vegetable.png';
import Fruit from '../../../app/images/Fruit.png';
import Decorative from '../../../app/images/Decorative.png';
import MemoryIcon from '@mui/icons-material/Memory';
import React from 'react';

export type PlantDetailsProps = {
  plant: any;
  moduleStatus: boolean;
  isModuleLoading: boolean;
  onModuleStatusChanged: () => void;
};

export const PlantDetails = ({
  plant,
  moduleStatus,
  isModuleLoading,
  onModuleStatusChanged
}: PlantDetailsProps) => {
  return (
    plant && (
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
        {isModuleLoading ? (
          <CircularProgress />
        ) : (
          <>
            <Box sx={styles.moduleIdWrapper}>
              <Tooltip placement="top-end" title="Module Status" arrow>
                <Paper sx={styles.moduleIdCard}>
                  <Typography variant="h6">Module Status</Typography>
                  <Switch checked={moduleStatus} onChange={onModuleStatusChanged} />
                </Paper>
              </Tooltip>
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
      </>
    )
  );
};
