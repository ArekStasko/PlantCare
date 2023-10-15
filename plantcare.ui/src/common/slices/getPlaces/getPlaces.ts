import emptyApi from "../../../app/api/emptyApi";

export const getPlacesApi = emptyApi.injectEndpoints({
    endpoints: build => ({
        GetPlaces: build.query({
            query: () => "/places/GetAll"
        }),
    }),
    overrideExisting: false,
})

export const {useGetPlacesQuery} = getPlacesApi;