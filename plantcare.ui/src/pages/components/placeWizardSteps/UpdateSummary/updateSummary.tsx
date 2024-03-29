import { Typography, Box } from '@mui/material';
import { useFormContext } from 'react-hook-form';
import styles from './updateSummary.styles';
import React from 'react';

export const UpdateSummary = () => {
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

export default UpdateSummary;
