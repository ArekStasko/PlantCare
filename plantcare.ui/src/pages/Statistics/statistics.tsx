import { Box, CircularProgress } from '@mui/material';
import React from 'react';
import { useGetHumidityMeasurementsQuery } from '../../common/slices/getHumidityMeasurements/getHumidityMeasurements';
import { useParams } from 'react-router';
import DateService from '../../common/services/DateService';
import { ResponsiveBump } from '@nivo/bump';
import StatisticsService from '../../common/services/StatisticsService';

export const Statistics = () => {
  let { moduleId } = useParams();

  const { data: humidityMeasurements, isLoading: humidityMeasurementsLoading } =
    useGetHumidityMeasurementsQuery({
      moduleId: moduleId!,
      fromDate: DateService.getStartOfCurrentDay(),
      toDate: DateService.getEndOfCurrentDay()
    });

  return (
    <Box>
      {humidityMeasurementsLoading ? (
        <>
          <CircularProgress />
        </>
      ) : (
        <Box sx={{ height: '600px', width: '1500px', backgroundColor: 'white' }}>
          <ResponsiveBump
            data={StatisticsService.convertDataToStatistics(humidityMeasurements!)}
            colors={{ scheme: 'spectral' }}
            lineWidth={3}
            activeLineWidth={6}
            inactiveLineWidth={3}
            inactiveOpacity={0.15}
            pointSize={10}
            activePointSize={16}
            inactivePointSize={0}
            pointColor={{ theme: 'background' }}
            pointBorderWidth={3}
            activePointBorderWidth={3}
            pointBorderColor={{ from: 'serie.color' }}
            axisTop={null}
            axisBottom={{
              tickSize: 5,
              tickPadding: 5,
              tickRotation: 0,
              legend: '',
              legendPosition: 'middle',
              legendOffset: 32
            }}
            axisLeft={{
              tickSize: 5,
              tickPadding: 5,
              tickRotation: 0,
              legend: 'ranking',
              legendPosition: 'middle',
              legendOffset: -40
            }}
            margin={{ top: 40, right: 100, bottom: 40, left: 60 }}
            axisRight={null}
          />
        </Box>
      )}
    </Box>
  );
};

export default Statistics;
