import plantcareApi from '../../../app/api/plantcareApi';
import { HumidityMeasurement } from '../../models/HumidityMeasurement';

export class GetHumidityMeasurementsApiParameters {
  moduleId!: string;
  fromDate!: string | null;
  toDate!: string | null;
}

const getCorrectAverageHumidityMeasurementsURL = (parameters: GetHumidityMeasurementsApiParameters) => {
  if (parameters.fromDate === null || parameters.toDate === null)
    return `/humidity-measurements/${parameters.moduleId}`;
  return `/humidity-measurements/${parameters.moduleId}?fromDate=${parameters.fromDate}&toDate=${parameters.toDate}`;
};

export const getAverageHumidityMeasurementsApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    GetHumidityMeasurements: build.query<
      HumidityMeasurement[],
      GetHumidityMeasurementsApiParameters
    >({
      query: (parameters: GetHumidityMeasurementsApiParameters) =>
        getCorrectAverageHumidityMeasurementsURL(parameters)
    })
  }),
  overrideExisting: false
});

export const { useGetHumidityMeasurementsQuery } = getAverageHumidityMeasurementsApi;
