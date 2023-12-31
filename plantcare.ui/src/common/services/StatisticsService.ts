import { HumidityMeasurement } from '../models/HumidityMeasurement';
import { HumidityData, HumidityStatistics } from '../models/HumidityStatistics';
import DateService from './DateService';

const convertDataToStatistics = (
  data: HumidityMeasurement[]
): { id: string; data: { x: string; y: number }[] }[] => {
  const humidityStatistics = [
    {
      id: 'Humidity Measurements',
      data: data.map((measurement) => {
        return {
          x: DateService.convertDatesToStrings(measurement.date),
          y: measurement.humidity
        } as HumidityData;
      })
    }
  ] as HumidityStatistics[];

  return humidityStatistics as { id: string; data: { x: string; y: number }[] }[];
};

export default {
  convertDataToStatistics
};
