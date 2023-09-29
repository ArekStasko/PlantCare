import emptyApi from "../../../app/api/emptyApi";

export const createPlantApi = emptyApi.injectEndpoints({
    endpoints: build => ({
        CreatePlant: build.mutation({
            query: ({ ...rest }) => ({
                url: "/plants/Create",
                method: "POST",
                body: rest,
            })
        })
    }),
    overrideExisting: false
});

export const {useCreatePlantMutation} = createPlantApi;