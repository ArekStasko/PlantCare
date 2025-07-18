import { AccordionDetails, Box, IconButton, Tooltip, Typography } from '@mui/material';
import React from 'react';
import { useNavigate } from 'react-router';
import InsertChartIcon from '@mui/icons-material/InsertChart';
import { PlantType } from '../../../common/models/plantTypes';
import Decorative from '../../../app/images/Decorative.png';
import EditIcon from '@mui/icons-material/Edit';
import Fruit from '../../../app/images/Fruit.png';
import Vegetable from '../../../app/images/Vegetable.png';
import { ShrinkText } from '../../../common/services/TextService';
import styles from '../dashboard.styles';
import RoutingConstants from '../../../app/routing/routingConstants';
import { Plant } from '../../../common/models/Plant';
import PlantActionsMenu from '../../plantActionsMenu/PlantActionsMenu';

interface PlantsAccordionDetailsProps {
  plants: Plant[];
}

export const PlantsAccordionDetails = (props: PlantsAccordionDetailsProps) => {
  const navigate = useNavigate();
  const [openPlantId, setOpenPlantId] = React.useState<number>();

  const getImage = (plantType: PlantType) => {
    switch (plantType) {
      case PlantType.Decorative:
        return Decorative;
      case PlantType.Fruit:
        return Fruit;
      case PlantType.Vegetable:
        return Vegetable;
    }
  };

  return (
    <>
      {props.plants!.map((plant) => (
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
                onClick={() => navigate(`${RoutingConstants.plantStatistics}/${plant.moduleId}`)}
                size="large"
                sx={{ mr: 5 }}
                color="primary"
              >
                <InsertChartIcon />
              </IconButton>
            </Tooltip>
            <Tooltip title={PlantType[plant.type]} arrow>
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
            </Tooltip>
          </Box>
        </AccordionDetails>
      ))}
    </>
  );
};

export default PlantsAccordionDetails;
