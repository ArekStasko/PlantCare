import { AlertColor, Box, CircularProgress, InputLabel, MenuItem, Select } from '@mui/material';
import React from 'react';
import { Controller, useFormContext } from 'react-hook-form';
import styles from './moduleSelect.styles';
import { useGetModulesQuery } from '../../../../common/RTK/getModules/getModules';
import { Plant } from '../../../../common/models/Plant';
import CustomAlert from '../../../../common/components/customAlert/customAlert';

interface ModuleSelectProps {
  plantData?: Plant | undefined;
}

export const ModuleSelect = ({ plantData }: ModuleSelectProps) => {
  const { data: modules, isLoading: modulesLoading } = useGetModulesQuery();

  const { control } = useFormContext();

  return (
    <Box sx={styles.moduleSelectWrapper}>
      {modulesLoading ? (
        <CircularProgress />
      ) : (
        <>
          {modules!.filter((m) => m.plant == null).length == 0 ? (
            <>
              <CustomAlert
                type={'error' as AlertColor}
                message={
                  "You don't have any module without Plant assinged to it. To create new Plant,\n" +
                  '                  please add new module.'
                }
              />
            </>
          ) : (
            <>
              <InputLabel id="SelectPlantModule">
                Choose a module that will monitor your plant moisture
              </InputLabel>
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
                    labelId="SelectPlantPlace"
                  >
                    {modules!
                      .filter((m) => m.plant == null)
                      .map((m) => (
                        <MenuItem value={m.id}>{m.id}</MenuItem>
                      ))}
                  </Select>
                )}
              />
            </>
          )}
        </>
      )}
    </Box>
  );
};

export default ModuleSelect;
