import React, { useEffect, useMemo, useState } from 'react';
import { Box, Pagination, Typography } from '@mui/material';
import { useGetPlacesQuery } from '../../common/RTK/getPlaces/getPlaces';
import styles from './dashboard.styles';
import CustomBackdrop from '../../common/components/customBackdrop/backdrop';
import PlacesAccordion from './components/PlacesAccordion';
import { useGetPlantsQuery } from '../../common/RTK/getPlants/getPlants';
import NoDataDialog from '../../common/components/NoDataAlert/noDataDialog';
import { Place } from '../../common/models/Place';

const Dashboard = () => {
  const [page, setPage] = useState<number>(1);
  const { data: places, isLoading: placesLoading } = useGetPlacesQuery();
  const { data: plants, isLoading: plantsLoading } = useGetPlantsQuery();
  const pagination = 4;

  const placesToRender = useMemo(() => {
    if (!places) return [];
    if (places.length < pagination) return places;
    const start = (page - 1) * pagination;
    const end = page * pagination;
    return places.slice(start, end);
  }, [page, places]);

  const checkCount = (count: number) => {
    if (count < page) setPage(count);
  };

  const paginationCount = useMemo(() => {
    if (!places) return 0;
    if (places.length % pagination === 0) {
      const count = Math.round(places.length / pagination);
      checkCount(count);
      return count;
    }

    if (places.length % pagination < pagination) {
      const count = Math.round(places.length / pagination) + 1;
      checkCount(count);
      return count;
    }
    return 0;
  }, [places]);

  return placesLoading || plantsLoading ? (
    <CustomBackdrop isLoading={placesLoading || plantsLoading} />
  ) : (
    <Box sx={styles.dashboardContainer}>
      {places && places.length !== 0 ? (
        <Box sx={styles.dashboardWrapper}>
          <Box sx={styles.accordionWrapper}>
            <PlacesAccordion places={placesToRender!} plants={plants!} />
          </Box>
          {places.length > pagination && (
            <Pagination
              count={paginationCount}
              page={page}
              onChange={(e, v) => setPage(v)}
              color="primary"
            />
          )}
        </Box>
      ) : (
        <Box sx={styles.alertWrapper}>
          <NoDataDialog />
        </Box>
      )}
    </Box>
  );
};

export default Dashboard;
