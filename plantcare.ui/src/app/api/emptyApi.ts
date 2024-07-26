//IT IS IMPORTANT TO NOT IMPORT CREATE API AND FETCHBASEQUERY FROM BELOW PATH
//import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/dist/query';
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

const emptyApi = createApi({
  reducerPath: 'emptyApi',
  baseQuery: fetchBaseQuery({
    baseUrl: 'http://192.168.33.10:8080/api/v1'
  }),
  endpoints: (build) => ({})
});

export default emptyApi;
