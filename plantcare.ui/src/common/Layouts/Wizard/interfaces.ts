import { ReactElement, ReactNode } from 'react';
import { UseFormReturn } from 'react-hook-form';

export interface wizardStepProps {
  children: ReactElement;
  currentStep: number;
  validators: string[];
  goToStep(step: number): any;
  nextStep(): any;
  previousStep(): any;
}

export interface IWizardStep {
  title: string;
  component: ReactElement;
  validators: string[];
  order: number;
}

export interface wizardContextProps {
  steps: IWizardStep[];
  methods: UseFormReturn<
    { name: string; description: string; plantType: string; plantPlace: string },
    any,
    undefined
  >;
}
