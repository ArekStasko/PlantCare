import { Place } from '../../../common/models/Place';
import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
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
import DeleteForeverIcon from '@mui/icons-material/DeleteForever';
import DeleteDialog from '../../../common/compontents/DeleteIcon/DeleteDialog/deleteDialog';
import { useGetPlacesQuery } from '../../../common/slices/getPlaces/getPlaces';
import DeleteIcon from '../../../common/compontents/DeleteIcon/deleteIcon';

interface PlaceAccordionProps {
  data: Place[];
}

export const PlacesAccordion = (props: PlaceAccordionProps) => {
  const [currentAccordion, setCurrentAccordion] = React.useState<number>();
  const navigate = useNavigate();

  return (
    <Box sx={styles.placesAccordionWrapper}>
      {props.data!.map((place) => (
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
          disableGutters>
          <AccordionSummary
            expandIcon={<ExpandMoreIcon />}
            aria-controls="panel1a-content"
            id="panel1a-header">
            <Box sx={styles.placesAccordionSummary}>
              <Typography variant="h6">{place.name}</Typography>
              <Box>
                <DeleteIcon
                  key={place.id}
                  resourceType="place"
                  resourceId={place.id}
                  resourceName={place.name}
                />
                <Tooltip title={`Update ${place.name}`} arrow>
                  <IconButton
                    onClick={() => navigate(`${RoutingConstants.updatePlace}/${place.id}`)}
                    size="large"
                    sx={{ mr: 5 }}
                    color="primary">
                    <BorderColorIcon />
                  </IconButton>
                </Tooltip>
              </Box>
            </Box>
          </AccordionSummary>
          {place.plants ? (
            <PlantsAccordionDetails place={place!} />
          ) : (
            <AccordionDetails>
              <Typography>There is no plants</Typography>
            </AccordionDetails>
          )}
        </Accordion>
      ))}
    </Box>
  );
};

export default PlacesAccordion;
