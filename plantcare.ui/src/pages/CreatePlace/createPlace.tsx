import { useCreatePlaceMutation } from '../../common/slices/createPlace/createPlace';
import validators from '../../common/services/Validators';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { CreatePlaceRequest } from '../../common/slices/createPlace/createPlaceRequest';
import { IWizardStep } from '../../common/Layouts/Wizard/interfaces';
import React from 'react';
import WizardContext from '../../common/Layouts/Wizard/WizardContext/wizardContext';
import UpdateSummary from '../components/placeWizardSteps/UpdateSummary/updateSummary';
import Details from '../components/placeWizardSteps/Details/details';
import { GetUserData } from '../../common/services/CookieService';

export const CreatePlace = () => {
  const [createPlace] = useCreatePlaceMutation();
  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.createPlaceSchema)
  });

  const onCreate = async () => {
    const userData = GetUserData();
    const request: CreatePlaceRequest = {
      name: methods.getValues('name'),
      userId: +userData!.id
    };
    return await createPlace(request);
  };

  const steps: IWizardStep[] = [
    {
      title: 'Place Details',
      component: <Details />,
      validators: ['name'],
      id: 0,
      nextStep: 1,
      isStepVisible: true,
      isFinal: false
    },
    {
      title: 'Place UpdateSummary',
      component: <UpdateSummary />,
      validators: [],
      id: 1,
      isStepVisible: true,
      previousStep: 0,
      isFinal: true
    }
  ];

  return <WizardContext onSubmit={onCreate} steps={steps} methods={methods} />;
};

export default CreatePlace;
