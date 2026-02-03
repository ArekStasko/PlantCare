import { CreatePlantCommand } from "@arekstasko/plantcare-api-client";
import plantcareApi from '../../../app/api/plantcareApi';

export const createPlantApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    CreatePlant: build.mutation<boolean, CreatePlantCommand>({
      query: ({ ...data }) => ({
        url: '/plants',
        method: 'POST',
        body: data
      }),
      invalidatesTags: ['Plants']
    })
  }),
  overrideExisting: false
});

export const { useCreatePlantMutation } = createPlantApi;
