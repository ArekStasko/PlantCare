import React from 'react';
import { Box, CircularProgress, Typography } from '@mui/material';
import styles from './summary.styles';
import { useFormContext } from 'react-hook-form';
import { useGetPlacesQuery } from '../../../../common/RTK/getPlaces/getPlaces';
import { ShrinkText } from '../../../../common/services/TextService';
import { PlantType } from '../../../../common/models/plantTypes';

export const Summary = () => {
  const { data: places, isLoading: placesLoading } = useGetPlacesQuery();
  const { getValues } = useFormContext();
  const titlesToDisplay = ['Name', 'Description', 'Type of Plant', 'Place', 'Module'];

  const renderTitles = () => titlesToDisplay.map((t) => <Typography key={t}>{t}</Typography>);
  const getPlaceName = () => places!.find((place) => place.id === +getValues('plantPlace'))!.name;

  return (
    <Box sx={styles.summaryWrapper}>
      <Typography variant="h6">Plant Summary</Typography>
      {placesLoading ? (
        <CircularProgress />
      ) : (
        <Box sx={styles.details}>
          <Box sx={styles.titleWrapper}>{renderTitles()}</Box>
          <Box sx={styles.dataWrapper}>
            <Typography>{getValues('name')}</Typography>
            <Typography sx={{ textAlign: 'end' }}>
              {ShrinkText(getValues('description'))}
            </Typography>
            <Typography>{PlantType[getValues('plantType')]}</Typography>
            <Typography>{getPlaceName()}</Typography>
            <Typography>{getValues('plantModule')}</Typography>
          </Box>
        </Box>
      )}
    </Box>
  );
};

export default Summary;
