import { Box } from "@mui/material";
import styles from './wizardProgress.styles'
import { WizardProgressProps } from "./interfaces";
import { ProgressTile } from "./progressTile";

export const WizardProgress = ({steps, currentStep}: WizardProgressProps) => {

  return(
    <Box sx={styles.wizardProgress}>
      <Box sx={styles.progress}>
        {
          steps.map(step => (
            <ProgressTile title={step.title} active={step.order === currentStep} completed={step.order < currentStep} />
          ))
        }
      </Box>
    </Box>
  )
}