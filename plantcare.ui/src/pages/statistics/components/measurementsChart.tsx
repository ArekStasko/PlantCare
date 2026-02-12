import StatisticsService from '../../../common/services/StatisticsService';
import React from 'react';
import { HumidityMeasurement } from '../../../common/models/HumidityMeasurement';
import { BarChart } from '@mui/x-charts';

interface MeasurementsChartProps {
  humidityMeasurements: HumidityMeasurement[];
}

export const MeasurementsChart = (props: MeasurementsChartProps) => {

  return (
    <BarChart
      xAxis={[
        {
          scaleType: 'band',
          data: StatisticsService.getHumidityMeasurementTime(props.humidityMeasurements)
        }
      ]}
      series={[
        {
          data: StatisticsService.getHumidityMeasurementValues(props.humidityMeasurements),
          valueFormatter: (v) => `Humidity: ${v}%`,
        }
      ]}
    />
  );
};

export default MeasurementsChart;
