import { Box, TextField, Typography } from "@mui/material";
import styles from './wifiForm.styles';
import { ModuleStepProps } from "../../addModule";
import { useFormContext } from "react-hook-form";
import React from "react";

const WifiForm = ({context}: ModuleStepProps) => {
  const {
    register,
    watch,
    formState: { errors }
  } = useFormContext();

  return(
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
    </Box>
  )
}

export default WifiForm