import { Typography, Box, InputLabel, TextField } from '@mui/material';
import styles from './placeDetails.styles';
import { useFormContext } from 'react-hook-form';
import React from 'react';
import { Place } from '../../../../common/models/Place';

export const PlaceDetails = () => {
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

export default PlaceDetails;
