import { Box, TextField, Typography } from '@mui/material';
import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { PlaceContext } from '../../interfaces';
import styles from './details.styles';
import React from 'react';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import validators from '../../../../common/services/Validators';

const Details = ({ wizardController }: WizardStepProps<PlaceContext>) => {
  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.createPlaceSchema),
    defaultValues: {
      name: wizardController.context.name ?? ''
    }
  });

  const {
    register,
    getValues,
    formState: { errors, isValid }
  } = methods;

  return (
    <WizardStep<PlaceContext>
      nextButton={{
        onClick: () => {
          wizardController.updateContext({
            ...wizardController.context,
            name: getValues('name')
          });
          wizardController.goToNextStep();
        },
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
        isDisabled: true,
        title: 'Back'
      }}
      title="Details"
      sx={styles.detailsContainer}
    >
      <Box sx={styles.placeDetailsWrapper}>
        <Typography variant="h6">Enter the name of the place</Typography>
        <TextField
          sx={styles.placeName}
          label="Name"
          id="name"
          variant="filled"
          error={!!errors.name}
          helperText={errors?.name?.message?.toString()}
          {...register('name')}
        />
      </Box>
    </WizardStep>
  );
};

export default Details;
