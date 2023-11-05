import React from 'react';
import { wizardContextProps } from '../interfaces';
import styles from './wizardContext.styles';
import { Box, Container, Step, StepLabel, Stepper } from '@mui/material';
import WizardStep from '../WizardStep/wizardStep';
import { FormProvider } from 'react-hook-form';
import { useNavigate } from 'react-router';
import RoutingConstants from '../../../../app/routing/routingConstants';

export const WizardContext = ({ onSubmit, steps, methods }: wizardContextProps) => {
  const navigate = useNavigate();
  const [currentStep, setCurrentStep] = React.useState(0);

  const goToStep = (step: number) => {
    if (step <= steps.length - 1 && step >= 0) {
      setCurrentStep(step);
      return;
    }
    return;
  };

  const nextStep = () => {
    if (currentStep + 1 <= steps.length - 1) {
      setCurrentStep(currentStep + 1);
      return;
    }
    return;
  };

  const previousStep = () => {
    if (currentStep - 1 >= 0) {
      setCurrentStep(currentStep - 1);
      return;
    }
    return;
  };
  const isLastStep = (): boolean => currentStep === steps.length - 1;
  const submitDecorator = async () => {
    await onSubmit();
    navigate(RoutingConstants.root);
  };

  return (
    <>
      <Container sx={styles.container}>
        <Box sx={styles.contentWrapper}>
          <Stepper activeStep={currentStep} sx={styles.stepper}>
            {steps.map((step) => (
              <Step key={step.order}>
                <StepLabel>{step.title}</StepLabel>
              </Step>
            ))}
          </Stepper>
          <FormProvider {...methods}>
            <WizardStep
              currentStep={currentStep}
              validators={steps[currentStep].validators}
              isLastStep={isLastStep}
              onSubmit={submitDecorator}
              goToStep={goToStep}
              previousStep={previousStep}
              nextStep={nextStep}>
              {steps[currentStep].component}
            </WizardStep>
          </FormProvider>
        </Box>
      </Container>
    </>
  );
};

export default WizardContext;
