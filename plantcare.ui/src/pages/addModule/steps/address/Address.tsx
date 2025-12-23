import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { AddModuleContext } from '../../interfaces';
import React, { useState } from 'react';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import { Box, Typography } from '@mui/material';
import styles from './address.styles';

const Address = ({ wizardController }: WizardStepProps<AddModuleContext>) => {
  const [address, setAddress] = useState<string | undefined>();
  const [receiveAddressSucc, setReceiveAddressSucc] = useState<boolean>(false);
  const [fetchingAddress, setFetchingAddress] = useState<boolean>(false);

  const receiveDataFromModule = async () => {
    setFetchingAddress(true);
    try {
      const crc = wizardController.context.moduleAddressService;
      if (crc) {
        const data = await crc.readValue();
        const textDecoder = new TextDecoder();
        setAddress(textDecoder.decode(data));
        const device = wizardController.context.device;
        device?.gatt?.disconnect();
        setFetchingAddress(false);
        return true;
      }
      setFetchingAddress(false);
      return false;
    } catch (error) {
      setFetchingAddress(false);
      return false;
    }
  };

  const onNext = async () => {
    if (!address) {
      const result = await receiveDataFromModule();
      setReceiveAddressSucc(result);
      return;
    }

    wizardController.updateContext({
      address: address
    });
    wizardController.goToNextStep();
  };

  return (
    <WizardStep
      nextButton={{
        onClick: async () => await onNext(),
        isDisabled: fetchingAddress || (receiveAddressSucc! && !address),
        title: address ? 'Next' : 'Get Address'
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
      <Box sx={styles.addressWrapper}>
        <Typography>Address Configuration</Typography>
      </Box>
    </WizardStep>
  );
};

export default Address;
