import { Box, Button, Paper } from "@mui/material";
import styles from './WizardNavigation.styles'

export interface WizardNavigationProps {
  onBack: () => void
  onCancel: () => void
  onNext: () => void
}

export const WizardNavigation = ({onBack, onCancel, onNext}: WizardNavigationProps) => {

  return(
    <Paper sx={styles.wizardNavigation}>
      <Button onClick={() => onBack()}>Back</Button>
      <Box>
        <Button onClick={() => onCancel()}>Cancel</Button>
        <Button onClick={() => onNext()}>Next</Button>
      </Box>
    </Paper>
  )
}