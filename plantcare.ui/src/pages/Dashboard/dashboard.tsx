import React from "react";
import {
    Accordion,
    AccordionDetails,
    AccordionSummary, Backdrop,
    Box,
    CircularProgress,
    Skeleton,
    Typography
} from "@mui/material";
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import { useGetPlacesQuery } from "../../common/slices/getPlaces/getPlaces";
import styles  from './dashboard.styles';

const Dashboard = () => {
    const [currentAccordion, setCurrentAccordion] = React.useState<number>();
    const {data, isLoading} = useGetPlacesQuery();


    return(
        isLoading ? (
            <Backdrop
                sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
                open={isLoading}
            >
                <CircularProgress color="inherit" />
            </Backdrop>
        ) : (
            data ? (
                <Box>
                    {
                        data.map(place => (
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
    )
}

export default Dashboard;