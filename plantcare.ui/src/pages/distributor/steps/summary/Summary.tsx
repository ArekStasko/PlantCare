import { Box, Button, Card, Divider, Typography } from "@mui/material";
import { AddDistributorContext } from '../../interfaces';
import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import styles from "../../../addModule/steps/summary/summary.styles";
import VisibilityOffIcon from "@mui/icons-material/VisibilityOff";
import VisibilityIcon from "@mui/icons-material/Visibility";
import { useEffect, useState } from "react";
import { CreateDistributorRequest } from "@arekstasko/plantcare-api-client";
import { useCreateDistributorMutation } from "../../../../common/RTK/Distributor/Distributor";

const Summary = ({ wizardController }: WizardStepProps<AddDistributorContext>) => {
  const [showPassword, setShowPassword] = useState(false);
  const [createDistributor, { isLoading: loading }] = useCreateDistributorMutation();

  useEffect(() => {
    wizardController.onLoading(loading);
  }, [loading]);

  const onSubmit = async () => {
    try {
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
          await crc.writeValue(data);
        }
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
