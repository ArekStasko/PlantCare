import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import { Box, Card, Divider, Typography } from '@mui/material';
import { buttonAction, WizardStepProps } from '../../../../common/wizard/interfaces';
import { CreatePlantContext } from '../../interfaces';
import styles from './summary.styles';
import { PlantType } from '../../../../common/models/plantTypes';
import Decorative from '../../../../app/images/Decorative.png';
import Vegetable from '../../../../app/images/Vegetable.png';
import Fruit from '../../../../app/images/Vegetable.png';
import React from 'react';

const Summary = ({ wizardController }: WizardStepProps<CreatePlantContext>) => {
  const nextButton = {
    onClick: () => {
      
    },
    isDisabled: false,
    title: 'Submit'
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

  const plantTypeToImage = (type?: PlantType) => {
    switch (type) {
      case PlantType.Decorative:
        return Decorative;
      case PlantType.Vegetable:
        return Vegetable;
      case PlantType.Fruit:
        return Fruit;
      default:
        return '';
    }
  };

  return (
    <WizardStep
      nextButton={nextButton}
      cancelButton={cancelButton}
      backButton={backButton}
      title={'Summary'}
    >
      <Card elevation={5} sx={styles.summaryList}>
        <Box sx={styles.summaryListElement}>
          <Box sx={styles.summaryListText}>
            <Typography variant="button" sx={styles.summaryListTitle}>
              Name
            </Typography>
            <Typography>{wizardController.context.name}</Typography>
          </Box>
          <Divider sx={styles.divider} />
        </Box>
        <Box sx={styles.summaryListElement}>
          <Box sx={styles.summaryListText}>
            <Typography variant="button" sx={styles.summaryListTitle}>
              Type
            </Typography>
            <Box
              component="img"
              sx={{
                height: 30,
                width: 30,
                maxHeight: { xs: 30, md: 30 },
                maxWidth: { xs: 30, md: 30 },
                borderRadius: 2
              }}
              alt="Plant_Type"
              src={plantTypeToImage(+(wizardController.context.type ?? 0) as PlantType)}
            />
          </Box>
          <Divider sx={styles.divider} />
        </Box>
        <Box sx={styles.summaryListElement}>
          <Box sx={styles.summaryListText}>
            <Typography variant="button" sx={styles.summaryListTitle}>
              Description
            </Typography>
            <Typography align="justify" sx={{ maxWidth: '60%' }}>
              {wizardController.context.description}
            </Typography>
          </Box>
          <Divider sx={styles.divider} />
        </Box>
        <Box sx={styles.summaryListElement}>
          <Box sx={styles.summaryListText}>
            <Typography variant="button" sx={styles.summaryListTitle}>
              Place
            </Typography>
            <Typography>{wizardController.context.placeName}</Typography>
          </Box>
          <Divider sx={styles.divider} />
        </Box>
        <Box sx={styles.summaryListElement}>
          <Box sx={styles.summaryListText}>
            <Typography variant="button" sx={styles.summaryListTitle}>
              Module
            </Typography>
            <Typography>{wizardController.context.module}</Typography>
          </Box>
          <Divider sx={styles.divider} />
        </Box>
      </Card>
    </WizardStep>
  );
};

export default Summary;
