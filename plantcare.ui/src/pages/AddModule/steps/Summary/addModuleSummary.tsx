import { Typography, Box } from '@mui/material';
import styles from './addModuleSummary.styles';
import React from 'react';

export const AddModuleSummary = () => {
  return (
    <Box sx={styles.addModuleSummaryWrapper}>
      <Typography variant="h6">Module Summary</Typography>
    </Box>
  );
};

export default AddModuleSummary;
