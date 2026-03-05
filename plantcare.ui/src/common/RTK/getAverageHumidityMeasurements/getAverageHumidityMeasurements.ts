import plantcareApi from '../../../app/api/plantcareApi';
import { AverageHumidity } from '@arekstasko/plantcare-api-client';

export class GetHumidityMeasurementsApiParameters {
  moduleId!: string;
  fromDate!: string | null;
  toDate!: string | null;
}

export const getAverageHumidityMeasurementsApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    GetAverageHumidityMeasurements: build.query<AverageHumidity[], GetHumidityMeasurementsApiParameters>({
      query: (parameters: GetHumidityMeasurementsApiParameters) => ({
        url: `/humidity-measurements/${parameters.moduleId}/average?fromDate=${parameters.fromDate}&toDate=${parameters.toDate}`
      })
    })
  }),
  overrideExisting: false
});

export const { useGetAverageHumidityMeasurementsQuery } = getAverageHumidityMeasurementsApi;
