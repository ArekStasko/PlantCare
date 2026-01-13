import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { AddModuleContext } from '../../interfaces';
import React from 'react';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import { Box, TextField, Typography } from "@mui/material";
import styles from './address.styles';
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import validators from "../../../../common/services/Validators";

const Address = ({ wizardController }: WizardStepProps<AddModuleContext>) => {
  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.addModuleAddressSchema),
    defaultValues: {
      address: wizardController.context.address ?? '',
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
        onClick: async () => await onNext(),
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
      title={'Device'}
    >
      <Box sx={styles.container}>
        <Typography sx={styles.subtitle} variant="h6">
          Provide server IP address to which you want to connect your module
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
