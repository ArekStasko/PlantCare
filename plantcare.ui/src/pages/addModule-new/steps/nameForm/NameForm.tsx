import { WizardStepProps } from "../../../../common/wizard/interfaces";
import { AddModuleContext } from "../../interfaces";
import { WizardStep } from "../../../../common/wizard/components/wizardStep/WizardStep";
import { Box, TextField, Typography } from "@mui/material";
import styles from './nameForm.styles'
import React from "react";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import validators from "../../../../common/services/Validators";


const NameForm = ({ wizardController }: WizardStepProps<AddModuleContext>) => {

  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.addModuleNameSchema),
    defaultValues: {
      moduleName: wizardController.context.moduleName ?? '',
    }
  });

  const {
    register,
    getValues,
    formState: { errors }
  } = methods;

  return(
    <WizardStep
      nextButton={{
        title: 'Next',
        isDisabled: false,
        onClick: () => {
          wizardController.updateContext({
            ...wizardController.context,
            moduleName: getValues('moduleName')
          })
      }}}
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
        <TextField
          sx={styles.textfield}
          label="moduleName"
          id="moduleName"
          variant="filled"
          error={!!errors.moduleName}
          helperText={errors?.moduleName?.message?.toString()}
          {...register('moduleName')}
        />
      </Box>
    </WizardStep>
  )
}

export default NameForm