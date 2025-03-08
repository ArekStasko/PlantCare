import { Box, Button, Paper } from "@mui/material";
import styles from './WizardNavigation.styles'

export interface WizardNavigationProps {
  isFinalStep: boolean
  isFirstStep: boolean
  onBack: () => void
  onCancel: () => void
  onNext: () => void
}

export const WizardNavigation = ({isFinalStep, isFirstStep, onBack, onCancel, onNext}: WizardNavigationProps) => {

  return(
    <Paper sx={styles.wizardNavigation}>
      <Button disabled={isFirstStep} onClick={() => onBack()}>Back</Button>
      <Box>
        <Button onClick={() => onCancel()}>Cancel</Button>
        <Button onClick={() => onNext()}>
          {
            isFinalStep ? "Submit" : "Next"
          }
        </Button>
      </Box>
    </Paper>
  )
}