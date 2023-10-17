import {Box, Skeleton, Typography} from "@mui/material";
import { useGetPlacesQuery } from "../../common/slices/getPlaces/getPlaces";

const Dashboard = () => {
    const {data, isLoading} = useGetPlacesQuery();


    return(
        isLoading ? (
            <Box>
                <Skeleton />
            </Box>
        ) : (
            <Box>
                <Typography>Test</Typography>
            </Box>
        )
    )
}

export default Dashboard;