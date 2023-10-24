import React from "react";
import {
    Box,
    Typography
} from "@mui/material";
import { useGetPlacesQuery } from "../../common/slices/getPlaces/getPlaces";
import styles  from './dashboard.styles';
import CustomBackdrop from "../../common/compontents/customBackdrop/backdrop";
import PlacesAccordion from "./components/PlacesAccordion";

const Dashboard = () => {
    const {data: places, isLoading : placesLoading} = useGetPlacesQuery();


    return(
        <Box>
            isLoading ? (
                <CustomBackdrop isLoading={placesLoading} />
            ) : (
            places ? (
                <PlacesAccordion data={places!} />
            ) : (
            <Box>
                <Typography>
                    you dont have any data
                </Typography>
            </Box>
            )
            )
        </Box>
    )
}

export default Dashboard;