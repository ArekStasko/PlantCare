import emptyApi from '../../../app/api/emptyApi';
import { Plant } from '../../models/Plant';

export const getPlantsApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    GetPlants: build.query<Plant[], string>({
      query: (userId: string) => `/plants/get?userId=${userId}`
    })
  }),
  overrideExisting: false
});

export const { useGetPlantsQuery } = getPlantsApi;
