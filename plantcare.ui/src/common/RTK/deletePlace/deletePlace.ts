import plantcareApi from '../../../app/api/plantcareApi';

export const deletePlaceApi = plantcareApi.injectEndpoints({
  endpoints: (build) => ({
    DeletePlace: build.mutation<boolean, number>({
      query: ({ placeId }: number) => ({
        url: `/places?id=${placeId}`,
        method: 'DELETE'
      }),
      invalidatesTags: ['Places']
    })
  }),
  overrideExisting: false
});

export const { useDeletePlaceMutation } = deletePlaceApi;
