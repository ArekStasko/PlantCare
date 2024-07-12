import emptyApi from '../../../app/api/emptyApi';
import { UpdatePlaceRequest } from './updatePlaceRequest';
import { GetToken } from '../../services/CookieService';

export const updatePlaceApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    UpdatePlace: build.mutation<boolean, UpdatePlaceRequest>({
      query: ({ ...data }) => ({
        url: '/places/Update',
        method: 'POST',
        body: data,
        headers: { Authorization: GetToken() }
      })
    })
  }),
  overrideExisting: false
});

export const { useUpdatePlaceMutation } = updatePlaceApi;
