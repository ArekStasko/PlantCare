import emptyApi from "../../../app/api/emptyApi";

export const getPlantsApi = emptyApi.injectEndpoints({
    endpoints: build => ({
        GetPlants: build.query({
            query: () => "/plants/GetAll"
        }),
    }),
    overrideExisting: false,
})

export const {useGetPlantsQuery} = getPlantsApi;