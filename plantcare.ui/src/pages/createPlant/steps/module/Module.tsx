import { WizardStep } from "../../../../common/wizard/components/wizardStep/WizardStep";
import { Typography } from "@mui/material";
import { buttonAction, WizardStepProps } from "../../../../common/wizard/interfaces";
import {CreatePlantContext} from "../../interfaces";

const Module = ({ wizardController }: WizardStepProps<CreatePlantContext>) => {

  const nextButton = {
    onClick: () => {
      wizardController.goToNextStep();
    },
    isDisabled: false,
    title: 'Next'
  } as buttonAction;

  const cancelButton = {
    onClick: () => wizardController.onCancel(),
    isDisabled: false,
    title: 'Cancel'
  } as buttonAction;

  const backButton = {
    onClick: () => wizardController.goToPreviousStep(),
    isDisabled: false,
    title: 'Back'
  } as buttonAction;

  return (
    <WizardStep nextButton={nextButton} cancelButton={cancelButton} backButton={backButton} title={"Module"}>
      <Typography>Plant Module</Typography>
    </WizardStep>
  )
}

export default Module