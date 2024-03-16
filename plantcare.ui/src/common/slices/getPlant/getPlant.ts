import emptyApi from '../../../app/api/emptyApi';
import { Plant } from '../../models/Plant';

export const getPlantApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    GetPlant: build.query<Plant, string>({
      query: (plantId: string) => `/plants/getById?id=${plantId}`
    })
  }),
  overrideExisting: false
});

export const { useLazyGetPlantQuery, useGetPlantQuery } = getPlantApi;
