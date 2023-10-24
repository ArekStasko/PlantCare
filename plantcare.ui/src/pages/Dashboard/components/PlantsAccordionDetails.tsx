import {AccordionDetails, Typography} from "@mui/material";
import React from "react";
import {Place} from "../../../common/models/Place";


interface PlantsAccordionDetailsProps{
    place: Place
}

export const PlantsAccordionDetails = (props: PlantsAccordionDetailsProps) => {

    return(
        <>
            {
                props.place.plants!.map(plant => (
                    <AccordionDetails>
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