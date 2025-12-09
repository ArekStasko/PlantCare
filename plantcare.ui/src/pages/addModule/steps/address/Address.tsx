import { WizardStepProps } from "../../../../common/wizard/interfaces";
import { AddModuleContext } from "../../interfaces";
import React, { useState } from "react";
import { BLEDevice } from "../../../../common/models/BLEDevice";
import { WizardStep } from "../../../../common/wizard/components/wizardStep/WizardStep";
import { Box, Typography } from "@mui/material";
import styles from "../deviceSelection/deviceSelection.styles";

const Address = ({ wizardController }: WizardStepProps<AddModuleContext>) => {
  const [device, setDevice] = useState<BLEDevice | undefined>(wizardController.context.device);


  return (
    <WizardStep
      nextButton={{
        onClick: () => wizardController.goToNextStep(),
        isDisabled: disableNextBtn,
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
      title={'Device'}
    >
      <Box sx={styles.deviceSelectionWrapper}>
        <Typography>
          Address Configuration
        </Typography>
      </Box>
    </WizardStep>
  );
};

export default Address;