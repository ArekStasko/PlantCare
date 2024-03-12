import { useCreatePlaceMutation } from '../../common/slices/createPlace/createPlace';
import { useGetPlacesQuery } from '../../common/slices/getPlaces/getPlaces';
import validators from '../../common/services/Validators';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { CreatePlaceRequest } from '../../common/slices/createPlace/createPlaceRequest';
import { IWizardStep } from '../../common/Layouts/Wizard/interfaces';
import React, { useState } from 'react';
import WizardContext from '../../common/Layouts/Wizard/WizardContext/wizardContext';
import UpdateSummary from '../components/placeWizardSteps/UpdateSummary/updateSummary';
import Details from '../components/placeWizardSteps/Details/details';

export const CreatePlace = () => {
  const [createPlace] = useCreatePlaceMutation();
  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.createPlaceSchema)
  });

  const onCreate = async () => {
    const request: CreatePlaceRequest = {
      name: methods.getValues('name')
    };
    return await createPlace(request);
  };

  const steps: IWizardStep[] = [
    {
      title: 'Place Details',
      component: <Details />,
      validators: ['name'],
      order: 0,
      nextStep: 1,
      isStepVisible: true,
      isFinal: false
    },
    {
      title: 'Place UpdateSummary',
      component: <UpdateSummary />,
      validators: [],
      order: 1,
      isStepVisible: true,
      isFinal: true
    }
  ];

  return <WizardContext onSubmit={onCreate} steps={steps} methods={methods} />;
};

export default CreatePlace;
