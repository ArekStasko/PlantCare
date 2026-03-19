//IT IS IMPORTANT TO NOT IMPORT CREATE API AND FETCHBASEQUERY FROM BELOW PATH
//import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/dist/query';
import { Client } from "@arekstasko/plantcare-api-client";
import axios from "axios";
import { getToken } from "identity-provider-client";

export const axiosInstance = axios.create({
    transformResponse: data => data,
  });

axiosInstance.interceptors.request.use((config) => {
  const token = getToken();

  if (token) {
    config.headers = {
      ...config.headers,
      Authorization: `Bearer ${token}`,
    };
  }

  return config;
});

const plantCareApi = new Client('http://192.168.1.40:8080/api', axiosInstance);

export default plantCareApi;