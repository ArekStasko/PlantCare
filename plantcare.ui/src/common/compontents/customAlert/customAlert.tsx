import { Alert, AlertColor, Typography } from '@mui/material';
import React from 'react';

interface customAlertProps {
  message: string;
  type: AlertColor;
}

export const CustomAlert = (props: customAlertProps) => {
  return (
    <Alert severity={props.type}>
      <Typography>{props.message}</Typography>
    </Alert>
  );
};

export default CustomAlert;
