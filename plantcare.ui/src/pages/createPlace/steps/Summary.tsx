import { Typography } from '@mui/material';
import { buttonAction, WizardStepProps } from '../../../common/wizard/interfaces';
import { CreatePlaceContext } from '../interfaces';
import { WizardStep } from '../../../common/wizard/components/wizardStep/WizardStep';

const Summary = ({ wizardController }: WizardStepProps<CreatePlaceContext>) => {
  const nextButton = {
    onClick: () => wizardController.goToNextStep(),
    isDisabled: false,
    title: 'Submit'
  } as buttonAction;

  const cancelButton = {
    onClick: () => wizardController.goToNextStep(),
    isDisabled: false,
    title: 'Cancel'
  } as buttonAction;

  const backButton = {
    onClick: () => wizardController.goToPreviousStep(),
    isDisabled: false,
    title: 'Back'
  } as buttonAction;

  return (
    <WizardStep
      nextButton={nextButton}
      cancelButton={cancelButton}
      backButton={backButton}
      title="Details"
    >
      <Typography>{wizardController.context.name}</Typography>
    </WizardStep>
  );
};

export default Summary;
