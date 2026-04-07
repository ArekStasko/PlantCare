import { AverageHumidity, IHumidityMeasurement } from '@arekstasko/plantcare-api-client';

const getHumidityMeasurementTime = (data: IHumidityMeasurement[]): string[] =>
  data.map((c) => {
    if (!c.measurementDate) return '';
    const hours = c.measurementDate.getHours();
    const minutes = c.measurementDate.getMinutes();

    const formattedHours = hours < 10 ? `0${hours}` : `${hours}`;
    const formattedMinutes = minutes < 10 ? `0${minutes}` : `${minutes}`;

    return `${formattedHours}:${formattedMinutes}`;
  });

const getHumidityMeasurementValues = (data: IHumidityMeasurement[]): number[] =>
  data.map((c) => (c.humidity ? c.humidity : 0));

const getAverageHumidityMeasurementTime = (data: AverageHumidity[]): string[] =>
  data.map((c) => {
    if (c.date === undefined) return 'Date unknown';
    return c.date;
  });

const getAverageHumidityMeasurementValues = (data: AverageHumidity[]): number[] =>
  data.map((c) => (c.humidity ? c.humidity : 0));

export default {
  getHumidityMeasurementTime,
  getHumidityMeasurementValues,
  getAverageHumidityMeasurementTime,
  getAverageHumidityMeasurementValues
};
