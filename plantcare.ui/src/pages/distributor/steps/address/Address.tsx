import { Box, TextField, Typography } from '@mui/material';
import { AddDistributorContext } from '../../interfaces';
import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import styles from './address.styles';
import React from 'react';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import validators from '../../../../common/services/Validators';

const Address = ({ wizardController }: WizardStepProps<AddDistributorContext>) => {
  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.addModuleAddressSchema),
    defaultValues: {
      address: wizardController.context.address ?? ''
    }
  });

  const {
    register,
    getValues,
    formState: { errors, isValid }
  } = methods;

  const onNext = async () => {
    wizardController.updateContext({
      address: getValues('address')
    });
    wizardController.goToNextStep();
  };

  return (
    <WizardStep
      nextButton={{
        onClick: async () => console.log('address submit'),
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
      title={'Address'}
    >
      <Box sx={styles.container}>
        <Typography sx={styles.subtitle} variant="h6">
          Provide server IP address to which you want to connect your distributor
        </Typography>
        <TextField
          sx={styles.textfield}
          label="IP Address"
          id="address"
          error={!!errors.address}
          helperText={errors.address && 'Address is required'}
          variant="filled"
          {...register('address')}
        />
      </Box>
    </WizardStep>
  );
};

export default Address;
