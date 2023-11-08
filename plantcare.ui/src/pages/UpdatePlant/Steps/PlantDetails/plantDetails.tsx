import React from 'react';
import { Box, InputLabel, MenuItem, Select, SelectChangeEvent, TextField } from '@mui/material';
import styles from './plantDetails.styles';
import { PlantType } from '../../../../common/models/plantTypes';
import { Controller, useFormContext } from 'react-hook-form';
import { Plant } from '../../../../common/models/Plant';

interface PlantDetailsProps {
  plantData: Plant;
}

export const PlantDetails = ({ plantData }: PlantDetailsProps) => {
  const {
    register,
    formState: { errors },
    control
  } = useFormContext();

  return (
    <Box sx={styles.plantDetailsWrapper}>
      <Box sx={styles.nameNtypeWrapper}>
        <Box sx={styles.inputWrapper}>
          <InputLabel id="SelectPlace">Provide your Plant name</InputLabel>
          <TextField
            sx={styles.nameInput}
            label="Name"
            id="name"
            defaultValue={plantData.name}
            error={!!errors.name}
            helperText={errors?.name?.message?.toString()}
            variant="filled"
            {...register('name')}
          />
        </Box>
        <Box sx={styles.inputWrapper}>
          <InputLabel id="SelectType">Select Type</InputLabel>
          <Controller
            control={control}
            name="plantType"
            render={({ field: { onChange, value }, formState: { errors } }) => (
              <Select
                sx={styles.typeSelect}
                onChange={onChange}
                defaultValue={plantData.type}
                value={value}
                id="plantType"
                error={!!errors.plantType}
                labelId="SelectType">
                <MenuItem value={PlantType.Vegetable}>Vegetable</MenuItem>
                <MenuItem value={PlantType.Fruit}>Fruit</MenuItem>
                <MenuItem value={PlantType.Decorative}>Decorative</MenuItem>
              </Select>
            )}
          />
        </Box>
      </Box>
      <Box sx={styles.descriptionWrapper}>
        <InputLabel id="descriptionLabel">Describe your plant</InputLabel>
        <TextField
          sx={styles.descriptionInput}
          error={!!errors.description}
          defaultValue={plantData.description}
          helperText={errors?.description?.message?.toString()}
          label="Description"
          id="description"
          multiline
          rows={8}
          {...register('description')}
        />
      </Box>
    </Box>
  );
};

export default PlantDetails;
