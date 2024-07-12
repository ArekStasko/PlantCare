import emptyApi from '../../../app/api/emptyApi';
import { CreatePlantRequest } from './createPlantRequest';
import { GetToken } from '../../services/CookieService';

export const createPlantApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    CreatePlant: build.mutation<boolean, CreatePlantRequest>({
      query: ({ ...data }) => ({
        url: '/plants/Create',
        method: 'POST',
        body: data,
        headers: { Authorization: GetToken() }
      })
    })
  }),
  overrideExisting: false
});

export const { useCreatePlantMutation } = createPlantApi;
