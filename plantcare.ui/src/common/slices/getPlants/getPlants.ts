import emptyApi from '../../../app/api/emptyApi';
import { Plant } from '../../models/Plant';
import { GetToken } from '../../services/CookieService';

export const getPlantsApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    GetPlants: build.query<Plant[], void>({
      query: () => ({
        url: `/plants/get`,
        headers: { Authorization: GetToken() }
      })
    })
  }),
  overrideExisting: false
});

export const { useGetPlantsQuery } = getPlantsApi;
