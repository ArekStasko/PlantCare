import { Box, CircularProgress, InputLabel, MenuItem, Select, Typography } from '@mui/material';
import React from 'react';
import { Controller, useFormContext } from 'react-hook-form';
import { useGetPlacesQuery } from '../../../../common/slices/getPlaces/getPlaces';
import styles from './placeSelect.styles';

export const PlaceSelect = () => {
  const { data: places, isLoading: placesLoading } = useGetPlacesQuery();

  const {
    formState: { errors },
    control
  } = useFormContext();

  return (
    <Box sx={styles.placeSelectWrapper}>
      <InputLabel id="SelectPlantPlace">Choose a place where your plant will be</InputLabel>
      {placesLoading ? (
        <CircularProgress />
      ) : (
        <Controller
          control={control}
          name="plantPlace"
          render={({ field: { onChange, value }, formState: { errors } }) => (
            <Select
              sx={styles.typeSelect}
              onChange={onChange}
              value={value}
              id="plantPlace"
              error={!!errors.plantPlace}
              labelId="SelectPlantPlace">
              {places!.map((p) => (
                <MenuItem value={p.id}>{p.name}</MenuItem>
              ))}
            </Select>
          )}
        />
      )}
    </Box>
  );
};

export default PlaceSelect;
