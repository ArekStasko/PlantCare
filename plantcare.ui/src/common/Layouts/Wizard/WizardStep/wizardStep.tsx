import React from "react";
import styles from "./wizardStep.styles";
import {Box, Button, Card, CardActions, CardContent} from "@mui/material";
import {wizardStepProps} from '../interfaces';
import {useNavigate} from "react-router";
import routingConstants from "../../../../app/routing/routingConstants";
import CancelDialog from "../../../compontents/CancelDialog/cancelDialog";


export const WizardStep = ({children, nextStep, previousStep}: wizardStepProps) => {
    const [openDialog, setOpenDialog] = React.useState(false);

    return(
            <Card sx={styles.card}>
                <CancelDialog setOpenDialog={setOpenDialog} openDialog={openDialog} />
                <CardContent sx={styles.contentWrapper}>
                    {children}
                </CardContent>
                <CardActions sx={styles.buttonWrapper}>
                    <Button sx={styles.btn} variant="contained" onClick={() => previousStep()} size="medium">Back</Button>
                    <Box>
                        <Button sx={styles.btn} variant="outlined" size="medium" onClick={()=> setOpenDialog(!openDialog)}>Cancel</Button>
                        <Button sx={styles.btn} variant="contained" onClick={() => nextStep()} size="medium">Proceed</Button>
                    </Box>
                </CardActions>
            </Card>
    )
}

export default WizardStep;