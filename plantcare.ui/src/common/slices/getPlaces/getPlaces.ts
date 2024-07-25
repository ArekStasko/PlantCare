import emptyApi from '../../../app/api/emptyApi';
import { Place } from '../../models/Place';
import { GetToken } from "../../services/CookieService";

export const getPlacesApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    GetPlaces: build.query<Place[], void>({
      query: () => ({
        url : '/places/get',
        headers: { Authorization: GetToken() }
      })
    })
  }),
  overrideExisting: false
});

export const { useGetPlacesQuery } = getPlacesApi;
