import {Place} from "../../../common/models/Place";
import {Accordion, AccordionDetails, AccordionSummary, Box, Typography} from "@mui/material";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import React from "react";
import PlantsAccordionDetails from "./PlantsAccordionDetails";
import styles from '../dashboard.styles';


interface PlaceAccordionProps{
    data: Place[]
}

export const PlacesAccordion = (props: PlaceAccordionProps) => {
    const [currentAccordion, setCurrentAccordion] = React.useState<number>();

    return(
        <Box sx={styles.placesAccordionWrapper}>
            {
                props.data!.map(place => (
                    <Accordion
                        expanded={currentAccordion == place.id}
                        onChange={e => {
                            if(currentAccordion == place.id) setCurrentAccordion(undefined)
                            else setCurrentAccordion(place.id)
                        }}
                        key={place.id}
                        disableGutters
                    >
                        <AccordionSummary
                            expandIcon={<ExpandMoreIcon />}
                            aria-controls="panel1a-content"
                            id="panel1a-header"
                        >
                            <Typography>{place.name}</Typography>
                        </AccordionSummary>
                        {
                            place.plants ? (
                                <PlantsAccordionDetails place={place!} />
                            ) : (
                                <AccordionDetails>
                                    <Typography>
                                        There is no plants
                                    </Typography>
                                </AccordionDetails>
                            )
                        }
                    </Accordion>
                ))
            }
        </Box>
    )
}

export default PlacesAccordion;