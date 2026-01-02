import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { AddModuleContext } from '../../interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import validators from '../../../../common/services/Validators';
import styles from './wifiForm.styles';
import { Box, TextField, Typography } from '@mui/material';
import React from 'react';

const WifiForm = ({ wizardController }: WizardStepProps<AddModuleContext>) => {
  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.addModuleSchema),
    defaultValues: {
      wifiName: wizardController.context.wifiName ?? '',
      wifiPassword: wizardController.context.wifiPassword ?? ''
    }
  });

  const {
    register,
    getValues,
    formState: { errors, isValid }
  } = methods;

  const onNext = () => {
    wizardController.updateContext({
      ...wizardController.context,
      wifiName: getValues('wifiName'),
      wifiPassword: getValues('wifiPassword')
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
      title={'Wifi Configuration'}
    >
      <Box sx={styles.container}>
        <Typography sx={styles.subtitle} variant="h6">
          Provide your WiFi network information
        </Typography>
        <TextField
          sx={styles.textfield}
          label="SSID"
          id="wifiName"
          error={!!errors.wifiName}
          helperText={errors.wifiName && 'WiFi name is required'}
          variant="filled"
          {...register('wifiName')}
        />
        <TextField
          sx={styles.textfield}
          label="Password"
          id="wifiPassword"
          type="password"
          error={!!errors.wifiPassword}
          helperText={errors.wifiPassword && 'WiFi password is required'}
          variant="filled"
          {...register('wifiPassword')}
        />
      </Box>
    </WizardStep>
  );
};

export default WifiForm;
