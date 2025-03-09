import { Box, Button, Paper } from "@mui/material";
import styles from './WizardNavigation.styles'

export interface WizardNavigationProps {
  isValid: boolean;
  isFinalStep: boolean
  isFirstStep: boolean
  onBack: () => void
  onCancel: () => void
  onNext: () => void
}

export const WizardNavigation = ({isValid, isFinalStep, isFirstStep, onBack, onCancel, onNext}: WizardNavigationProps) => {

  return(
    <Paper elevation={3} sx={styles.wizardNavigation}>
      <Button disabled={isFirstStep} variant='contained' onClick={() => onBack()}>Back</Button>
      <Box>
        <Button variant='outlined' color='warning' onClick={() => onCancel()}>Cancel</Button>
        <Button disabled={!isValid} variant='contained' sx={styles.nextButton} onClick={() => onNext()}>
          {
            isFinalStep ? "Submit" : "Next"
          }
        </Button>
      </Box>
    </Paper>
  )
}