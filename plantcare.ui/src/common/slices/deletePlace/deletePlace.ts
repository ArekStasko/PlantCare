import emptyApi from "../../../app/api/emptyApi";

export const deletePlaceApi = emptyApi.injectEndpoints({
    endpoints: build => ({
        DeletePlace: build.mutation({
            query: ({ placeId, ...rest }) => ({
                url: `/place/Delete?id=${placeId}`,
                method: "DELETE"
            })
        })
    }),
    overrideExisting: false
})

export const { useDeletePlaceMutation } = deletePlaceApi;