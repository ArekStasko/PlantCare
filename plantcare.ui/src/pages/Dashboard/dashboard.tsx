import React from "react";
import {
    Accordion,
    AccordionDetails,
    AccordionSummary,
    Box,
    Typography
} from "@mui/material";
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import { useGetPlacesQuery } from "../../common/slices/getPlaces/getPlaces";
import styles  from './dashboard.styles';
import CustomBackdrop from "../../common/compontents/customBackdrop/backdrop";

const Dashboard = () => {
    const [currentAccordion, setCurrentAccordion] = React.useState<number>();
    const {data, isLoading : placesLoading} = useGetPlacesQuery();


    return(
        <Box>
            isLoading ? (
                <CustomBackdrop isLoading={placesLoading} />
            ) : (
            data ? (
            <Box>
                {
                    data!.map(place => (
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
                                    place!.plants.map(plant => (
                                        <AccordionDetails>
                                            <Typography>
                                                {plant.name}
                                            </Typography>
                                            <Typography>
                                                {plant.description}
                                            </Typography>
                                        </AccordionDetails>
                                    ))
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
            ) : (
            <Box>
                <Typography>
                    For now you dont have any data
                </Typography>
            </Box>
            )
            )
        </Box>
    )
}

export default Dashboard;