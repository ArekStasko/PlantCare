import {ReactElement, ReactNode} from "react";

export interface wizardStepProps {
    children: ReactElement
    goToStep(step: number): any
    nextStep(): any
    previousStep(): any
}

export interface IWizardStep {
    title: string
    component: ReactElement
    order: number
}

export interface wizardContextProps {
    steps: IWizardStep[]
}

