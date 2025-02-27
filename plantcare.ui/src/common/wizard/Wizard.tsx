import { Box, Typography } from "@mui/material";
import React, { useMemo, useState } from "react";
import { WizardController, WizardProps } from "./interfaces";


const Wizard = <T,>({initialContext, steps}: WizardProps<T>) => {
  const [currentStep, setCurrentStep] = useState(0);
  const [context, setContext] = useState<T>(initialContext);
  const [loading, setLoading] = useState<boolean>(false);

  const wizardController: WizardController<T> = {
    updateContext: (context: T) => setContext(context),
    clearContext: () => setContext(initialContext),
    goToNextStep: () => setCurrentStep((prev) => prev++),
    goToPreviousStep: () => console.log((prev) => prev--),
    goToStep: (step: number) => console.log(step)
  }

  const step = useMemo(() => steps.find(step => step.order === currentStep).step, [currentStep])

  return (
    <Box>
      {
        step ? (
          <step wizardController={wizardController} />
        ) : (
          <Typography>Something went wrong</Typography>
        )
      }
    </Box>
  )
}

export default Wizard;