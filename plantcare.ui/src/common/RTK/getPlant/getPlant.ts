import emptyApi from '../../../app/api/emptyApi';
import { Plant } from '../../models/Plant';
import { GetPlantData } from './getPlantData';
import { GetToken } from '../../services/CookieService';

export const getPlantApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    GetPlant: build.query<Plant, GetPlantData>({
      query: ({ plantId }: GetPlantData) => ({
        url: `/plants/getById?id=${plantId}`,
        headers: { Authorization: GetToken() }
      })
    })
  }),
  overrideExisting: false
});

export const { useLazyGetPlantQuery, useGetPlantQuery } = getPlantApi;
