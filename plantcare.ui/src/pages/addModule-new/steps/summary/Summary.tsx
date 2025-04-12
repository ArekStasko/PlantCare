import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { AddModuleContext } from '../../interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import styles from "./summary.styles";
import { Box, Button, Card, Divider, Typography } from "@mui/material";
import VisibilityIcon from '@mui/icons-material/Visibility';
import VisibilityOffIcon from '@mui/icons-material/VisibilityOff';
import { useState } from "react";

const Summary = ({ wizardController }: WizardStepProps<AddModuleContext>) => {
  const [showPassword, setShowPassword] = useState(false);

  return (
    <WizardStep
      nextButton={{
        onClick: () => console.log('Submit'),
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
              {showPassword ? <VisibilityOffIcon onClick={() => setShowPassword(false)} sx={{cursor: 'pointer'}} /> : <VisibilityIcon onClick={() => setShowPassword(true)} sx={{cursor: 'pointer'}} />}
              {wizardController.context.wifiPassword}
            </Button>
          </Box>
          <Divider sx={{ width: '80%' }} />
        </Box>
      </Card>
    </WizardStep>
  );
};

export default Summary;
