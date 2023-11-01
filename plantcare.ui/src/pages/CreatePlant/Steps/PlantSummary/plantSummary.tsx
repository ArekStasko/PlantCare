import React, {useEffect} from "react";
import {Box, Typography} from "@mui/material";
import {ICreatePlantState} from "../../interfaces";


export const PlantSummary = ({state}: ICreatePlantState) => {

    return(
        <Box>
            <Typography>
                Plant Summary
            </Typography>
        </Box>
    )
}

export default PlantSummary;