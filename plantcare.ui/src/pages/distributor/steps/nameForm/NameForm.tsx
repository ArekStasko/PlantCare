import { Box, IconButton, TextField, Typography } from '@mui/material';
import { AddDistributorContext } from '../../interfaces';
import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import validators from '../../../../common/services/Validators';
import { GetName } from 'namee';
import styles from '../../../addModule/steps/nameForm/nameForm.styles';
import RefreshIcon from '@mui/icons-material/Refresh';
import React from 'react';

const NameForm = ({ wizardController }: WizardStepProps<AddDistributorContext>) => {
  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.addDistributorNameSchema),
    defaultValues: {
      distributorName: wizardController.context.distributorName ?? GetName()
    }
  });

  const {
    register,
    setValue,
    getValues,
    trigger,
    formState: { errors, isValid }
  } = methods;

  const resetName = () => {
    setValue('distributorName', GetName());
    trigger('distributorName');
  };

  return (
    <WizardStep
      nextButton={{
        onClick: () => {
          wizardController.updateContext({
            ...wizardController.context,
            distributorName: getValues('distributorName')
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
        isDisabled: false,
        title: 'Back'
      }}
      title={'NameForm'}
    >
      <Box sx={styles.nameForm}>
        <Typography variant="h6">Enter the name of the distributor</Typography>
        <Box sx={styles.textFieldWrapper}>
          <TextField
            sx={styles.textfield}
            id="distributorName"
            variant="standard"
            error={!!errors.distributorName}
            helperText={errors?.distributorName?.message?.toString()}
            {...register('distributorName')}
          />
          <IconButton sx={styles.iconButton} onClick={() => resetName()}>
            <RefreshIcon />
          </IconButton>
        </Box>
      </Box>
    </WizardStep>
  );
};

export default NameForm;
