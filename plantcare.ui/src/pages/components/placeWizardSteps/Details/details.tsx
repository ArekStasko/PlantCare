import { Typography, Box, TextField } from '@mui/material';
import styles from './details.styles';
import { useFormContext } from 'react-hook-form';
import React from 'react';

export const Details = () => {
  const {
    register,
    formState: { errors }
  } = useFormContext();

  return (
    <Box sx={styles.placeDetailsWrapper}>
      <Typography variant="h6">Enter the name of the place</Typography>
      <TextField
        sx={styles.placeName}
        label="Name"
        id="name"
        error={!!errors.name}
        helperText={errors?.name?.message?.toString()}
        variant="filled"
        {...register('name')}
      />
    </Box>
  );
};

export default Details;
