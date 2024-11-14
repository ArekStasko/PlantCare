import { Typography, Box, Button } from "@mui/material";
import styles from './deviceSelection.styles';
import React from 'react';

export const DeviceSelection = () => {

  const handleConnectClick = async () => {
    try {
      const device = await navigator.bluetooth.requestDevice({
        acceptAllDevices: true, // Możesz również używać filtrów
        optionalServices: ['battery_service']
      });
      console.log("Znaleziono urządzenie:", device);
    } catch (error) {
      console.error("Błąd podczas wyszukiwania urządzeń Bluetooth:", error);
    }
  };

  return (
    <Box sx={styles.deviceSelectionWrapper}>
      <Typography variant="h6">Select module from device list</Typography>
      <Button onClick={handleConnectClick}>
        Bluetooth Test
      </Button>
    </Box>
  );
};

export default DeviceSelection;