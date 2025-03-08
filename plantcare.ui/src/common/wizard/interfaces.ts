import { SxProps } from "@mui/material";

export interface WizardStepProps<T> {
  wizardController: WizardController<T>;
}

export interface WizardStep<T> {
  order: number;
  title: string;
  isFinal: boolean;
  getStep: (wizardController: WizardController<T>) => JSX.Element;
}

export interface WizardController<T> {
  context: T,
  onLoading: (isLoading: boolean) => void,
  updateContext: (context: T) => void;
  clearContext: () => void;
  goToNextStep: () => void;
  goToPreviousStep: () => void;
  goToStep: (step: number) => void;
}

export interface WizardProps<T> {
  sx?: SxProps;
  initialContext: T;
  steps: WizardStep<T>[];
  onSubmit: (context: T) => void;
}