import React from "react";

export interface WizardStepProps<T> {
  wizardController: WizardController<T>;
}

export interface WizardStep<T> {
  order: number;
  title: string;
  step: React.ComponentType<WizardStepProps<T>>;
}

export interface WizardController<T> {
  updateContext: (context: T) => void;
  clearContext: () => void;
  goToNextStep: () => void;
  goToPreviousStep: () => void;
  goToStep: (step: number) => void;
}

export interface WizardProps<T> {
  initialContext: T;
  steps: WizardStep<T>[];
}