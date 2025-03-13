export interface WizardProgressStep {
  order: number,
  title: string,
}

export interface ProgressTileProps {
  title: string;
  completed: boolean;
  active: boolean;
}

export interface WizardProgressProps {
  steps: WizardProgressStep[],
  currentStep: number,
}