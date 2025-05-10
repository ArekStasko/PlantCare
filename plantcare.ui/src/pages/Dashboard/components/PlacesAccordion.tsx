import { Place } from '../../../common/models/Place';
import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  Alert,
  Box,
  IconButton,
  Tooltip,
  Typography
} from '@mui/material';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import React from 'react';
import PlantsAccordionDetails from './PlantsAccordionDetails';
import styles from '../dashboard.styles';
import RoutingConstants from '../../../app/routing/routingConstants';
import BorderColorIcon from '@mui/icons-material/BorderColor';
import { useNavigate } from 'react-router';
import { Plant } from '../../../common/models/Plant';
import PlaceActionsMenu from "../../placeActionsMenu/PlaceActionsMenu";

interface PlaceAccordionProps {
  places: Place[];
  plants: Plant[];
}

export const PlacesAccordion = (props: PlaceAccordionProps) => {
  const [currentAccordion, setCurrentAccordion] = React.useState<number>();
  const [openPlaceActionsMenu, setOpenPlaceActionMenu] = React.useState(false);
  const navigate = useNavigate();

  const filterPlantsByPlaceId = (placeId: number) => {
    return props.plants.filter((p) => p.placeId === placeId);
  };

  return (
    <Box sx={styles.placesAccordionWrapper}>
      {props.places!.map((place) => (
        <Accordion
          sx={{
            border: '1px solid black'
          }}
          expanded={currentAccordion == place.id}
          onChange={(e) => {
            if (currentAccordion == place.id) setCurrentAccordion(undefined);
            else setCurrentAccordion(place.id);
          }}
          key={place.id}
          disableGutters
        >
          <PlaceActionsMenu openDialog={openPlaceActionsMenu} setOpenDialog={(open: boolean) => setOpenPlaceActionMenu(open)} place={place}/>
          <AccordionSummary
            expandIcon={<ExpandMoreIcon />}
            aria-controls="panel1a-content"
            id="panel1a-header"
          >
            <Box sx={styles.placesAccordionSummary}>
              <Typography variant="h6">{place.name}</Typography>
              <Box>
                <Tooltip title={`Update ${place.name}`} arrow>
                  <IconButton
                    onClick={() => setOpenPlaceActionMenu(true)}
                    size="large"
                    sx={{ mr: 5 }}
                    color="primary"
                  >
                    <BorderColorIcon />
                  </IconButton>
                </Tooltip>
              </Box>
            </Box>
          </AccordionSummary>
          {props.plants && filterPlantsByPlaceId(place.id).length !== 0 ? (
            <PlantsAccordionDetails plants={filterPlantsByPlaceId(place.id)!} />
          ) : (
            <AccordionDetails>
              <Alert variant="outlined" severity="warning">
                There is no plants assigned to {place.name}
              </Alert>
            </AccordionDetails>
          )}
        </Accordion>
      ))}
    </Box>
  );
};

export default PlacesAccordion;
