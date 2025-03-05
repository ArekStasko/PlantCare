export interface WizardProgressStep {
  order: number,
  title: string,
}

export interface WizardProgressProps {
  steps: WizardProgressStep[],
  currentStep: number,
}