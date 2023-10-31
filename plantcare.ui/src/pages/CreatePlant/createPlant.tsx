import React from "react";
import {Box, Container, Step, StepLabel, Stepper, Typography} from "@mui/material"
import CardLayout from "../../common/Layouts/CardLayout/cardLayout";
import styles from './createPlant.styles'

export const CreatePlant = () => {

    return(
        <Container sx={styles.container}>
            <Box sx={styles.contentWrapper}>
                <Stepper sx={styles.stepper}>
                    <Step>
                        <StepLabel>Test step label 1</StepLabel>
                    </Step>
                    <Step>
                        <StepLabel>Test step label 2</StepLabel>
                    </Step>
                    <Step>
                        <StepLabel>Test step label 3</StepLabel>
                    </Step>
                </Stepper>
                <CardLayout>
                    <Typography>
                        Test
                    </Typography>
                </CardLayout>
            </Box>
        </Container>
    )
}

export default CreatePlant;