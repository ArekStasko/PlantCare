import { UpdatePlaceCommand } from "@arekstasko/plantcare-api-client";
import plantcareApi from '../../../app/api/plantcareApi';

export const updatePlaceApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    UpdatePlace: build.mutation<boolean, UpdatePlaceCommand>({
      query: ({ ...data }) => ({
        url: '/places',
        method: 'PUT',
        body: data
      }),
      invalidatesTags: ['Places']
    })
  }),
  overrideExisting: false
});

export const { useUpdatePlaceMutation } = updatePlaceApi;
