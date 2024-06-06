import emptyApi from '../../../app/api/emptyApi';
import { Plant } from '../../models/Plant';
import { GetPlantData } from './getPlantData';

export const getPlantApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    GetPlant: build.query<Plant, GetPlantData>({
      query: ({ plantId, userId }: GetPlantData) => `/plants/getById?id=${plantId}&userId=${userId}`
    })
  }),
  overrideExisting: false
});

export const { useLazyGetPlantQuery, useGetPlantQuery } = getPlantApi;
