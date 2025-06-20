import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import { Box, CircularProgress, InputLabel, MenuItem, Select, Typography } from '@mui/material';
import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { PlantContext } from '../../interfaces';
import { useGetPlacesQuery } from '../../../../common/RTK/getPlaces/getPlaces';
import { Controller, useForm } from 'react-hook-form';
import React from 'react';
import { yupResolver } from '@hookform/resolvers/yup';
import validators from '../../../../common/services/Validators';
import styles from './place.styles';

const Place = ({ wizardController }: WizardStepProps<PlantContext>) => {
  const { data: places, isLoading: placesLoading } = useGetPlacesQuery();

  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.selectPlaceSchema),
    defaultValues: {
      place: wizardController.context.place ?? ''
    }
  });

  const {
    getValues,
    formState: { isValid },
    control
  } = methods;

  const onNext = () => {
    const placeId = getValues('place');
    const placeName = places?.find((p) => p.id === +placeId)?.name;
    wizardController.updateContext({
      ...wizardController.context,
      place: getValues('place'),
      placeName
    });
    wizardController.goToNextStep();
  };

  return (
    <WizardStep
      nextButton={{
        onClick: onNext,
        isDisabled: !isValid,
        title: 'Next'
      }}
      cancelButton={{
        onClick: () => wizardController.onCancel(),
        isDisabled: false,
        title: 'Cancel'
      }}
      backButton={{
        onClick: () => wizardController.goToPreviousStep(),
        isDisabled: false,
        title: 'Back'
      }}
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
