import React from 'react';
import { wizardContextProps } from '../interfaces';
import styles from './wizardContext.styles';
import { Box, CircularProgress, Container, Step, StepLabel, Stepper } from "@mui/material";
import WizardStep from '../WizardStep/wizardStep';
import { FormProvider } from 'react-hook-form';
import { number } from 'yup';

export const WizardContext = ({ onSubmit, steps, methods, isLoading }: wizardContextProps) => {
  const [currentStepId, setCurrentStepId] = React.useState(0);

  const getCurrentStep = () => {
    return steps.find((s) => s.id === currentStepId);
  };

  const goToNextStep = () => {
    const step = steps.find((s) => s.id === getCurrentStep()!.nextStep);
    if (!step) return;
    setCurrentStepId(step.id);
  };

  const previousStep = () => {
    const currentStep = getCurrentStep()!;
    if (currentStep.previousStep !== undefined) {
      const previousStep = steps.find((s) => s.id === currentStep.previousStep);
      if (!previousStep) return;
      setCurrentStepId(previousStep.id);
    }
  };
  const isLastStep = (): boolean => getCurrentStep()!.isFinal;
  const submitDecorator = async (): Promise<boolean> => {
    const result = await onSubmit();
    if ('data' in result) {
      return result.data;
    } else {
      return false;
    }
  };

  return (
    <>
      <Container sx={styles.container}>
        <Box sx={styles.contentWrapper}>
          <Stepper activeStep={currentStepId} sx={styles.stepper}>
            {steps.map(
              (step) =>
                step.isStepVisible && (
                  <Step key={step.id}>
                    <StepLabel>{step.title}</StepLabel>
                  </Step>
                )
            )}
          </Stepper>
          <FormProvider {...methods}>
            {isLoading ? (
              <CircularProgress />
            ) : (
              <WizardStep
                currentStepId={currentStepId}
                validators={getCurrentStep()!.validators}
                isLastStep={isLastStep}
                onSubmit={submitDecorator}
                goToStep={goToNextStep}
                previousStep={previousStep}>
                {getCurrentStep()!.component}
              </WizardStep>
            )}
          </FormProvider>
        </Box>
      </Container>
    </>
  );
};

export default WizardContext;
