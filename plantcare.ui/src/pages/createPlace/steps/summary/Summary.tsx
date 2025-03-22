import { Box, Typography } from "@mui/material";
import { buttonAction, WizardStepProps } from '../../../../common/wizard/interfaces';
import { CreatePlaceContext } from '../../interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import { useCreatePlaceMutation } from '../../../../common/RTK/createPlace/createPlace';
import { CreatePlaceRequest } from '../../../../common/RTK/createPlace/createPlaceRequest';
import { useEffect } from 'react';
import Popup, { PopupStatus } from '../../../../common/compontents/popup/Popup';
import { useNavigate } from 'react-router';
import RoutingConstants from '../../../../app/routing/routingConstants';
import styles from'./summary.styles'

const Summary = ({ wizardController }: WizardStepProps<CreatePlaceContext>) => {
  const navigate = useNavigate();
  const [createPlant, { data, isLoading }] = useCreatePlaceMutation();

  useEffect(() => {
    wizardController.onLoading(isLoading);
  }, [isLoading]);

  const nextButton = {
    onClick: async () => {
      const request = {
        name: wizardController.context.name
      } as CreatePlaceRequest;
      await createPlant(request);
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
      popup={
        <Popup
          titleText={'Success'}
          contentText={
            data
              ? 'The new Place has been added successfully.'
              : 'An error occurred while adding a new Place, please try again later.'
          }
          openPopup={data ?? false}
          confirmText={'Go to Dashboard'}
          confirmAction={() => navigate(RoutingConstants.root)}
          status={data ? PopupStatus.success : PopupStatus.failure}
        />
      }
    >
      <Box styles={styles.summaryWrapper}>
        <Typography>Name</Typography>
        <Typography>{wizardController.context.name}</Typography>
      </Box>
    </WizardStep>
  );
};

export default Summary;
