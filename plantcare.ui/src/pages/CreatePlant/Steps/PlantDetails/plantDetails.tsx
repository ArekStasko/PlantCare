import React from 'react';
import { Box, InputLabel, MenuItem, Select, SelectChangeEvent, TextField } from '@mui/material';
import styles from './plantDetails.styles';
import { PlantType } from '../../../../common/models/plantTypes';
import { useFormContext } from 'react-hook-form';

export const PlantDetails = () => {
  const {
    register,
    formState: { errors },
    setValue
  } = useFormContext();

  const handlePlantTypeChange = (e: SelectChangeEvent<PlantType>) => {
    setValue('plantType', e.target.value);
  };

  return (
    <Box sx={styles.plantDetailsWrapper}>
      <Box sx={styles.nameNtypeWrapper}>
        <Box sx={styles.inputWrapper}>
          <InputLabel id="SelectPlace">Provide your Plant name</InputLabel>
          <TextField
            sx={styles.nameInput}
            label="Name"
            id="name"
            error={!!errors.name}
            variant="filled"
            {...register('name')}
          />
        </Box>
        <Box sx={styles.inputWrapper}>
          <InputLabel id="SelectType">Select Type</InputLabel>
          <Select
            {...register('plantType')}
            sx={styles.typeSelect}
            id="plantType"
            labelId="SelectType">
            <MenuItem value={PlantType.Vegetable}>Vegetable</MenuItem>
            <MenuItem value={PlantType.Fruit}>Fruit</MenuItem>
            <MenuItem value={PlantType.Decorative}>Decorative</MenuItem>
          </Select>
        </Box>
      </Box>
      <Box sx={styles.descriptionWrapper}>
        <InputLabel id="descriptionLabel">Describe your plant</InputLabel>
        <TextField
          sx={styles.descriptionInput}
          error={!!errors.description}
          label="Description"
          id="description"
          multiline
          maxRows={9}
          rows={8}
          {...register('description')}
        />
      </Box>
    </Box>
  );
};

export default PlantDetails;
