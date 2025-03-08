import { Backdrop, Box, CircularProgress, Paper, Typography } from "@mui/material";
import React, { useMemo, useState } from "react";
import { WizardController, WizardProps } from "./interfaces";
import { WizardProgress } from "./components/wizardProgress/WizardProgress";
import styles from './wizard.styles'
import { WizardProgressStep } from "./components/wizardProgress/interfaces";
import { WizardNavigation } from "./components/wizardNavigation/WizardNavigation";

const Wizard = <T,>({initialContext, steps, onSubmit, cancelUri}: WizardProps<T>) => {
  const [currentStep, setCurrentStep] = useState(0);
  const [context, setContext] = useState<T>(initialContext);
  const [loading, setLoading] = useState<boolean>(false);

  const wizardController: WizardController<T> = {
    context: context,
    onLoading: (isLoading: boolean) => setLoading(isLoading),
    updateContext: (context: T) => setContext(context),
    clearContext: () => setContext(initialContext),
    goToNextStep: () => setCurrentStep((prev) => prev++),
    goToPreviousStep: () => setCurrentStep((prev) => prev--),
    goToStep: (step: number) => setCurrentStep(step)
  }

  const Step = useMemo(() => steps.find(step => step.order === currentStep)?.getStep(wizardController), [currentStep])
  const stepsToDisplayInProgress = useMemo(() => {
    return steps.map(s => ({
      order: s.order,
      title: s.title,
    } as WizardProgressStep))
  }, [steps])

  const onNext = () => {

  }

  const onBack = () => {

  }

  const onCancel = () => {

  }

  return (
    <Box sx={styles.wizard}>
      <Box sx={styles.wizardContent}>
      <WizardProgress steps={stepsToDisplayInProgress} currentStep={currentStep} />
      <Backdrop open={loading}>
        <CircularProgress color="secondary" size={20} />
      </Backdrop>
      {
        Step ? (
          <Paper sx={styles.stepStyles} elevation={3}>
            {Step}
          </Paper>
        ) : (
          <Typography>Something went wrong</Typography>
        )
      }
      <WizardNavigation onCancel={onCancel} onBack={onBack} onNext={onNext} />
      </Box>
    </Box>
  )
}

export default Wizard;