import React from 'react';
import { Box, InputLabel, MenuItem, Select, TextField, Typography } from '@mui/material';
import styles from './details.styles';
import { Controller, useFormContext } from 'react-hook-form';
import { Plant } from '../../../../common/models/Plant';
import { PlantType } from '../../../../common/models/plantTypes';
import Decorative from '../../../../app/images/Decorative.png';
import Vegetable from '../../../../app/images/Vegetable.png';
import Fruit from '../../../../app/images/Fruit.png';

interface PlantDetailsProps {
  plantData?: Plant | undefined;
}

export const Details = ({ plantData }: PlantDetailsProps) => {
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
            defaultValue={plantData?.name ?? ''}
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
                defaultValue={plantData?.type ?? ''}
                value={value}
                id="plantType"
                error={!!errors.plantType}
                labelId="SelectType"
              >
                <MenuItem value={PlantType.Vegetable}>
                  <Box sx={styles.option}>
                    <Typography>Vegetable</Typography>
                    <Box
                      component="img"
                      sx={{
                        height: 30,
                        width: 30,
                        maxHeight: { xs: 30, md: 30 },
                        maxWidth: { xs: 30, md: 30 },
                        borderRadius: 2
                      }}
                      alt="Plant_Type"
                      src={Vegetable}
                    />
                  </Box>
                </MenuItem>
                <MenuItem value={PlantType.Fruit}>
                  <Box sx={styles.option}>
                    <Typography>Fruit</Typography>
                    <Box
                      component="img"
                      sx={{
                        height: 30,
                        width: 30,
                        maxHeight: { xs: 30, md: 30 },
                        maxWidth: { xs: 30, md: 30 },
                        borderRadius: 2
                      }}
                      alt="Plant_Type"
                      src={Fruit}
                    />
                  </Box>
                </MenuItem>
                <MenuItem value={PlantType.Decorative}>
                  <Box sx={styles.option}>
                    <Typography>Decorative</Typography>
                    <Box
                      component="img"
                      sx={{
                        height: 30,
                        width: 30,
                        maxHeight: { xs: 30, md: 30 },
                        maxWidth: { xs: 30, md: 30 },
                        borderRadius: 2
                      }}
                      alt="Plant_Type"
                      src={Decorative}
                    />
                  </Box>
                </MenuItem>
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
          defaultValue={plantData?.description ?? ''}
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

export default Details;
