import plantcareApi from '../../../app/api/plantcareApi';
import { Module } from '../../models/Module';
import emptyApi from "../emptyApi";
import { CreateModuleRequest, GetModuleResponse } from "@arekstasko/plantcare-api-client";


const getModules = async () =>
plantcareApi
  .modulesAll()
  .then((result) => ({
    data: result,
  }))
  .catch((err) => ({
    error: err
  }))

const getModule = async (id: number) =>
  plantcareApi
  .modulesGET(id)
  .then((result) => ({
    data: result
  }))
  .catch((err) => ({
    error: err
  }))

const createModule = async (request: CreateModuleRequest) =>
  plantcareApi
    .modulesPOST(request)
    .then((result) => ({
      data: result ?? false
    }))
    .catch((err) => ({
      error: err
    }))

export const ModulesApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    GetModules: build.query<GetModuleResponse[], void>({
      queryFn: getModules,
      providesTags: ['Modules']
    }),
    GetModule: build.query<GetModuleResponse, number>({
      queryFn: getModule,
      providesTags: ['Modules']
    }),
    CreateModule: build.mutation<boolean, CreateModuleRequest>({
      queryFn: createModule,
      invalidatesTags: ['Modules']
    })
  }),
  overrideExisting: false
});

export const { useGetModulesQuery, useGetModuleQuery } = ModulesApi;
