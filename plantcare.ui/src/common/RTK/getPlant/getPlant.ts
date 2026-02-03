import plantcareApi from '../../../app/api/plantcareApi';
import { Plant } from '../../models/Plant';

export const getPlantApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    GetPlant: build.query<Plant, number>({
      query: ({ plantId }: number) => ({
        url: `/plants/${plantId}`
      }),
      providesTags: ['Plants']
    })
  }),
  overrideExisting: false
});

export const { useLazyGetPlantQuery, useGetPlantQuery } = getPlantApi;
