import emptyApi from '../../../app/api/emptyApi';
import { Plant } from '../../models/Plant';
import { HumidityMeasurement } from '../../models/HumidityMeasurement';

export const getHumidityMeasurementsApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    GetHumidityMeasurements: build.query<HumidityMeasurement[], string>({
      query: (moduleId: string, fromDate: Date, toDate: Date) =>
        `/humidity-measurements/Get?id=${moduleId}&fromDate=${fromDate}&toDate=${toDate}`
    })
  }),
  overrideExisting: false
});

export const { useGetHumidityMeasurementsQuery } = getHumidityMeasurementsApi;
