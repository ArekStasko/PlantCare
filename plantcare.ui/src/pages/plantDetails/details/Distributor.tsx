import { Box, Button, CircularProgress, LinearProgress, Paper, Tooltip, Typography } from "@mui/material";
import styles from "./details.styles";
import RoutingPaths from "../../../app/routing/routingConstants";
import React from "react";
import {
  DistributorPlantRequest,
  useGetDistributorWithWaterSupplyQuery, useWaterSupplyMutation
} from "../../../common/RTK/Distributor/Distributor";
import { useNavigate } from "react-router";
import SyncIcon from "@mui/icons-material/Sync";

export type DistributorProps = {
  distributorId?: number;
  plantId?: number;
  moduleId?: number;
}

export const Distributor = ({distributorId, plantId, moduleId}: DistributorProps) => {
  const navigate = useNavigate();
  const [supplyWater, { isLoading: isWaterSupplyLoading }] = useWaterSupplyMutation();
  const { data: distributorWithWaterSupply, isFetching: isDistributorFetching } = useGetDistributorWithWaterSupplyQuery(
    {
      id: distributorId,
      plantId: plantId,
    } as DistributorPlantRequest,
    {
      pollingInterval: 10 * 60 * 1000
    }
  )

  const performWaterSupply = async () => {
    const request = {
      id: distributorId,
      plantId: plantId
    } as DistributorPlantRequest;

    await supplyWater(request);
  };

  const WaterSupplyInProgress = (isWaterSupply?: boolean) =>
        isWaterSupply ? (
          <Box sx={styles.waterSupplyProgress}>
            <Typography>
              Hydration in progress
            </Typography>
            <Box sx={styles.progress}>
              <LinearProgress />
            </Box>
          </Box>
        ) : (
          <Tooltip placement="top-end" title="Run hydration">
            <Button onClick={() => performWaterSupply()}>Hydrate</Button>
          </Tooltip>
        )

  return(
    <Box sx={styles.details_paper}>
      <Paper sx={styles.details_card}>
        {isDistributorFetching || isWaterSupplyLoading ? (
          <CircularProgress />
        ) : distributorWithWaterSupply ? WaterSupplyInProgress(distributorWithWaterSupply.isWaterSupplyActive) : (
          <Tooltip placement="top-end" title="Add distributor">
            <Button
              onClick={() =>
                navigate(`${RoutingPaths.addDistributor}/${plantId}/${moduleId}`)
              }
            >
              Add distributor
            </Button>
          </Tooltip>
        )}
      </Paper>
    </Box>
  )
}