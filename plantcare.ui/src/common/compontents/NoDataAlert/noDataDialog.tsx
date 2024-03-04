import { Alert, Typography } from '@mui/material';
import React from 'react';

export const NoDataAlert = () => {
  return (
    <Alert variant="filled" severity="warning">
      <Typography variant="button">You don't have any data</Typography>
      <Typography variant="body2">
        To add some data go to actions tab and select what you want to add
      </Typography>
    </Alert>
  );
};

export default NoDataAlert;
