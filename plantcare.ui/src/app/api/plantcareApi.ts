//IT IS IMPORTANT TO NOT IMPORT CREATE API AND FETCHBASEQUERY FROM BELOW PATH
//import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/dist/query';
import { BaseQueryApi, createApi, FetchArgs, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { ClientRootState } from 'identity-provider-client';
import { Client } from "@arekstasko/plantcare-api-client";

const client = new Client('http://192.168.1.40:8080/api');

const plantCareApi = async (
  args: { method: keyof Client; body?: any; params?: any },
  api: BaseQueryApi,
  extraOptions: {}
) => {
  try {
    const token = (api.getState() as ClientRootState).auth.accessToken;

    if (token && client['http']) {
      client['http'].fetch = (url, init = {}) => {
        init.headers = {
          ...init.headers,
          Authorization: `Bearer ${token}`,
        };
        return fetch(url, init);
      };
    }

    const data = await (client[args.method] as any)(args.body, args.params);
    return { data };
  } catch (error: any) {
    return { error: { status: error?.response?.status || 500, data: error.message } };
  }
};

export default plantCareApi;