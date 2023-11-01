import React, {useEffect} from "react";
import {Box, Typography} from "@mui/material";
import {ICreatePlantState} from "../../interfaces";



export const PlantDetails = ({state}: ICreatePlantState) => {

    return(
        <Box>
            <Typography>
                Plant Details
            </Typography>
        </Box>
    )
}

export default PlantDetails;