import { HumidityMeasurement } from '../models/HumidityMeasurement';
import { HumidityData, HumidityStatistics } from '../models/HumidityStatistics';
import DateService from './DateService';

const getHumidityMeasurementTime = (data: HumidityMeasurement[]): string[] =>
  data.map((c) => {
    const date = new Date(c.date);
    const hours = date.getHours();
    const minutes = date.getMinutes();

    const formattedHours = hours < 10 ? `0${hours}` : `${hours}`;
    const formattedMinutes = minutes < 10 ? `0${minutes}` : `${minutes}`;

    return `${formattedHours}:${formattedMinutes}`;
  });

const getHumidityMeasurementValues = (data: HumidityMeasurement[]): number[] =>
  data.map((c) => c.humidity);

export default {
  getHumidityMeasurementTime,
  getHumidityMeasurementValues
};
