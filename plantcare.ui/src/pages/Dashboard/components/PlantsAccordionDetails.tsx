import {AccordionDetails, Box, IconButton, Typography} from "@mui/material";
import React from "react";
import {Place} from "../../../common/models/Place";
import styles from "../dashboard.styles"
import InsertChartIcon from '@mui/icons-material/InsertChart';
import {PlantType} from "../../../common/models/plantTypes";
import Decorative from "../../../app/images/Decorative.png";
import Fruit from "../../../app/images/Fruit.png";
import Vegetable from "../../../app/images/Vegetable.png";

interface PlantsAccordionDetailsProps{
    place: Place
}

export const PlantsAccordionDetails = (props: PlantsAccordionDetailsProps) => {

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
                            <Typography variant="overline">
                                {plant.name}
                            </Typography>
                            <Typography variant="body2">
                                {plant.description}
                            </Typography>
                        </Box>
                        <Box sx={styles.plantsAccordionDetailsButtons}>
                            <IconButton size="large" color="primary">
                                <InsertChartIcon />
                            </IconButton>
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