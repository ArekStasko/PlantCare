import CreatePlaceCommand from '@arekstasko/plantcare-api-client'
import plantcareApi from '../../../app/api/plantcareApi';

export const createPlaceApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    CreatePlace: build.mutation<boolean, CreatePlaceCommand>({
      query: ({ ...data }) => ({
        url: '/places',
        method: 'POST',
        body: data
      }),
      invalidatesTags: ['Places']
    })
  }),
  overrideExisting: false
});

export const { useCreatePlaceMutation } = createPlaceApi;
