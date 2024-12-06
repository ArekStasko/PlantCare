import { Typography, Box, Button, CircularProgress } from "@mui/material";
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import styles from './deviceSelection.styles';
import React, { useState } from "react";
import CustomAlert from "../../../../common/compontents/customAlert/customAlert";
import { BLEDevice } from "../../../../common/models/BLEDevice";
import { DeviceContext } from "../../addModule";

export interface DeviceSelectionProps {
  deviceContext: DeviceContext;
}

export const DeviceSelection = ({deviceContext}: DeviceSelectionProps) => {
  const [device, setDevice] = useState<BLEDevice | undefined>(deviceContext.device);
  const [characteristic, setcharacteristic] = useState<BluetoothRemoteGATTCharacteristic | undefined>(deviceContext.characteristic);
  const [alert, setAlert] = useState<string | undefined>();
  const [selectingDevice, setSelectingDevice] = useState<boolean>(false);

  const selectDevice = async () => {
    setSelectingDevice(true);
    if ('bluetooth' in navigator) {
      try {
        let device;
        let characteristic;

        const serviceUuid = '00000180-0000-1000-8000-00805f9b34fb';
        const characteristicUuid = '0000dead-0000-1000-8000-00805f9b34fb';

        device = await navigator.bluetooth.requestDevice({
          acceptAllDevices: true,
          optionalServices: [serviceUuid]
        });

        const server = await device.gatt?.connect();
        const service = await server?.getPrimaryService(serviceUuid);
        characteristic = await service?.getCharacteristic(characteristicUuid);
        setDevice(device);
        setcharacteristic(characteristic)
        deviceContext.device = device;
        deviceContext.characteristic = characteristic;
      } catch (error) {
        setAlert("We are unable to connect to the device, make sure the bluetooth is on")
      }
    } else {
      setAlert("Bluetooth is not available");
    }
    setSelectingDevice(false);
  };


  return (
    <Box sx={styles.deviceSelectionWrapper}>
      {
        selectingDevice ? (
          <CircularProgress />
          ) : (
          <>
            {alert !== undefined && device === undefined && (
              <CustomAlert message={alert} type={"error"} />
            )}
            <Box>
              <Typography variant="h6">Select plantcare module from device list</Typography>
              <Typography variant="subtitle1">Make sure that bluetooth is turned on</Typography>
            </Box>
            <Button onClick={selectDevice}>
              Select device
            </Button>
            {device && (
                <Card sx={{ minWidth: 275 }}>
                  <CardContent>
                    <Typography>
                      Paired Device:
                    </Typography>
                    <Typography component="div">
                      {device.name}
                    </Typography>
                  </CardContent>
                </Card>
            )}
          </>
        )
      }
    </Box>
  );
};

export default DeviceSelection;