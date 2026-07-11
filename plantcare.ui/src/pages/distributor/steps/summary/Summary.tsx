import { Box, Button, Card, Divider, Typography } from '@mui/material';
import { AddDistributorContext } from '../../interfaces';
import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import styles from '../../../addModule/steps/summary/summary.styles';
import VisibilityOffIcon from '@mui/icons-material/VisibilityOff';
import VisibilityIcon from '@mui/icons-material/Visibility';
import React, { useEffect, useState } from 'react';
import { CreateDistributorRequest } from '@arekstasko/plantcare-api-client';
import {
  DistributorPlantRequest,
  useAddPlantToDistributorMutation,
  useCreateDistributorMutation
} from '../../../../common/RTK/Distributor/Distributor';
import Popup, { PopupStatus } from '../../../../common/components/popup/Popup';
import { useNavigate } from 'react-router';
import RoutingPaths from '../../../../app/routing/routingConstants';

const Summary = ({ wizardController }: WizardStepProps<AddDistributorContext>) => {
  const navigate = useNavigate();
  const [showPassword, setShowPassword] = useState(false);
  const [status, setStatus] = useState<boolean | undefined>(undefined);
  const [createDistributor, { isLoading: createDistributorLoading }] =
    useCreateDistributorMutation();
  const [addPlantToDistributor, { isLoading: addPlantToDistributorLoading }] =
    useAddPlantToDistributorMutation();

  useEffect(() => {
    const loading = createDistributorLoading || addPlantToDistributorLoading;
    wizardController.onLoading(loading);
  }, [createDistributorLoading, addPlantToDistributorLoading]);

  const performAddPlantToDistributor = async (distributorId: string) => {
    const request = {
      id: distributorId,
      plantId: wizardController.context.plantId?.toString()
    } as DistributorPlantRequest;
    const result = await addPlantToDistributor(request);
    if ('data' in result) {
      setStatus(result.data);
    }
  };

  const onSubmit = async () => {
    const request = {
      name: wizardController.context.distributorName
    } as CreateDistributorRequest;
    const result = await createDistributor(request);
    if ('data' in result) {
      const crc = wizardController.context.wifiDataService;
      if (crc) {
        const name = wizardController.context.wifiName;
        const psw = wizardController.context.wifiPassword;
        const address = wizardController.context.address;
        const encoder = new TextEncoder();
        const data = encoder.encode(`${name}|${psw}|${result.data}|${address}`);
        try {
          await crc.writeValue(data);
        } catch (err) {
          console.log(err);
        }
      }
      await performAddPlantToDistributor(result.data.toString());
    }
  };

  return (
    <WizardStep
      nextButton={{
        onClick: () => onSubmit(),
        isDisabled: false,
        title: 'Next'
      }}
      cancelButton={{
        onClick: () => wizardController.onCancel(),
        isDisabled: false,
        title: 'Cancel'
      }}
      backButton={{
        onClick: () => wizardController.goToPreviousStep(),
        isDisabled: false,
        title: 'Back'
      }}
      title={'Summary'}
      popup={
        <Popup
          titleText={status !== undefined && status ? 'Success' : 'Error'}
          contentText={
            status !== undefined && status
              ? 'The new distributor has been added successfully.'
              : 'An error occurred while adding a new distributor, please try again later.'
          }
          openPopup={status !== undefined}
          confirmText={'Go to plant details'}
          confirmAction={() =>
            navigate(
              `${RoutingPaths.plantDetails}/${wizardController.context.plantId}/${wizardController.context.moduleId}`
            )
          }
          status={status !== undefined && status ? PopupStatus.success : PopupStatus.failure}
        />
      }
    >
      <Card elevation={5} sx={styles.summaryList}>
        <Box sx={styles.summaryListElement}>
          <Box sx={styles.summaryListText}>
            <Typography variant="button" sx={styles.summaryListTitle}>
              Wifi Name
            </Typography>
            <Typography>{wizardController.context.wifiName}</Typography>
          </Box>
          <Divider sx={{ width: '80%' }} />
        </Box>
        <Box sx={styles.summaryListElement}>
          <Box sx={styles.summaryListText}>
            <Typography variant="button" sx={styles.summaryListTitle}>
              Wifi Password
            </Typography>
            <Button
              onClick={() => setShowPassword(!showPassword)}
              sx={styles.summaryListPassword(showPassword)}
            >
              {showPassword ? (
                <VisibilityOffIcon
                  onClick={() => setShowPassword(false)}
                  sx={{ cursor: 'pointer' }}
                />
              ) : (
                <VisibilityIcon onClick={() => setShowPassword(true)} sx={{ cursor: 'pointer' }} />
              )}
              {wizardController.context.wifiPassword}
            </Button>
          </Box>
          <Divider sx={{ width: '80%' }} />
        </Box>
        <Box sx={styles.summaryListElement}>
          <Box sx={styles.summaryListText}>
            <Typography variant="button" sx={styles.summaryListTitle}>
              Distributor name
            </Typography>
            <Typography>{wizardController.context.distributorName}</Typography>
          </Box>
          <Divider sx={{ width: '80%' }} />
        </Box>
      </Card>
    </WizardStep>
  );
};

export default Summary;
