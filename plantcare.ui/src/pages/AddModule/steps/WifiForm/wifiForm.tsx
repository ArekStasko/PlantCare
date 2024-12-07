import { Box, Button, CircularProgress, TextField, Typography } from "@mui/material";
import styles from './wifiForm.styles';
import { ModuleStepProps } from "../../addModule";
import { useFormContext } from "react-hook-form";
import React, { useState } from "react";
import CustomAlert from "../../../../common/compontents/customAlert/customAlert";
import {v4 as uuidv4} from 'uuid';

const WifiForm = ({context}: ModuleStepProps) => {
  const [connecting, setConnecting] = useState(false);
  const [alert, setAlert] = useState<string | undefined>();
  const {
    getValues,
    setValue,
    register,
    watch,
    formState: { errors }
  } = useFormContext();

  const sendWiFiData = async () => {
    setConnecting(true)
    try {
      const crc = context.characteristic;
      if (crc) {
        const name = getValues("wifiName");
        const psw = getValues("wifiPassword");
        const encoder = new TextEncoder();
        const uuid = uuidv4();
        const data = encoder.encode(`${name}|${psw}|${uuid}`);
        await crc.writeValue(data);
        setValue("connected", true);
        setAlert(undefined)
      }

      if(!crc){
        setAlert("Something went wrong while connecting to WiFi network")
      }
    } catch (error) {
      setValue("connected", false);
      setAlert("Something went wrong while connecting to WiFi network")
    }
    setConnecting(false)
  }

  return(
    <>
      {watch('connected') && (
        <CustomAlert message={"Successfully connected to WiFi"} type={"success"} />
      )}
    {alert !== undefined && (
      <CustomAlert message={alert} type={"error"} />
    )}
    <Box sx={styles.container}>
      <Typography variant="h6">Provide your WiFi network information</Typography>
      <TextField
        sx={styles.textfield}
        label="Full Name"
        id="wifiName"
        error={!!errors.wifiName}
        helperText={errors.wifiName && "WiFi name is required"}
        variant="filled"
        disabled={watch('connected')}
        {...register('wifiName')}
      />
      <TextField
        sx={styles.textfield}
        label="Password"
        id="wifiPassword"
        type="password"
        error={!!errors.wifiPassword}
        helperText={errors.wifiPassword && "WiFi password is required"}
        variant="filled"
        disabled={watch('connected')}
        {...register('wifiPassword')}
      />
      {
        connecting ? (
          <CircularProgress />
        ) : (
          <Button
            variant="outlined"
            size="medium"
            sx={styles.connectBtn}
            onClick={() => sendWiFiData()}
            disabled={!watch('wifiPassword') || !watch('wifiName') || watch('connected')}
          >
            Connect
          </Button>
        )
      }
    </Box>
    </>
  )
}

export default WifiForm