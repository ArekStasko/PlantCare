import { ReactElement, ReactNode } from 'react';
import { UseFormReturn } from 'react-hook-form';
import { FetchBaseQueryError } from '@reduxjs/toolkit/query';
import { SerializedError } from '@reduxjs/toolkit';

export interface wizardStepProps {
  children: ReactElement;
  currentStepId: number;
  validators: string[];
  isLastStep(): boolean;
  onSubmit(): Promise<boolean>;
  goToStep(): void;
  previousStep(): void;
}

export interface IWizardStep {
  id: number;
  title: string;
  component: ReactElement;
  validators: string[];
  isFinal: boolean;
  isStepVisible: boolean;
  previousStep?: number;
  nextStep?: number;
}

export interface wizardContextProps {
  onSubmit(): Promise<{ data: boolean } | { error: FetchBaseQueryError | SerializedError }>;
  steps: IWizardStep[];
  methods: UseFormReturn<any, undefined>;
}
