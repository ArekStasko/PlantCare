import { Box } from '@mui/material';
import { ResponsiveLine } from '@nivo/line';
import StatisticsService from '../../../common/services/StatisticsService';
import React from 'react';
import { HumidityMeasurement } from '../../../common/models/HumidityMeasurement';

interface MeasurementsChartProps {
  humidityMeasurements: HumidityMeasurement[];
}

export const MeasurementsChart = (props: MeasurementsChartProps) => {
  const getSmallestRecord = (): number => {
    const recordsArray = props.humidityMeasurements.map((a) => a.humidity);
    return recordsArray.reduce((a, b) => Math.min(a, b));
  };

  return (
    <Box sx={{ height: '100%', width: '100%' }}>
      <ResponsiveLine
        data={StatisticsService.convertDataToStatistics(props.humidityMeasurements!)}
        margin={{ top: 40, right: 40, bottom: 40, left: 50 }}
        xScale={{ type: 'point' }}
        enableArea={true}
        areaBaselineValue={getSmallestRecord()}
        yScale={{
          type: 'linear',
          min: 'auto',
          max: 'auto',
          stacked: true,
          reverse: false
        }}
        yFormat=" >-.2f"
        axisBottom={{
          tickSize: 5,
          tickPadding: 5,
          tickRotation: 0,
          legend: 'Measurement Time',
          legendOffset: 36,
          legendPosition: 'middle'
        }}
        axisLeft={{
          tickSize: 5,
          tickPadding: 5,
          tickRotation: 0,
          legend: 'Humidity',
          legendOffset: -40,
          legendPosition: 'middle'
        }}
        theme={{
          axis: {
            ticks: {
              line: {
                stroke: 'white'
              },
              text: {
                fill: 'white'
              }
            }
          },
          crosshair: {
            line: {
              stroke: '#fff',
              strokeWidth: 2,
              strokeOpacity: 0.35
            }
          }
        }}
        pointSize={10}
        pointColor={{ theme: 'background' }}
        pointBorderWidth={2}
        pointBorderColor={{ from: 'serieColor' }}
        pointLabelYOffset={-12}
        useMesh={true}
      />
    </Box>
  );
};

export default MeasurementsChart;
