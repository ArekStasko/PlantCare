import emptyApi from "../../../app/api/emptyApi";

export const getPlantApi = emptyApi.injectEndpoints({
    endpoints: build => ({
        GetPlant: build.query({
            query: ({plantId}) => `/plants/Get?id=${plantId}`
        }),
    }),
    overrideExisting: false,
})

export const {useGetPlantQuery} = getPlantApi;