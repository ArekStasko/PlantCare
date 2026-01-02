import { Backdrop, Box, CircularProgress } from '@mui/material';
import React, { useMemo, useState } from 'react';
import { WizardController, WizardProps } from './interfaces';
import { WizardProgress } from './components/wizardProgress/WizardProgress';
import styles from './wizard.styles';
import { WizardProgressStep } from './components/wizardProgress/interfaces';
import CancelDialog from '../components/CancelDialog/cancelDialog';

const Wizard = <T,>({ initialContext, steps }: WizardProps<T>) => {
  const [currentStep, setCurrentStep] = useState(0);
  const [openCancelDialog, setOpenCancelDialog] = useState(false);
  const [context, setContext] = useState<T>(initialContext);
  const [loading, setLoading] = useState<boolean>(false);

  const wizardController: WizardController<T> = {
    context: context,
    onLoading: (isLoading: boolean) => setLoading(isLoading),
    updateContext: (context: T) => setContext((prev) => ({...prev, ...context})),
    clearContext: () => setContext(initialContext),
    goToNextStep: () => setCurrentStep((prev) => prev + 1),
    goToPreviousStep: () => setCurrentStep((prev) => prev - 1),
    goToStep: (step: number) => setCurrentStep(step),
    onCancel: () => setOpenCancelDialog(true)
  };

  const Step = useMemo(
    () => steps.find((step) => step.order === currentStep)?.getStep(wizardController),
    [currentStep, wizardController]
  );

  const stepsToDisplayInProgress = useMemo(() => {
    return steps.map(
      (s) =>
        ({
          title: s.title,
          order: s.order
        }) as WizardProgressStep
    );
  }, [steps]);

  return (
    <Box sx={styles.wizard}>
      <Box sx={styles.wizardContent}>
        <WizardProgress steps={stepsToDisplayInProgress} currentStep={currentStep} />
        <Backdrop open={loading}>
          <CircularProgress color="secondary" size={20} />
        </Backdrop>
        {Step}
      </Box>
      <CancelDialog openDialog={openCancelDialog} setOpenDialog={setOpenCancelDialog} />
    </Box>
  );
};

export default Wizard;
