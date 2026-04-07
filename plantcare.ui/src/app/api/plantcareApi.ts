import { Client } from '@arekstasko/plantcare-api-client';
import axios, { AxiosRequestHeaders } from 'axios';
import { getToken } from 'identity-provider-client';

export const axiosInstance = axios.create({
  transformResponse: (data) => data
});

axiosInstance.interceptors.request.use((config) => {
  const token = getToken();
  console.log("TOKEN:", token);
  if (token) {
    config.headers = {
      ...config.headers,
      Authorization: token
    } as AxiosRequestHeaders;
  }

  return config;
});

const plantCareApi = new Client('http://192.168.1.40:8080', axiosInstance);

export default plantCareApi;
