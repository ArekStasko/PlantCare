import {Accordion, AccordionDetails, AccordionSummary, Box, Skeleton, Typography} from "@mui/material";
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import { useGetPlacesQuery } from "../../common/slices/getPlaces/getPlaces";
import React from "react";

const Dashboard = () => {
    const {data, isLoading} = useGetPlacesQuery();


    return(
        isLoading ? (
            <Box>
                <Skeleton />
            </Box>
        ) : (
            data ? (
                <Box>
                    {
                        data.map(place => (
                            <Accordion key={place.id}>
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
    )
}

export default Dashboard;