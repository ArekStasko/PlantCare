import { BarChart } from '@mui/x-charts';
import { HumidityMeasurement } from '../../../../common/models/HumidityMeasurement';
import StatisticsService from '../../../../common/services/StatisticsService';

interface MeasurementsChartProps {
  humidityMeasurements: HumidityMeasurement[];
}

const GeneralHumidityMeasurementsChart = ({ humidityMeasurements }: MeasurementsChartProps) => {
  return (
    <BarChart
      xAxis={[
        {
          scaleType: 'band',
          data: StatisticsService.getHumidityMeasurementTime(humidityMeasurements)
        }
      ]}
      series={[
        {
          data: StatisticsService.getHumidityMeasurementValues(humidityMeasurements),
          valueFormatter: (v) => `Humidity: ${v}%`
        }
      ]}
    />
  );
};

export default GeneralHumidityMeasurementsChart;
