import { SxProps } from "@mui/material";
import wizardStep from "../Layouts/Wizard/WizardStep/wizardStep";

export interface WizardStepProps<T> {
  wizardController: WizardController<T>;
}

export interface WizardStepData {
  order: number;
  title: string;
  isFinal: boolean;
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
  onCancel: () => void;
}

export interface WizardProps<T> {
  initialContext: T;
  steps: WizardStep<T>[];
  onSubmit: (context: T) => void;
}