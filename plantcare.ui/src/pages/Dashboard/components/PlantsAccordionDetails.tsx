import {AccordionDetails, Box, IconButton, Tooltip, Typography} from "@mui/material";
import React from "react";
import {useNavigate} from "react-router";
import InsertChartIcon from '@mui/icons-material/InsertChart';
import {PlantType} from "../../../common/models/plantTypes";
import Decorative from "../../../app/images/Decorative.png";
import Fruit from "../../../app/images/Fruit.png";
import Vegetable from "../../../app/images/Vegetable.png";
import {ShrinkText} from "../../../common/services/TextService";
import {Place} from "../../../common/models/Place";
import styles from "../dashboard.styles"
import RoutingConstants from "../../../app/routing/routingConstants";

interface PlantsAccordionDetailsProps{
    place: Place
}

export const PlantsAccordionDetails = (props: PlantsAccordionDetailsProps) => {
    const navigate = useNavigate();

    const getImage = (plantType: PlantType) => {
        switch (plantType){
            case PlantType.Decorative:
                return Decorative
            case PlantType.Fruit:
                return Fruit
            case PlantType.Vegetable:
                return Vegetable
        }
    }

    return(
        <>
            {
                props.place.plants!.map(plant => (
                    <AccordionDetails
                        sx={styles.plantsAccordionDetailsWrapper}
                    >
                        <Box sx={styles.plantsAccordionDetailsInfo}>
                            <Typography sx={{mr: 5}} variant="overline">
                                {plant.name}
                            </Typography>
                            <Typography sx={{ml: 5}} variant="body2">
                                {ShrinkText(plant.description)}
                            </Typography>
                        </Box>
                        <Box sx={styles.plantsAccordionDetailsButtons}>
                            <Tooltip title={`Show Statistics of ${plant.name}`}>
                                <IconButton onClick={() => navigate(`${RoutingConstants.plantStatistics}/${plant.id}`)} size="large" sx={{mr: 5}} color="primary">
                                    <InsertChartIcon />
                                </IconButton>
                            </Tooltip>
                            <Box
                                component="img"
                                sx={{
                                    height: 50,
                                    width: 50,
                                    maxHeight: { xs: 50, md: 50 },
                                    maxWidth: { xs: 50, md: 50 },
                                    borderRadius: 2
                                }}
                                alt="Plant_Type"
                                src={getImage(plant.type)}
                            />
                        </Box>
                    </AccordionDetails>
                ))
            }
        </>
    )
}

export default PlantsAccordionDetails;