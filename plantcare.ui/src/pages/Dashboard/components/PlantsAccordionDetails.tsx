import {AccordionDetails, Typography} from "@mui/material";
import React from "react";
import {Place} from "../../../common/models/Place";
import styles from "../dashboard.styles"


interface PlantsAccordionDetailsProps{
    place: Place
}

export const PlantsAccordionDetails = (props: PlantsAccordionDetailsProps) => {

    return(
        <>
            {
                props.place.plants!.map(plant => (
                    <AccordionDetails
                        sx={styles.plantsAccordionDetailsWrapper}
                    >
                        <Typography>
                            {plant.name}
                        </Typography>
                        <Typography>
                            {plant.description}
                        </Typography>
                    </AccordionDetails>
                ))
            }
        </>
    )
}

export default PlantsAccordionDetails;