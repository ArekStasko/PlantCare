import { Box, Typography } from "@mui/material";
import { WizardStepProps } from "../../../common/wizard/interfaces";
import { CreatePlaceContext } from "../interfaces";


const Details = ({wizardController}: WizardStepProps<CreatePlaceContext>) => {
  return (
    <Box>
      <Typography>
        Create Place Details
      </Typography>
    </Box>
  )
}

export default Details;