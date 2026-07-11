import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { AddModuleContext } from '../../interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import styles from './summary.styles';
import { Box, Button, Card, Divider, Typography } from '@mui/material';
import VisibilityIcon from '@mui/icons-material/Visibility';
import VisibilityOffIcon from '@mui/icons-material/VisibilityOff';
import React, { useEffect, useState } from 'react';
import { CreateModuleRequest } from '@arekstasko/plantcare-api-client';
import { useCreateModuleMutation } from '../../../../common/RTK/Module/Module';
import Popup, { PopupStatus } from '../../../../common/components/popup/Popup';
import RoutingPaths from '../../../../app/routing/routingConstants';
import { useNavigate } from 'react-router';

const Summary = ({ wizardController }: WizardStepProps<AddModuleContext>) => {
  const [showPassword, setShowPassword] = useState(false);
  const [createModule, { isLoading: loading }] = useCreateModuleMutation();
  const [success, setSuccess] = useState(false);
  const [showPopup, setShowPopup] = useState(false);

  const navigate = useNavigate();

  useEffect(() => {
    wizardController.onLoading(loading);
  }, [loading]);

  const onSubmit = async () => {
    try {
      const request = {
        name: wizardController.context.moduleName
      } as CreateModuleRequest;
      const result = await createModule(request);
      if ('data' in result) {
        const crc = wizardController.context.wifiDataService;
        if (crc) {
          const name = wizardController.context.wifiName;
          const psw = wizardController.context.wifiPassword;
          const address = wizardController.context.address;
          const encoder = new TextEncoder();
          const data = encoder.encode(`${name}|${psw}|${result.data}|${address}`);
          await crc.writeValue(data);
        }
        setSuccess(true);
        return;
      }
      setSuccess(false);
    } catch (error) {}
    setShowPopup(true);
  };

  return (
    <WizardStep
      nextButton={{
        onClick: () => onSubmit(),
        isDisabled: false,
        title: 'Submit'
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
          titleText={success ? 'Success' : 'Error'}
          contentText={
            success
              ? 'The new module has been added successfully.'
              : 'An error occurred while adding a new module, please try again later.'
          }
          openPopup={showPopup}
          confirmText={'Go to dashboard'}
          confirmAction={() => navigate(`${RoutingPaths.root}`)}
          status={success ? PopupStatus.success : PopupStatus.failure}
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
              Module name
            </Typography>
            <Typography>{wizardController.context.moduleName}</Typography>
          </Box>
          <Divider sx={{ width: '80%' }} />
        </Box>
      </Card>
    </WizardStep>
  );
};

export default Summary;
