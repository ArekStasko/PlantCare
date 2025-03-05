import { Box, Button, Paper } from "@mui/material";
import styles from './WizardNavigation.styles'

export const WizardNavigation = () => {

  return(
    <Paper sx={styles.wizardNavigation}>
      <Button>Back</Button>
      <Box>
        <Button>Cancel</Button>
        <Button>Next</Button>
      </Box>
    </Paper>
  )
}