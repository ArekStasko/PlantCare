import { UpdatePlantCommand } from '@arekstasko/plantcare-api-client';
import plantcareApi from '../../../app/api/plantcareApi';

export const updatePlantApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    UpdatePlant: build.mutation<boolean, UpdatePlantCommand>({
      query: ({ ...data }) => ({
        url: '/plants',
        method: 'PUT',
        body: data
      }),
      invalidatesTags: ['Plants', 'Modules']
    })
  }),
  overrideExisting: false
});

export const { useUpdatePlantMutation } = updatePlantApi;
