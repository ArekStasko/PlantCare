import { UpdatePlaceCommand } from '@arekstasko/plantcare-api-client';
import plantcareApi from '../../../app/api/plantcareApi';
import emptyApi from "../emptyApi";

const updatePlaceCall = (request: UpdatePlaceCommand) =>
  plantcareApi
    .placesPUT(request)
    .then(result => ({
      data: result,
      error: null
    }))
    .catch(err => ({
      data: null,
      error: err
    }))

export const updatePlaceApi = emptyApi.injectEndpoints({
  endpoints: (build) => ({
    UpdatePlace: build.mutation<boolean, UpdatePlaceCommand>({
      query: updatePlaceCall,
      invalidatesTags: ['Places']
    })
  }),
  overrideExisting: false
});

export const { useUpdatePlaceMutation } = updatePlaceApi;
