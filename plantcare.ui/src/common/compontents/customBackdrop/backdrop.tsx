import { Backdrop, CircularProgress } from '@mui/material';
import React from 'react';

interface CustomBackdropProps {
  isLoading: boolean;
}

export const CustomBackdrop = (props: CustomBackdropProps) => {
  return (
    <Backdrop
      sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
      open={props.isLoading}
    >
      <CircularProgress color="inherit" />
    </Backdrop>
  );
};

export default CustomBackdrop;
