import { Box, TextField, Typography } from '@mui/material';
import { buttonAction, WizardStepProps } from '../../../common/wizard/interfaces';
import { CreatePlaceContext } from '../interfaces';
import styles from '../../components/placeWizardSteps/Details/details.styles';
import React from 'react';
import { WizardStep } from '../../../common/wizard/components/wizardStep/WizardStep';

const Details = ({ wizardController }: WizardStepProps<CreatePlaceContext>) => {
  const nextButton = {
    onClick: () => wizardController.goToNextStep(),
    isDisabled: false,
    title: 'Next'
  } as buttonAction;

  const cancelButton = {
    onClick: () => wizardController.goToNextStep(),
    isDisabled: false,
    title: 'Cancel'
  } as buttonAction;

  const backButton = {
    onClick: () => wizardController.goToPreviousStep(),
    isDisabled: true,
    title: 'Back'
  } as buttonAction;

  return (
    <WizardStep<CreatePlaceContext>
      nextButton={nextButton}
      cancelButton={cancelButton}
      backButton={backButton}
      title="Details"
      sx={styles.detailsContainer}
    >
      <Box sx={styles.placeDetailsWrapper}>
        <Typography variant="h6">Enter the name of the place</Typography>
        <TextField sx={styles.placeName} label="Name" id="name" variant="filled" />
      </Box>
    </WizardStep>
  );
};

export default Details;
