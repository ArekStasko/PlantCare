import { Box, CircularProgress, InputLabel, MenuItem, Select } from '@mui/material';
import React from 'react';
import { Controller, useFormContext } from 'react-hook-form';
import styles from './moduleSelect.styles';
import { useGetModulesQuery } from '../../../../common/slices/getModules/getModules';
import { Plant } from '../../../../common/models/Plant';

interface ModuleSelectProps {
  plantData?: Plant | undefined;
}

export const ModuleSelect = ({ plantData }: ModuleSelectProps) => {
  const { data: modules, isLoading: modulesLoading } = useGetModulesQuery();

  const { control } = useFormContext();

  return (
    <Box sx={styles.moduleSelectWrapper}>
      <InputLabel id="SelectPlantModule">Choose a place where your plant will be</InputLabel>
      {modulesLoading ? (
        <CircularProgress />
      ) : (
        <Controller
          control={control}
          name="plantModule"
          render={({ field: { onChange, value }, formState: { errors } }) => (
            <Select
              sx={styles.typeSelect}
              onChange={onChange}
              value={value}
              defaultValue={plantData?.placeId ?? ''}
              id="plantModule"
              error={!!errors.plantPlace}
              labelId="SelectPlantPlace">
              {modules!.map((p) => (
                <MenuItem value={p.id}>{p.id}</MenuItem>
              ))}
            </Select>
          )}
        />
      )}
    </Box>
  );
};

export default ModuleSelect;
