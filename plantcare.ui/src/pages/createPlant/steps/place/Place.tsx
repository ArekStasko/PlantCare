import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import { Box, CircularProgress, InputLabel, MenuItem, Select, Typography } from '@mui/material';
import { buttonAction, WizardStepProps } from '../../../../common/wizard/interfaces';
import { CreatePlantContext } from '../../interfaces';
import { useGetPlacesQuery } from '../../../../common/RTK/getPlaces/getPlaces';
import { Controller, useForm } from 'react-hook-form';
import React from 'react';
import { yupResolver } from '@hookform/resolvers/yup';
import validators from '../../../../common/services/Validators';
import styles from './place.styles';

const Place = ({ wizardController }: WizardStepProps<CreatePlantContext>) => {
  const { data: places, isLoading: placesLoading } = useGetPlacesQuery();

  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.selectPlaceSchema),
    defaultValues: {
      place: wizardController.context.place ?? ''
    }
  });

  const {
    formState: { errors, isValid },
    control
  } = methods;

  const nextButton = {
    onClick: () => {
      wizardController.goToNextStep();
    },
    isDisabled: !isValid,
    title: 'Next'
  } as buttonAction;

  const cancelButton = {
    onClick: () => wizardController.onCancel(),
    isDisabled: false,
    title: 'Cancel'
  } as buttonAction;

  const backButton = {
    onClick: () => wizardController.goToPreviousStep(),
    isDisabled: false,
    title: 'Back'
  } as buttonAction;

  return (
    <WizardStep
      nextButton={nextButton}
      cancelButton={cancelButton}
      backButton={backButton}
      title={'Place'}
    >
      <Box sx={styles.placeSelectWrapper}>
        <InputLabel id="SelectPlantPlace">Choose a place where your plant will be</InputLabel>
        {placesLoading ? (
          <CircularProgress />
        ) : (
          <Controller
            control={control}
            name="place"
            render={({ field: { onChange, value }, formState: { errors } }) => (
              <Select
                sx={styles.typeSelect}
                onChange={onChange}
                value={value}
                defaultValue={wizardController.context.place ?? ''}
                id="plantPlace"
                error={!!errors.place}
                labelId="SelectPlantPlace"
              >
                {places!.map((p) => (
                  <MenuItem value={p.id}>{p.name}</MenuItem>
                ))}
              </Select>
            )}
          />
        )}
      </Box>
    </WizardStep>
  );
};

export default Place;
