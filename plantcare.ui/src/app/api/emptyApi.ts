import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query";

const emptyApi = createApi({
    reducerPath: 'emptyApi',
    baseQuery: fetchBaseQuery({
        baseUrl: process.env.API_URL
    }),
    endpoints: build => ({})
})

export default emptyApi;