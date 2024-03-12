import { ReactElement, ReactNode } from 'react';
import { UseFormReturn } from 'react-hook-form';
import { FetchBaseQueryError } from '@reduxjs/toolkit/query';
import { SerializedError } from '@reduxjs/toolkit';

export interface wizardStepProps {
  children: ReactElement;
  currentStep: number;
  validators: string[];
  isLastStep(): boolean;
  onSubmit(): Promise<boolean>;
  goToStep(step: number): void;
  previousStep(): void;
  nextStep?: number;
}

export interface IWizardStep {
  title: string;
  component: ReactElement;
  validators: string[];
  order: number;
  isFinal: boolean;
  isStepVisible: boolean;
  nextStep?: number;
}

export interface wizardContextProps {
  onSubmit(): Promise<{ data: boolean } | { error: FetchBaseQueryError | SerializedError }>;
  steps: IWizardStep[];
  methods: UseFormReturn<any, undefined>;
}
