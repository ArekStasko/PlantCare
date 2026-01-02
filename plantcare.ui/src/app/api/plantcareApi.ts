//IT IS IMPORTANT TO NOT IMPORT CREATE API AND FETCHBASEQUERY FROM BELOW PATH
//import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/dist/query';
import { BaseQueryApi, createApi, FetchArgs, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { ClientRootState } from 'identity-provider-client';

const plantcareApiBaseQuery = async (
  args: string | FetchArgs,
  api: BaseQueryApi,
  extraOptions: {}
) => {
  const rawBaseQuery = fetchBaseQuery({
    baseUrl: 'http://192.168.1.40:8080/api/v1',
    prepareHeaders: (headers, { getState }) => {
      const token = (getState() as ClientRootState).auth.accessToken;
      if (token) {
        headers.set('Authorization', token);
      }
      return headers;
    }
  });
  const result = await rawBaseQuery(args, api, extraOptions);
  return result;
};

const plantcareApi = createApi({
  reducerPath: 'emptyApi',
  baseQuery: plantcareApiBaseQuery,
  endpoints: (build) => ({}),
  tagTypes: ['Plants', 'Modules', 'Places']
});

export default plantcareApi;
