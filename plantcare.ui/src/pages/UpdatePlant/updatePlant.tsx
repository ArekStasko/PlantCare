import React, { useEffect } from 'react';
import WizardContext from '../../common/Layouts/Wizard/WizardContext/wizardContext';
import { IWizardStep } from '../../common/Layouts/Wizard/interfaces';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { useUpdatePlantMutation } from '../../common/slices/updatePlant/updatePlant';
import { UpdatePlantRequest } from '../../common/slices/updatePlant/updatePlantRequest';
import { useParams } from 'react-router';
import { useGetPlantQuery } from '../../common/slices/getPlant/getPlant';
import validators from '../../common/services/Validators';
import CustomBackdrop from '../../common/compontents/customBackdrop/backdrop';
import Summary from '../components/plantWizardSteps/Summary/summary';
import Details from '../components/plantWizardSteps/Details/details';
import PlaceSelect from '../components/plantWizardSteps/PlaceSelect/placeSelect';
import { useDeletePlantMutation } from '../../common/slices/deletePlant/deletePlant';
import { FetchBaseQueryError } from '@reduxjs/toolkit/query';
import { SerializedError } from '@reduxjs/toolkit';
import ActionSelect from '../components/ActionSelect/actionSelect';
import DeleteSummary from '../components/plantWizardSteps/DeleteSummary/deleteSummary';

export const UpdatePlant = () => {
  const { id } = useParams();

  const [updatePlant] = useUpdatePlantMutation();
  const [deletePlant] = useDeletePlantMutation();
  const { data: plant, isLoading: plantLoading } = useGetPlantQuery({
    plantId: id!
  });

  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.updatePlantSchema)
  });

  const getNextStepByFlow = () => {
    const flow = methods.getValues('flow');
    if (flow === 'delete') return 1;
    return 2;
  };

  const getIsStepVisible = (stepFlow: string) => {
    const flow = methods.getValues('flow');
    return stepFlow === flow;
  };

  const onSubmit = async (): Promise<
    { data: boolean } | { error: FetchBaseQueryError | SerializedError }
  > => {
    const flow = methods.getValues('flow');
    let result = undefined;
    if (flow === 'delete') {
      const id = +methods.getValues('id');
      result = await deletePlant({ plantId: id });
    }

    if (flow === 'update') {
      const request: UpdatePlantRequest = {
        id: +methods.getValues('id'),
        name: methods.getValues('name'),
        description: methods.getValues('description'),
        type: +methods.getValues('plantType'),
        placeId: methods.getValues('plantPlace')
      };
      result = await updatePlant(request);
    }

    if (!result) return { data: false };
    return result;
  };

  useEffect(() => {
    if (!plantLoading) {
      methods.reset({
        id: plant!.id.toString(),
        name: plant!.name,
        description: plant!.description,
        plantType: plant!.type.toString(),
        plantPlace: plant!.placeId.toString()
      });
    }
  }, [plantLoading]);

  const steps: IWizardStep[] = [
    {
      title: 'Select Action',
      component: <ActionSelect />,
      validators: ['flow'],
      id: 0,
      nextStep: getNextStepByFlow(),
      isStepVisible: true,
      isFinal: false
    },
    {
      title: 'Delete Plant Summary',
      component: <DeleteSummary />,
      validators: [],
      previousStep: 0,
      id: 1,
      isStepVisible: getIsStepVisible('delete'),
      isFinal: true
    },
    {
      title: 'Plant Details',
      component: <Details plantData={plant!} />,
      validators: ['name', 'description', 'plantType'],
      previousStep: 0,
      id: 2,
      nextStep: 3,
      isStepVisible: getIsStepVisible('update'),
      isFinal: false
    },
    {
      title: 'Place Select',
      component: <PlaceSelect plantData={plant!} />,
      validators: ['plantPlace'],
      previousStep: 2,
      id: 3,
      nextStep: 4,
      isStepVisible: getIsStepVisible('update'),
      isFinal: false
    },
    {
      title: 'Plant Update Summary',
      component: <Summary />,
      validators: [],
      previousStep: 3,
      id: 4,
      isStepVisible: getIsStepVisible('update'),
      isFinal: true
    }
  ];

  return plantLoading ? (
    <>
      <CustomBackdrop isLoading={plantLoading} />
    </>
  ) : (
    <>
      <WizardContext onSubmit={onSubmit} steps={steps} methods={methods} />
    </>
  );
};

export default UpdatePlant;
