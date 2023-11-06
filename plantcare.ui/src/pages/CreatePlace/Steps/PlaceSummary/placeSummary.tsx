import { Typography, Box, CircularProgress } from '@mui/material';
import styles from './placeSummary.styles';
import { useFormContext } from 'react-hook-form';
import React from 'react';
import { ShrinkText } from '../../../../common/services/TextService';
import { PlantType } from '../../../../common/models/plantTypes';

export const PlaceSummary = () => {
  const { getValues } = useFormContext();

  return (
    <Box sx={styles.placeSummaryWrapper}>
      <Typography variant="h6">Plant Summary</Typography>
      <Box sx={styles.details}>
        <Box sx={styles.titleWrapper}>
          <Typography>Name</Typography>
        </Box>
        <Box sx={styles.dataWrapper}>
          <Typography>{getValues('name')}</Typography>
        </Box>
      </Box>
    </Box>
  );
};

export default PlaceSummary;
