import React from "react";
import { SxProps } from "@mui/material";
import { buttonAction, WizardStep } from "../wizard/interfaces";

export interface DialogWizardController<T> {
  context: T;
  updateContext: (context: T) => void;
  clearContext: () => void;
  goToNextStep: () => void;
  goToPreviousStep: () => void;
  onCancel: () => void;
}

export interface DialogWizardStep<T> {
  order: number;
  title: string;
  getStep: (wizardController: DialogWizardController<T>) => JSX.Element;
}

export interface DialogWizardStepProps<T> {
  dialogWizardController: DialogWizardController<T>;
}

export interface DialogWizardStepProviderProps<T> {
  children: React.ReactNode;
  nextButton: buttonAction;
  cancelButton: buttonAction;
  backButton: buttonAction;
  title: string;
  sx?: SxProps;
}

export interface DialogWizardProps<T> {
  initialContext: T;
  steps: DialogWizardStep<T>[];
  open: boolean;
  onClose: (close: boolean) => void;
}