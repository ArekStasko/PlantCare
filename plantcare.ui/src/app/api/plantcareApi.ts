//IT IS IMPORTANT TO NOT IMPORT CREATE API AND FETCHBASEQUERY FROM BELOW PATH
//import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/dist/query';
import { BaseQueryApi, createApi, FetchArgs, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { GetToken } from "../../common/services/CookieService";

const plantcareApiBaseQuery = async (args: (string | FetchArgs), api: BaseQueryApi, extraOptions: {}) => {
  const rawBaseQuery = fetchBaseQuery({
    baseUrl: 'http://192.168.1.40:8080/api/v1',
    prepareHeaders: (headers) => {
      headers.set('Authorization', GetToken()!);
      return headers;
    },
  });
  const result = await rawBaseQuery(args, api, extraOptions);
  return result;
};

const plantcareApi = createApi({
  reducerPath: 'emptyApi',
  baseQuery: plantcareApiBaseQuery,
  endpoints: (build) => ({}),
});

export default plantcareApi;
