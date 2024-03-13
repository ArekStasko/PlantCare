import { Typography, Box, Alert } from '@mui/material';
import styles from './deleteSummary.styles';
import React from 'react';

export const DeleteSummary = () => {
  return (
    <Box sx={styles.placeSummaryWrapper}>
      <Typography variant="h6">Delete Plant Summary</Typography>
      <Alert sx={styles.details} variant="outlined" severity="warning">
        <Typography>{`Do you want do delete this Plant ?`}</Typography>
        <Box>
          <Typography>
            By clicking confirm this plant will be deleted permanently. You won't be able to restore
            the deleted resource.
          </Typography>
        </Box>
      </Alert>
    </Box>
  );
};

export default DeleteSummary;
