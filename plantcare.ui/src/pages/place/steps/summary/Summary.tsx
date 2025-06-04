import { Box, Card, Divider, Typography } from '@mui/material';
import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { PlaceContext, PlaceFlowType } from '../../interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import { useCreatePlaceMutation } from '../../../../common/RTK/createPlace/createPlace';
import { CreatePlaceRequest } from '../../../../common/RTK/createPlace/createPlaceRequest';
import { useEffect } from 'react';
import Popup, { PopupStatus } from '../../../../common/components/popup/Popup';
import { useNavigate } from 'react-router';
import RoutingConstants from '../../../../app/routing/routingConstants';
import styles from './summary.styles';
import { useUpdatePlaceMutation } from '../../../../common/RTK/updatePlace/updatePlace';
import { UpdatePlaceRequest } from '../../../../common/RTK/updatePlace/updatePlaceRequest';

const Summary = ({ wizardController }: WizardStepProps<PlaceContext>) => {
  const navigate = useNavigate();
  const [createPlace, { data: createPlaceResult, isLoading: createPlaceLoading }] =
    useCreatePlaceMutation();
  const [updatePlace, { data: updatePlaceResult, isLoading: updatePlaceLoading }] =
    useUpdatePlaceMutation();

  useEffect(() => {
    wizardController.onLoading(createPlaceLoading || updatePlaceLoading);
  }, [createPlaceLoading, updatePlaceLoading]);

  const onSubmit = async () => {
    if (wizardController.context.flowType === PlaceFlowType.UPDATE) {
      const request = {
        id: wizardController.context.id,
        name: wizardController.context.name
      } as UpdatePlaceRequest;
      await updatePlace(request);
      return;
    }

    const request = {
      name: wizardController.context.name
    } as CreatePlaceRequest;
    await createPlace(request);
  };

  return (
    <WizardStep
      nextButton={{
        onClick: onSubmit,
        isDisabled: createPlaceLoading || updatePlaceLoading,
        title: 'Submit'
      }}
      cancelButton={{
        onClick: () => wizardController.goToNextStep(),
        isDisabled: false,
        title: 'Cancel'
      }}
      backButton={{
        onClick: () => wizardController.goToPreviousStep(),
        isDisabled: false,
        title: 'Back'
      }}
      title="Details"
      popup={
        <Popup
          titleText={'Success'}
          contentText={
            createPlaceResult || updatePlaceResult
              ? 'The new Place has been added successfully.'
              : 'An error occurred while adding a new Place, please try again later.'
          }
          openPopup={(createPlaceResult || updatePlaceResult) ?? false}
          confirmText={'Go to Dashboard'}
          confirmAction={() => navigate(RoutingConstants.root)}
          status={
            createPlaceResult || updatePlaceResult ? PopupStatus.success : PopupStatus.failure
          }
        />
      }
    >
      <Card elevation={5} sx={styles.summaryList}>
        <Box sx={styles.summaryListElement}>
          <Box sx={styles.summaryListText}>
            <Typography variant="button" sx={styles.summaryListTitle}>
              Name
            </Typography>
            <Typography>{wizardController.context.name}</Typography>
          </Box>
          <Divider sx={{ width: '80%' }} />
        </Box>
      </Card>
    </WizardStep>
  );
};

export default Summary;
