import { Box, Paper, Step, StepLabel, Stepper } from "@mui/material";
import styles from './wizardProgress.styles'
import { WizardProgressProps } from "./interfaces";

export const WizardProgress = ({steps, currentStep}: WizardProgressProps) => {

  return(
    <Paper elevation={3} sx={styles.wizardProgress}>
      <Stepper sx={styles.progress} activeStep={currentStep}>
        {
          steps.map(step => (
            <Step >
              <StepLabel>{step.title}</StepLabel>
            </Step>
          ))
        }
      </Stepper>
    </Paper>
  )
}