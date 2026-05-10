import { AccordionDetails, Box, CircularProgress, IconButton, Tooltip, Typography } from "@mui/material";
import React from "react";
import { useNavigate } from "react-router";
import InsertChartIcon from "@mui/icons-material/InsertChart";
import Decorative from "../../../app/images/Decorative.png";
import EditIcon from "@mui/icons-material/Edit";
import Fruit from "../../../app/images/Fruit.png";
import Vegetable from "../../../app/images/Vegetable.png";
import { ShrinkText } from "../../../common/services/TextService";
import styles from "../dashboard.styles";
import RoutingConstants from "../../../app/routing/routingConstants";
import PlantActionsMenu from "../../plantActionsMenu/PlantActionsMenu";
import { HumidityStatus, Plant, PlantType } from "@arekstasko/plantcare-api-client";
import { useGetHumidityStatusQuery } from "../../../common/RTK/Place/Place";
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import ErrorIcon from '@mui/icons-material/Error';
import WarningIcon from '@mui/icons-material/Warning';
import HelpIcon from '@mui/icons-material/Help';

interface PlantsAccordionDetailsProps {
  plants: Plant[];
  placeId: number;
}

export const PlantsAccordionDetails = (props: PlantsAccordionDetailsProps) => {
  const { data: humidityStatuses, isLoading } = useGetHumidityStatusQuery(props.placeId);
  const navigate = useNavigate();
  const [openPlantId, setOpenPlantId] = React.useState<number>();

  const getImage = (plantType?: PlantType) => {
    switch (plantType) {
      case PlantType._1:
        return Decorative;
      case PlantType._2:
        return Fruit;
      case PlantType._3:
        return Vegetable;
      default:
        return 'Not Specified';
    }
  };

  const getStatusIcon = (status?: HumidityStatus) => {
    switch (status) {
      case HumidityStatus._1:
        return <CheckCircleIcon color="success" />;
      case HumidityStatus._2:
        return <WarningIcon color="warning" />;
      case HumidityStatus._3:
        return <ErrorIcon color="error" />;
      case HumidityStatus._4:
      default:
        return <HelpIcon color="action" />;
    }
  };

  return isLoading ? (
    <CircularProgress />
  ) : (
    <>
      {props.plants!.map((plant) => {
        const plantStatus = humidityStatuses?.find((s: any) => s.plantId === plant.id)?.status;

        return (
          <AccordionDetails key={plant.id} sx={styles.plantsAccordionDetailsWrapper}>
            <Box sx={styles.plantsAccordionDetailsInfo}>
              <Typography sx={{ mr: 5 }} variant="overline">
                {plant.name}
              </Typography>
              <Typography sx={{ ml: 5 }} variant="body2">
                {ShrinkText(plant.description)}
              </Typography>
            </Box>

            <PlantActionsMenu
              plant={plant}
              closeDialog={() => setOpenPlantId(undefined)}
              openDialog={openPlantId === plant.id}
            />

            <Box sx={styles.plantsAccordionDetailsButtons}>
              <Tooltip title={`Update ${plant.name}`} arrow>
                <IconButton
                  onClick={() => setOpenPlantId(plant.id)}
                  size="large"
                  sx={{ mr: 5 }}
                  color="primary"
                >
                  <EditIcon />
                </IconButton>
              </Tooltip>

              <Tooltip title={`Show Statistics of ${plant.name}`} arrow>
                <IconButton
                  onClick={() =>
                    navigate(`${RoutingConstants.plantDetails}/${plant.id}/${plant.moduleId}`)
                  }
                  size="large"
                  sx={{ mr: 5 }}
                  color="primary"
                >
                  <InsertChartIcon />
                </IconButton>
              </Tooltip>
              <Tooltip title={`Humidity Status: ${plantStatus ?? 'Unknown'}`} arrow>
                <IconButton disableRipple sx={{ cursor: 'default' }}>
                  {getStatusIcon(plantStatus)}
                </IconButton>
              </Tooltip>
              <Tooltip title={plant.type ? PlantType[plant.type] : 'Not Specified'} arrow>
                <Box
                  component="img"
                  sx={{
                    height: 50,
                    width: 50,
                    maxHeight: { xs: 50, md: 50 },
                    maxWidth: { xs: 50, md: 50 },
                    borderRadius: 2,
                    ml: 2
                  }}
                  alt="Plant_Type"
                  src={getImage(plant.type)}
                />
              </Tooltip>
            </Box>
          </AccordionDetails>
        );
      })}
    </>
  );
};

export default PlantsAccordionDetails;