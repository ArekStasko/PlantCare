import { useMemo, useState } from "react";
import { DialogWizardController, DialogWizardProps } from "./interfaces";
import { Dialog } from "@mui/material";


const DialogWizard = <T,>({ initialContext, steps, open, onClose }: DialogWizardProps<T>) => {
  const [currentStep, setCurrentStep] = useState(0);
  const [context, setContext] = useState<T>(initialContext);

  const dialogWizardController: DialogWizardController<T> = {
    context: context,
    updateContext: (context: T) => setContext(context),
    clearContext: () => setContext(initialContext),
    goToNextStep: () => setCurrentStep((prev) => prev + 1),
    goToPreviousStep: () => setCurrentStep((prev) => prev - 1),
    onCancel: () => onClose(false)
  };

  const Step = useMemo(
    () => steps.find((step) => step.order === currentStep)?.getStep(dialogWizardController),
    [currentStep]
  );

  return(
    <Dialog open={open} onClose={() => onClose(false)}>
      {Step}
    </Dialog>
  )
}

export default DialogWizard;