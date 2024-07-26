import React from 'react';
import WizardContext from '../../common/Layouts/Wizard/WizardContext/wizardContext';
import { IWizardStep } from '../../common/Layouts/Wizard/interfaces';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import validators from '../../common/services/Validators';
import { CreatePlantRequest } from '../../common/RTK/createPlant/createPlantRequest';
import { useCreatePlantMutation } from '../../common/RTK/createPlant/createPlant';
import Summary from '../components/plantWizardSteps/Summary/summary';
import Details from '../components/plantWizardSteps/Details/details';
import PlaceSelect from '../components/plantWizardSteps/PlaceSelect/placeSelect';
import ModuleSelect from '../components/plantWizardSteps/ModuleSelect/moduleSelect';

export const CreatePlant = () => {
  const [createPlant] = useCreatePlantMutation();
  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.createPlantSchema)
  });

  const onCreate = async () => {
    const request: CreatePlantRequest = {
      name: methods.getValues('name'),
      description: methods.getValues('description'),
      type: +methods.getValues('plantType'),
      placeId: methods.getValues('plantPlace'),
      moduleId: methods.getValues('plantModule')
    };
    const result = await createPlant(request);
    return result;
  };

  const steps: IWizardStep[] = [
    {
      title: 'Plant Details',
      component: <Details />,
      validators: ['name', 'description', 'plantType'],
      id: 0,
      nextStep: 1,
      isStepVisible: true,
      isFinal: false
    },
    {
      title: 'Place Select',
      component: <PlaceSelect />,
      validators: ['plantPlace'],
      id: 1,
      nextStep: 2,
      previousStep: 0,
      isStepVisible: true,
      isFinal: false
    },
    {
      title: 'Module Select',
      component: <ModuleSelect />,
      validators: ['plantModule'],
      id: 2,
      nextStep: 3,
      previousStep: 1,
      isStepVisible: true,
      isFinal: false
    },
    {
      title: 'Plant UpdateSummary',
      component: <Summary />,
      validators: [],
      id: 3,
      previousStep: 2,
      isStepVisible: true,
      isFinal: true
    }
  ];

  return <WizardContext onSubmit={onCreate} steps={steps} methods={methods} />;
};

export default CreatePlant;
