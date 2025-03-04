import { Box, Typography } from "@mui/material";
import { WizardStepProps } from "../../../common/wizard/interfaces";
import { CreatePlaceContext } from "../interfaces";


const Summary = ({wizardController}: WizardStepProps<CreatePlaceContext>) => {
  return (
    <Box>
      <Typography>
        Create Place Summary
      </Typography>
    </Box>
  )
}

export default Summary;