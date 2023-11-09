import React, { useEffect } from 'react';
import WizardContext from '../../common/Layouts/Wizard/WizardContext/wizardContext';
import { IWizardStep } from '../../common/Layouts/Wizard/interfaces';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { useGetPlacesQuery } from '../../common/slices/getPlaces/getPlaces';
import { useUpdatePlantMutation } from '../../common/slices/updatePlant/updatePlant';
import { UpdatePlantRequest } from '../../common/slices/updatePlant/updatePlantRequest';
import { useParams } from 'react-router';
import { useGetPlantQuery } from '../../common/slices/getPlant/getPlant';
import validators from '../../common/services/Validators';
import CustomBackdrop from '../../common/compontents/customBackdrop/backdrop';
import Summary from '../components/plantWizardSteps/Summary/summary';
import Details from '../components/plantWizardSteps/Details/details';
import PlaceSelect from '../components/plantWizardSteps/PlaceSelect/placeSelect';

export const UpdatePlant = () => {
  const { id } = useParams();

  const [updatePlant] = useUpdatePlantMutation();
  const { data: plant, isLoading: plantLoading, refetch: refetchPlant } = useGetPlantQuery(id!);
  const { refetch: refetchPlaces } = useGetPlacesQuery();

  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.updatePlantSchema)
  });

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

  const onUpdate = async () => {
    const request: UpdatePlantRequest = {
      id: +methods.getValues('id'),
      name: methods.getValues('name'),
      description: methods.getValues('description'),
      type: +methods.getValues('plantType'),
      placeId: methods.getValues('plantPlace')
    };
    await updatePlant(request);
    refetchPlant();
    refetchPlaces();
  };

  const steps: IWizardStep[] = [
    {
      title: 'Plant Details',
      component: <Details plantData={plant!} />,
      validators: ['name', 'description', 'plantType'],
      order: 0
    },
    {
      title: 'Place Select',
      component: <PlaceSelect plantData={plant!} />,
      validators: ['plantPlace'],
      order: 1
    },
    {
      title: 'Plant Update Summary',
      component: <Summary />,
      validators: [],
      order: 2
    }
  ];

  return plantLoading ? (
    <>
      <CustomBackdrop isLoading={plantLoading} />
    </>
  ) : (
    <>
      <WizardContext onSubmit={onUpdate} steps={steps} methods={methods} />
    </>
  );
};

export default UpdatePlant;
