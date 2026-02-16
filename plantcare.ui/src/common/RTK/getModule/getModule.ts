import plantcareApi from "../../../app/api/plantcareApi";
import { Module } from "../../models/Module";

export const getModuleApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    GetModule: build.query<Module, Number>({
      query: (moduleId: Number) => ({
        url: `/modules/${moduleId}`,
      }),
      providesTags: ['Modules']
    })
  }),
  overrideExisting: false
});

export const { useGetModuleQuery } = getModuleApi;