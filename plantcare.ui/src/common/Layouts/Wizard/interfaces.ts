import { ReactElement, ReactNode } from 'react';
import { UseFormReturn } from 'react-hook-form';

export interface wizardStepProps {
  children: ReactElement;
  goToStep(step: number): any;
  nextStep(): any;
  previousStep(): any;
}

export interface IWizardStep {
  title: string;
  component: ReactElement;
  order: number;
}

export interface wizardContextProps {
  steps: IWizardStep[];
  methods: UseFormReturn<{ name: string; description: string; plantType: string }, any, undefined>;
}
