import React from 'react';
import WizardContext from '../../common/Layouts/Wizard/WizardContext/wizardContext';
import PlantDetails from './Steps/PlantDetails/plantDetails';
import { IWizardStep } from '../../common/Layouts/Wizard/interfaces';
import PlaceSelect from './Steps/PlaceSelect/placeSelect';
import PlantSummary from './Steps/PlantSummary/plantSummary';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import validators from '../../common/services/Validators';
import { Plant } from '../../common/models/Plant';
import { CreatePlantRequest } from '../../common/slices/createPlant/createPlantRequest';
import { useCreatePlantMutation } from '../../common/slices/createPlant/createPlant';

export const CreatePlant = () => {
  const [createPlant, createPlantResult] = useCreatePlantMutation();
  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.createPlantSchema)
  });

  const onCreate = async () => {
    const request: CreatePlantRequest = {
      name: methods.getValues('name'),
      description: methods.getValues('description'),
      type: +methods.getValues('plantType'),
      placeId: methods.getValues('plantPlace')
    };
    await createPlant(request);
  };

  const steps: IWizardStep[] = [
    {
      title: 'Plant Details',
      component: <PlantDetails />,
      validators: ['name', 'description', 'plantType'],
      order: 0
    },
    {
      title: 'Place Select',
      component: <PlaceSelect />,
      validators: ['plantPlace'],
      order: 1
    },
    {
      title: 'Plant Summary',
      component: <PlantSummary />,
      validators: [],
      order: 2
    }
  ];

  return <WizardContext onSubmit={onCreate} steps={steps} methods={methods} />;
};

export default CreatePlant;
