import { Box } from '@mui/material';
import { ResponsiveLine } from '@nivo/line';
import StatisticsService from '../../../common/services/StatisticsService';
import React, { useEffect } from 'react';
import { HumidityMeasurement } from '../../../common/models/HumidityMeasurement';
import { BarChart } from "@mui/x-charts";

interface MeasurementsChartProps {
  humidityMeasurements: HumidityMeasurement[];
}

export const MeasurementsChart = (props: MeasurementsChartProps) => {
  const getSmallestRecord = (): number => {
    const recordsArray = props.humidityMeasurements.map((a) => a.humidity);
    return recordsArray.reduce((a, b) => Math.min(a, b)) + 1;
  };

  return (
    <BarChart
      xAxis={[{ scaleType: 'band', data: StatisticsService.getHumidityMeasurementTime(props.humidityMeasurements) }]}
      series={[{ data: StatisticsService.getHumidityMeasurementValues(props.humidityMeasurements) }]}
    />
  );
};

export default MeasurementsChart;
