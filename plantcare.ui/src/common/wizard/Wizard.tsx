import { Backdrop, Box, CircularProgress, Paper, Typography } from "@mui/material";
import React, { useContext, useMemo, useState } from "react";
import { WizardController, WizardProps, WizardStep, WizardStepData } from "./interfaces";
import { WizardProgress } from "./components/wizardProgress/WizardProgress";
import styles from './wizard.styles'
import { WizardProgressStep } from "./components/wizardProgress/interfaces";
import { WizardNavigation } from "./components/wizardNavigation/WizardNavigation";
import CancelDialog from "../compontents/CancelDialog/cancelDialog";

const Wizard = <T,>({initialContext, steps, onSubmit}: WizardProps<T>) => {
  const [currentStep, setCurrentStep] = useState(0);
  const [openCancelDialog, setOpenCancelDialog] = useState(false)
  const [context, setContext] = useState<T>(initialContext);
  const [loading, setLoading] = useState<boolean>(false);

  const Step = useMemo(() => steps.find(step => step.order === currentStep)?.getStep(wizardController), [currentStep])

  const wizardController: WizardController<T> = {
    context: context,
    onLoading: (isLoading: boolean) => setLoading(isLoading),
    updateContext: (context: T) => setContext(context),
    clearContext: () => setContext(initialContext),
    goToNextStep: () => setCurrentStep((prev) => prev + 1),
    goToPreviousStep: () => setCurrentStep((prev) => prev - 1),
    goToStep: (step: number) => setCurrentStep(step),
    onCancel: () => setOpenCancelDialog(true),
  }

  const stepsToDisplayInProgress = useMemo(() => {
    return steps.map(s => ({
      order: s.order,
      title: s.title,
    } as WizardProgressStep))
  }, [steps])

  return (
    <Box sx={styles.wizard}>
      <Box sx={styles.wizardContent}>
      <WizardProgress
        steps={stepsToDisplayInProgress}
        currentStep={currentStep}
      />
      <Backdrop open={loading}>
        <CircularProgress color="secondary" size={20} />
      </Backdrop>
        {Step}
      </Box>
      <CancelDialog openDialog={openCancelDialog} setOpenDialog={setOpenCancelDialog} />
    </Box>
  )
}

export default Wizard;