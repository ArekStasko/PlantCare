import plantcareApi from '../../../app/api/plantcareApi';
import { CreatePlaceRequest } from './createPlaceRequest';

export const createPlaceApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    CreatePlace: build.mutation<boolean, CreatePlaceRequest>({
      query: ({ ...data }) => ({
        url: '/places/Create',
        method: 'POST',
        body: data
      }),
      invalidatesTags: ['Places']
    })
  }),
  overrideExisting: false
});

export const { useCreatePlaceMutation } = createPlaceApi;
