import plantcareApi from '../../../app/api/plantcareApi';
import { Plant } from '../../models/Plant';

export const getPlantsApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    GetPlants: build.query<Plant[], void>({
      query: () => ({
        url: `/plants/get`
      }),
      providesTags: ['Plants']
    })
  }),
  overrideExisting: false
});

export const { useGetPlantsQuery } = getPlantsApi;
