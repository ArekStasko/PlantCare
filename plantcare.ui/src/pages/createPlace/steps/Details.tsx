import { Box, TextField, Typography } from "@mui/material";
import { WizardStepProps } from "../../../common/wizard/interfaces";
import { CreatePlaceContext } from "../interfaces";
import styles from "../../components/placeWizardSteps/Details/details.styles";
import React, { useMemo } from "react";
import { WizardStep } from "../../../common/wizard/components/wizardStep/WizardStep";


const Details = ({wizardController}: WizardStepProps<CreatePlaceContext>) => {

  return (
    <WizardStep<CreatePlaceContext>
      wizardController={wizardController}
      isValid={true}
      isFinal={false}
      title="Details"
      sx={styles.detailsContainer}>
      <Box sx={styles.placeDetailsWrapper}>
        <Typography variant="h6">Enter the name of the place</Typography>
        <TextField
          sx={styles.placeName}
          label="Name"
          id="name"
          variant="filled"
        />
      </Box>
    </WizardStep>
  )
}

export default Details;