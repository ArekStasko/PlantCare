import plantcareApi from '../../../app/api/plantcareApi';
import { Plant } from '../../models/Plant';
import { GetPlantData } from './getPlantData';

export const getPlantApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    GetPlant: build.query<Plant, GetPlantData>({
      query: ({ plantId }: GetPlantData) => ({
        url: `/plants/getById?id=${plantId}`
      })
    })
  }),
  overrideExisting: false
});

export const { useLazyGetPlantQuery, useGetPlantQuery } = getPlantApi;
