import { Box, Card, Divider, Typography } from '@mui/material';
import { buttonAction, WizardStepProps } from '../../../../common/wizard/interfaces';
import { CreatePlaceContext } from '../../interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import { useCreatePlaceMutation } from '../../../../common/RTK/createPlace/createPlace';
import { CreatePlaceRequest } from '../../../../common/RTK/createPlace/createPlaceRequest';
import { useEffect } from 'react';
import Popup, { PopupStatus } from '../../../../common/components/popup/Popup';
import { useNavigate } from 'react-router';
import RoutingConstants from '../../../../app/routing/routingConstants';
import styles from './summary.styles';

const Summary = ({ wizardController }: WizardStepProps<CreatePlaceContext>) => {
  const navigate = useNavigate();
  const [createPlant, { data, isLoading }] = useCreatePlaceMutation();

  useEffect(() => {
    wizardController.onLoading(isLoading);
  }, [isLoading]);

  const onSubmit = async () => {
    const request = {
      name: wizardController.context.name
    } as CreatePlaceRequest;
    await createPlant(request);
  };

  return (
    <WizardStep
      nextButton={{
        onClick: onSubmit,
        isDisabled: isLoading,
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
