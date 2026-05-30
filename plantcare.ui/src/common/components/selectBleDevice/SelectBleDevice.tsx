import { Box, Button, CircularProgress, Typography } from '@mui/material';
import CustomAlert from '../customAlert/customAlert';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import styles from './SelectBleDevice.styles';
import { useState } from 'react';
import { BLEDevice } from '../../models/BLEDevice';

export type SelectBleDeviceProps = {
  serviceUuid: string;
  saveWifiDataServiceCharacteristicUuid: string;
  title: string;
  onDeviceSelection: (device, wifiDataService) => void;
};

export const SelectBleDevice = ({
  serviceUuid,
  saveWifiDataServiceCharacteristicUuid,
  title,
  onDeviceSelection
}: SelectBleDeviceProps) => {
  const [selectingDevice, setSelectingDevice] = useState<boolean>(false);
  const [device, setDevice] = useState<BLEDevice | undefined>(wizardController.context.device);
  const [alert, setAlert] = useState<string | undefined>();

  const selectDevice = async () => {
    setSelectingDevice(true);
    if ('bluetooth' in navigator) {
      try {
        let device;
        let wifiDataService;

        device = await navigator.bluetooth.requestDevice({
          acceptAllDevices: true,
          optionalServices: [serviceUuid]
        });
        const server = await device.gatt?.connect();
        const service = await server?.getPrimaryService(serviceUuid);
        wifiDataService = await service?.getCharacteristic(saveWifiDataServiceCharacteristicUuid);

        onDeviceSelection(device, wifiDataService);
        setDevice(device);
      } catch (error) {
        console.error(error);
        setAlert('We are unable to connect to the device, make sure the bluetooth is on');
      }
    } else {
      setAlert('Bluetooth is not available');
    }
    setSelectingDevice(false);
  };

  return (
    <Box sx={styles.deviceSelectionWrapper}>
      {selectingDevice ? (
        <CircularProgress />
      ) : (
        <>
          {alert !== undefined && device === undefined && (
            <CustomAlert message={alert} type={'error'} />
          )}
          <Box>
            <Typography variant="h6">{title}</Typography>
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
  );
};
