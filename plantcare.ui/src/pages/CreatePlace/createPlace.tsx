import { Box, Typography } from '@mui/material';
import { useCreatePlaceMutation } from '../../common/slices/createPlace/createPlace';
import { useGetPlacesQuery } from '../../common/slices/getPlaces/getPlaces';
import validators from '../../common/services/Validators';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { CreatePlaceRequest } from '../../common/slices/createPlace/createPlaceRequest';
import { IWizardStep } from '../../common/Layouts/Wizard/interfaces';
import PlaceDetails from './Steps/PlaceDetails/placeDetails';
import PlaceSummary from './Steps/PlaceSummary/placeSummary';
import React from 'react';
import WizardContext from '../../common/Layouts/Wizard/WizardContext/wizardContext';

export const CreatePlace = () => {
  const [createPlace, createPlaceResult] = useCreatePlaceMutation();
  const { refetch } = useGetPlacesQuery();
  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.createPlaceSchema)
  });

  const onCreate = async () => {
    const request: CreatePlaceRequest = {
      name: methods.getValues('name')
    };
    await createPlace(request);
    refetch();
  };

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

  return <WizardContext onSubmit={onCreate} steps={steps} methods={methods} />;
};

export default CreatePlace;
