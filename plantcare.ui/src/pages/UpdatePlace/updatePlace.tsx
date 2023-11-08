import { useGetPlacesQuery } from '../../common/slices/getPlaces/getPlaces';
import validators from '../../common/services/Validators';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { IWizardStep } from '../../common/Layouts/Wizard/interfaces';
import React, { useEffect } from 'react';
import WizardContext from '../../common/Layouts/Wizard/WizardContext/wizardContext';
import { useUpdatePlaceMutation } from '../../common/slices/updatePlace/updatePlace';
import PlaceDetails from './steps/PlaceDetails/placeDetails';
import PlaceSummary from './steps/PlaceSummary/placeSummary';
import { useParams } from 'react-router';
import { UpdatePlaceRequest } from '../../common/slices/updatePlace/updatePlaceRequest';
import CustomBackdrop from '../../common/compontents/customBackdrop/backdrop';

export const UpdatePlace = () => {
  const { id } = useParams();

  const [updatePlace, updatePlaceResult] = useUpdatePlaceMutation();
  const { data: places, isLoading: placesLoading } = useGetPlacesQuery();
  const { refetch } = useGetPlacesQuery();
  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.updatePlaceSchema)
  });

  const onUpdate = async () => {
    const request: UpdatePlaceRequest = {
      id: +methods.getValues('id'),
      name: methods.getValues('name')
    };
    await updatePlace(request);
    refetch();
  };

  useEffect(() => {
    if (!placesLoading) {
      methods.reset({
        id: id!.toString(),
        name: places!.find((place) => place.id.toString() === id!)!.name
      });
    }
  }, [placesLoading]);

  const steps: IWizardStep[] = [
    {
      title: 'Place Details',
      component: <PlaceDetails />,
      validators: ['name'],
      order: 0
    },
    {
      title: 'Place Summary',
      component: <PlaceSummary />,
      validators: [],
      order: 1
    }
  ];

  return placesLoading ? (
    <>
      <CustomBackdrop isLoading={placesLoading} />
    </>
  ) : (
    <>
      <WizardContext onSubmit={onUpdate} steps={steps} methods={methods} />;
    </>
  );
};

export default UpdatePlace;
