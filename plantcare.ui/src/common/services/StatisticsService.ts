import { AverageHumidity, HumidityMeasurement } from '@arekstasko/plantcare-api-client';

const getHumidityMeasurementTime = (data: HumidityMeasurement[]): string[] =>
  data.map((c) => {
    if (!c.date) return '';
    const date = new Date(c.date);
    const hours = date.getHours();
    const minutes = date.getMinutes();

    const formattedHours = hours < 10 ? `0${hours}` : `${hours}`;
    const formattedMinutes = minutes < 10 ? `0${minutes}` : `${minutes}`;

    return `${formattedHours}:${formattedMinutes}`;
  });

const getHumidityMeasurementValues = (data: HumidityMeasurement[]): number[] =>
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
