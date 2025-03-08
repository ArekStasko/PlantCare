import { Box, TextField, Typography } from "@mui/material";
import { WizardStepProps } from "../../../common/wizard/interfaces";
import { CreatePlaceContext } from "../interfaces";
import styles from "../../components/placeWizardSteps/Details/details.styles";
import React from "react";


const Details = ({wizardController}: WizardStepProps<CreatePlaceContext>) => {
  return (
    <Box>
      <Typography variant="h6">Enter the name of the place</Typography>
      <TextField
        sx={styles.placeName}
        label="Name"
        id="name"
        variant="filled"
      />
    </Box>
  )
}

export default Details;