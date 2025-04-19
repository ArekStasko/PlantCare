import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { AddModuleContext } from '../../interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import { Box, IconButton, TextField, Typography } from "@mui/material";
import styles from './nameForm.styles';
import React from 'react';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import validators from '../../../../common/services/Validators';
import { GetName } from 'namee'
import RefreshIcon from '@mui/icons-material/Refresh';

const NameForm = ({ wizardController }: WizardStepProps<AddModuleContext>) => {
  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.addModuleNameSchema),
    defaultValues: {
      moduleName: wizardController.context.moduleName ?? GetName()
    }
  });

  const {
    register,
    setValue,
    getValues,
    formState: { errors }
  } = methods;

  const resetName = () => setValue('moduleName', GetName())

  return (
    <WizardStep
      nextButton={{
        title: 'Next',
        isDisabled: false,
        onClick: () => {
          wizardController.updateContext({
            ...wizardController.context,
            moduleName: getValues('moduleName')
          });
          wizardController.goToNextStep()
        }
      }}
      cancelButton={{
        title: 'Cancel',
        isDisabled: false,
        onClick: () => wizardController.onCancel()
      }}
      backButton={{
        title: 'Back',
        isDisabled: false,
        onClick: () => wizardController.goToPreviousStep()
      }}
      title="Module Name"
      sx={styles.container}
    >
      <Box sx={styles.nameForm}>
        <Typography variant="h6">Enter the name of the place</Typography>
        <Box sx={styles.textFieldWrapper}>
          <TextField
            sx={styles.textfield}
            label="moduleName"
            id="moduleName"
            variant="filled"
            error={!!errors.moduleName}
            helperText={errors?.moduleName?.message?.toString()}
            {...register('moduleName')}
          />
          <IconButton sx={styles.iconButton} onClick={() => resetName()}>
            <RefreshIcon/>
          </IconButton>
        </Box>
      </Box>
    </WizardStep>
  );
};

export default NameForm;
