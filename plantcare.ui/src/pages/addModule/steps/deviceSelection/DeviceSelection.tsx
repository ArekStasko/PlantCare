import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { AddModuleContext } from '../../interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import React, { useMemo, useState } from 'react';
import { BLEDevice } from '../../../../common/models/BLEDevice';
import styles from './deviceSelection.styles';
import { Box, Button, CircularProgress, Typography } from '@mui/material';
import CustomAlert from '../../../../common/components/customAlert/customAlert';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';

const DeviceSelection = ({ wizardController }: WizardStepProps<AddModuleContext>) => {
  const [device, setDevice] = useState<BLEDevice | undefined>(wizardController.context.device);
  const [alert, setAlert] = useState<string | undefined>();
  const [selectingDevice, setSelectingDevice] = useState<boolean>(false);

  const selectDevice = async () => {
    setSelectingDevice(true);
    if ('bluetooth' in navigator) {
      try {
        let device;
        let wifiDataService;

        const serviceUuid = '00000180-0000-1000-8000-00805f9b34fb';
        const saveWifiDataServiceCharacteristicUuid = '0000dead-0000-1000-8000-00805f9b34fb';

        device = await navigator.bluetooth.requestDevice({
          acceptAllDevices: true,
          optionalServices: [serviceUuid]
        });

        const server = await device.gatt?.connect();
        const service = await server?.getPrimaryService(serviceUuid);
        wifiDataService = await service?.getCharacteristic(
          saveWifiDataServiceCharacteristicUuid
        );

        wizardController.updateContext({
          ...wizardController.context,
          device: device,
          wifiDataService: wifiDataService,
        });
        setDevice(device);
      } catch (error) {
        console.error(error)
        setAlert('We are unable to connect to the device, make sure the bluetooth is on');
      }
    } else {
      setAlert('Bluetooth is not available');
    }
    setSelectingDevice(false);
  };

  const disableNextBtn = useMemo(() => {
    const savedDevice = wizardController.context.device;
    const savedWifiDataServiceCharacteristic = wizardController.context.wifiDataService;

    return (
      !savedDevice ||
      !savedWifiDataServiceCharacteristic
    );
  }, [wizardController.context]);

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
        {selectingDevice ? (
          <CircularProgress />
        ) : (
          <>
            {alert !== undefined && device === undefined && (
              <CustomAlert message={alert} type={'error'} />
            )}
            <Box>
              <Typography variant="h6">Select plantcare module from device list</Typography>
              <Typography variant="subtitle1">Make sure that bluetooth is turned on</Typography>
            </Box>
            <Button onClick={async () => await selectDevice()}>Select device</Button>
            {device && (
              <Card sx={{ minWidth: 275 }}>
                <CardContent>
                  <Typography>Paired Device:</Typography>
                  <Typography component="div">{device.name}</Typography>
                </CardContent>
              </Card>
            )}
          </>
        )}
      </Box>
    </WizardStep>
  );
};

export default DeviceSelection;
