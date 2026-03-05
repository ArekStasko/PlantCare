import plantcareApi from '../../../app/api/plantcareApi';
import { AverageHumidity } from '@arekstasko/plantcare-api-client';

export class GetHumidityMeasurementsApiParameters {
  moduleId!: string;
  fromDate!: string | null;
  toDate!: string | null;
}

const getCorrectAverageHumidityMeasurementsURL = (
  parameters: GetHumidityMeasurementsApiParameters
) => {
  if (parameters.fromDate === null || parameters.toDate === null)
    return `/humidity-measurements/${parameters.moduleId}`;
  return `/humidity-measurements/${parameters.moduleId}?fromDate=${parameters.fromDate}&toDate=${parameters.toDate}`;
};

export const getAverageHumidityMeasurementsApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    GetAverageHumidityMeasurements: build.query<AverageHumidity[], GetHumidityMeasurementsApiParameters>({
      query: (parameters: GetHumidityMeasurementsApiParameters) =>
        getCorrectAverageHumidityMeasurementsURL(parameters)
    })
  }),
  overrideExisting: false
});

export const { useGetAverageHumidityMeasurementsQuery } = getAverageHumidityMeasurementsApi;
