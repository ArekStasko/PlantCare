import { Box, Paper, SxProps, Typography } from "@mui/material";
import React, { useMemo } from "react";
import styles from "../../wizard.styles";
import { WizardNavigation } from "../wizardNavigation/WizardNavigation";
import { WizardController } from "../../interfaces";

export interface wizardStepFuncProps<T> {
  children: React.ReactNode;
  wizardController: WizardController<T>;
  isValid: boolean;
  isFinal: boolean;
  title: string;
  sx?: SxProps;
}

export const WizardStep = <T,>({children, wizardController, isValid, isFinal, sx}: wizardStepFuncProps<T>) => {

  const onNext = () => {
    wizardController.goToNextStep();
  }

  const onBack = () => {
    wizardController.goToPreviousStep();
  }

  const onCancel = () => wizardController.onCancel();

  return (
    <Box sx={{...sx, ...styles.stepWrapper}}>
          <Paper sx={styles.stepStyles} elevation={3}>
            <Typography variant="h5">
              {currentStepObject?.title}
            </Typography>
            {children}
          </Paper>
      <WizardNavigation
        isValid={isValid}
        isFirstStep={currentStepObject?.order === 0}
        isFinalStep={isFinal}
        onCancel={onCancel}
        onBack={onBack}
        onNext={onNext}
      />
    </Box>
  )
}