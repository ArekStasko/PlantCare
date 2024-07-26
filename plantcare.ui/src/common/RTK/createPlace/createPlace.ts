import emptyApi from '../../../app/api/emptyApi';
import { CreatePlaceRequest } from './createPlaceRequest';

export const createPlaceApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    CreatePlace: build.mutation<boolean, CreatePlaceRequest>({
      query: ({ ...data }) => ({
        url: '/places/Create',
        method: 'POST',
        body: data,
      })
    })
  }),
  overrideExisting: false
});

export const { useCreatePlaceMutation } = createPlaceApi;
