import emptyApi from '../../../app/api/emptyApi';
import { Plant } from '../../models/Plant';

export const getPlantsApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    GetPlants: build.query<Plant[], void>({
      query: () => '/plants/get'
    })
  }),
  overrideExisting: false
});

export const { useGetPlantsQuery } = getPlantsApi;
