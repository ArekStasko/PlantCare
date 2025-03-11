import { Box, Button, Paper, Typography } from "@mui/material";
import React from "react";
import styles from "./wizardStep.styles";
import { wizardStepProviderProps } from "../../interfaces";

export const WizardStep = <T,>({children, isValid, nextButton, cancelButton, backButton, title, sx}: wizardStepProviderProps<T>) => {

  return (
    <Box sx={{...sx, ...styles.stepWrapper}}>
          <Paper sx={styles.stepStyles} elevation={3}>
            <Typography variant="h5">
              {title}
            </Typography>
            {children}
          </Paper>
      <Paper elevation={3} sx={styles.wizardNavigation}>
        <Button disabled={backButton.isDisabled} variant='contained' onClick={() => backButton.onClick()}>{backButton.title}</Button>
        <Box>
          <Button disabled={cancelButton.isDisabled} variant='outlined' color='warning' onClick={() => cancelButton.onClick()}>{cancelButton.title}</Button>
          <Button disabled={nextButton..isDisabled} variant='contained' sx={styles.nextButton} onClick={() => nextButton.onClick()}>
              {nextButton.title}
          </Button>
        </Box>
      </Paper>
    </Box>
  )
}