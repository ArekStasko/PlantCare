import emptyApi from '../../../app/api/emptyApi';
import { CreatePlaceRequest } from './createPlaceRequest';
import { GetToken } from '../../services/CookieService';

export const createPlaceApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    CreatePlace: build.mutation<boolean, CreatePlaceRequest>({
      query: ({ ...data }) => ({
        url: '/places/Create',
        method: 'POST',
        body: data,
        headers: { Authorization: GetToken() }
      })
    })
  }),
  overrideExisting: false
});

export const { useCreatePlaceMutation } = createPlaceApi;
