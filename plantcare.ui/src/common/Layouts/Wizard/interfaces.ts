import { ReactElement, ReactNode } from 'react';
import { UseFormReturn } from 'react-hook-form';

export interface wizardStepProps {
  children: ReactElement;
  currentStep: number;
  validators: string[];
  isLastStep(): boolean;
  onSubmit(): Promise<void>;
  goToStep(step: number): void;
  nextStep(): void;
  previousStep(): void;
}

export interface IWizardStep {
  title: string;
  component: ReactElement;
  validators: string[];
  order: number;
}

export interface wizardContextProps {
  onSubmit(): Promise<void>;
  steps: IWizardStep[];
  methods: UseFormReturn<any, undefined>;
}
