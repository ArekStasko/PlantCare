import { Alert, AlertColor, SxProps, Typography } from '@mui/material';
import React from 'react';

interface customAlertProps {
  message: string;
  type: AlertColor;
  sx?: SxProps;
}

export const CustomAlert = (props: customAlertProps) => {
  return (
    <Alert sx={props.sx} variant="outlined" severity={props.type}>
      <Typography>{props.message}</Typography>
    </Alert>
  );
};

export default CustomAlert;
