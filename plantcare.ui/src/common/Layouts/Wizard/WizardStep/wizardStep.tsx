import React from 'react';
import styles from './wizardStep.styles';
import { Box, Button, Card, CardActions, CardContent } from '@mui/material';
import { wizardStepProps } from '../interfaces';
import { useNavigate } from 'react-router';
import routingConstants from '../../../../app/routing/routingConstants';
import CancelDialog from '../../../compontents/CancelDialog/cancelDialog';
import { useFormContext } from 'react-hook-form';
import CustomAlert from '../../../compontents/customAlert/customAlert';
import RoutingConstants from '../../../../app/routing/routingConstants';
import { useGetPlacesQuery } from '../../../slices/getPlaces/getPlaces';
import { useGetPlantsQuery } from '../../../slices/getPlants/getPlants';

export const WizardStep = ({
  children,
  currentStep,
  validators,
  onSubmit,
  isLastStep,
  goToStep,
  previousStep,
  nextStep
}: wizardStepProps) => {
  const navigate = useNavigate();
  const { refetch: refetchPlaces } = useGetPlacesQuery();
  const { refetch: refetchPlants } = useGetPlantsQuery();
  const [openCancelDialog, setOpenCancelDialog] = React.useState(false);
  const [isAlertActive, setIsAlertActive] = React.useState(false);
  const [isSuccess, setIsSuccess] = React.useState(false);

  const {
    formState: { errors, isValid },
    watch
  } = useFormContext();
  const isFormCorrect = () =>
    !validators.some((validator) => errors[validator] || watch(validator) === undefined);

  const submitFlow = async () => {
    const result = await onSubmit();
    setIsSuccess(result);
    setIsAlertActive(!isAlertActive);
  };

  const goToDashboard = () => {
    refetchPlaces();
    refetchPlants();
    navigate(RoutingConstants.root);
  };

  return (
    <Card sx={styles.card}>
      <CancelDialog setOpenDialog={setOpenCancelDialog} openDialog={openCancelDialog} />
      {isAlertActive && (
        <CustomAlert
          message={
            isSuccess ? 'Operation was successfully processed' : 'Sorry, something went wrong.'
          }
          type={isSuccess ? 'success' : 'error'}
        />
      )}
      <CardContent sx={styles.contentWrapper}>{children}</CardContent>
      <CardActions sx={styles.buttonWrapper}>
        <Button
          disabled={currentStep === 0}
          sx={styles.btn}
          variant="contained"
          onClick={() => previousStep()}
          size="medium">
          Back
        </Button>
        <Box>
          <Button
            sx={styles.btn}
            variant="outlined"
            size="medium"
            onClick={() => setOpenCancelDialog(!openCancelDialog)}>
            Cancel
          </Button>
          {isLastStep() ? (
            <Button
              disabled={!isFormCorrect()}
              sx={styles.btn}
              variant="contained"
              onClick={async () => (isAlertActive ? goToDashboard() : await submitFlow())}
              size="medium">
              {isAlertActive ? 'Go to Dashboard' : 'Submit'}
            </Button>
          ) : (
            <Button
              disabled={!isFormCorrect()}
              sx={styles.btn}
              variant="contained"
              onClick={() => goToStep(nextStep!)}
              size="medium">
              Proceed
            </Button>
          )}
        </Box>
      </CardActions>
    </Card>
  );
};

export default WizardStep;
