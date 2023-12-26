import emptyApi from '../../../app/api/emptyApi';
import { HumidityMeasurement } from '../../models/HumidityMeasurement';

export class GetHumidityMeasurementsApiParameters {
  moduleId!: string;
  fromDate!: string | null;
  toDate!: string | null;
}

const getCorrectHumidityMeasurementsURL = (parameters: GetHumidityMeasurementsApiParameters) => {
  if (parameters.fromDate === null || parameters.toDate === null)
    return `/humidity-measurements/Get?id=${parameters.moduleId}`;
  return `/humidity-measurements/Get?id=${parameters.moduleId}&fromDate=${parameters.fromDate}&toDate=${parameters.toDate}`;
};

export const getHumidityMeasurementsApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    GetHumidityMeasurements: build.query<
      HumidityMeasurement[],
      GetHumidityMeasurementsApiParameters
    >({
      query: (parameters: GetHumidityMeasurementsApiParameters) =>
        getCorrectHumidityMeasurementsURL(parameters)
    })
  }),
  overrideExisting: false
});

export const { useGetHumidityMeasurementsQuery } = getHumidityMeasurementsApi;
