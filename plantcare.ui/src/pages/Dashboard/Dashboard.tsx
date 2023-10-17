import {Box, Typography} from "@mui/material";
import { useGetPlacesQuery } from "../../common/slices/getPlaces/getPlaces";

const Dashboard = () => {
    const {data, isLoading} = useGetPlacesQuery();
    

    return(
        <Box>
            <Typography>Test</Typography>
        </Box>
    )
}

export default Dashboard;