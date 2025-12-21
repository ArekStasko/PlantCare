import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { AddModuleContext } from '../../interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import styles from './summary.styles';
import { Box, Button, Card, Divider, Typography } from '@mui/material';
import VisibilityIcon from '@mui/icons-material/Visibility';
import VisibilityOffIcon from '@mui/icons-material/VisibilityOff';
import { useEffect, useState } from 'react';
import {
  CreateModuleRequest,
  useCreateModuleMutation
} from '../../../../common/RTK/createModule/createModule';

const Summary = ({ wizardController }: WizardStepProps<AddModuleContext>) => {
  const [showPassword, setShowPassword] = useState(false);
  const [createModule, { isLoading: loading }] = useCreateModuleMutation();

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
        const id = result.data;
        const crc = wizardController.context.writeService;
        if (crc) {
          const encoder = new TextEncoder();
          const data = encoder.encode(`${id}`);
          await crc.writeValue(data);
        }
        const device = wizardController.context.device;
        device?.gatt?.disconnect();
        return { data: true };
      }
      return { data: false };
    } catch (error) {
      return { data: false };
    }
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
