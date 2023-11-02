import React from 'react';
import WizardContext from '../../common/Layouts/Wizard/WizardContext/wizardContext';
import PlantDetails from './Steps/PlantDetails/plantDetails';
import { IWizardStep } from '../../common/Layouts/Wizard/interfaces';
import PlaceSelect from './Steps/PlaceSelect/placeSelect';
import PlantSummary from './Steps/PlantSummary/plantSummary';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import validators from '../../common/services/Validators';

export const CreatePlant = () => {
  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.createPlantSchema)
  });

  const steps: IWizardStep[] = [
    {
      title: 'Plant Details',
      component: <PlantDetails />,
      order: 0
    },
    {
      title: 'Place Select',
      component: <PlaceSelect />,
      order: 1
    },
    {
      title: 'Plant Summary',
      component: <PlantSummary />,
      order: 2
    }
  ];

  return <WizardContext steps={steps} methods={methods} />;
};

export default CreatePlant;
