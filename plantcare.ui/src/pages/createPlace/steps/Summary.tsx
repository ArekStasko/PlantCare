import { Typography } from '@mui/material';
import { buttonAction, WizardStepProps } from '../../../common/wizard/interfaces';
import { CreatePlaceContext } from '../interfaces';
import { WizardStep } from '../../../common/wizard/components/wizardStep/WizardStep';
import { useCreatePlaceMutation } from "../../../common/RTK/createPlace/createPlace";
import { CreatePlaceRequest } from "../../../common/RTK/createPlace/createPlaceRequest";
import { useEffect } from "react";

const Summary = ({ wizardController }: WizardStepProps<CreatePlaceContext>) => {
  const [createPlant, {isLoading}] = useCreatePlaceMutation()

  useEffect(() => {
    wizardController.onLoading(isLoading);
  }, [isLoading])

  const nextButton = {
    onClick: async () => {
      const request = {
        name: wizardController.context.name
      } as CreatePlaceRequest;
      await createPlant(request)
    },
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
