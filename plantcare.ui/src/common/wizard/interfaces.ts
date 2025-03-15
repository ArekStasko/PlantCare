import { SxProps } from '@mui/material';
import React from 'react';

export interface WizardStepProps<T> {
  wizardController: WizardController<T>;
}

export interface buttonAction {
  onClick: () => void;
  title: string;
  isDisabled: boolean;
}

export interface wizardStepProviderProps<T> {
  children: React.ReactNode;
  nextButton: buttonAction;
  cancelButton: buttonAction;
  backButton: buttonAction;
  title: string;
  sx?: SxProps;
}

export interface WizardStepData {
  order: number;
  title: string;
  isFinal: boolean;
}

export interface WizardStep<T> {
  order: number;
  title: string;
  getStep: (wizardController: WizardController<T>) => JSX.Element;
}

export interface WizardController<T> {
  context: T;
  onLoading: (isLoading: boolean) => void;
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
}
