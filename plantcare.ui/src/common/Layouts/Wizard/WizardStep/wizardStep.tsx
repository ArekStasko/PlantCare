import React from "react";
import styles from "./wizardStep.styles";
import {Button, Card, CardActions, CardContent} from "@mui/material";
import {wizardStepProps} from '../interfaces';


export const WizardStep = ({children, nextStep, previousStep}: wizardStepProps) => {

    return(
            <Card sx={styles.card}>
                <CardContent>
                    {children}
                </CardContent>
                <CardActions>
                    <Button onClick={() => previousStep()} size="large">Back</Button>
                    <Button size="large">Cancel</Button>
                    <Button onClick={() => nextStep()} size="large">Proceed</Button>
                </CardActions>
            </Card>
    )
}

export default WizardStep;