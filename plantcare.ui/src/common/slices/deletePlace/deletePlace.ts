import emptyApi from "../../../app/api/emptyApi";

export const deletePlaceApi = emptyApi.injectEndpoints({
    endpoints: build => ({
        DeletePlace: build.mutation<boolean, number>({
            query: (placeId: number) => ({
                url: `/places/Delete?id=${placeId}`,
                method: "DELETE"
            })
        })
    }),
    overrideExisting: false
})

export const { useDeletePlaceMutation } = deletePlaceApi;