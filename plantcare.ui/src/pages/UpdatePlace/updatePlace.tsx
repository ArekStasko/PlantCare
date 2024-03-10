import { useGetPlacesQuery } from '../../common/slices/getPlaces/getPlaces';
import validators from '../../common/services/Validators';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { IWizardStep } from '../../common/Layouts/Wizard/interfaces';
import React, { useEffect } from 'react';
import WizardContext from '../../common/Layouts/Wizard/WizardContext/wizardContext';
import { useUpdatePlaceMutation } from '../../common/slices/updatePlace/updatePlace';
import { useParams } from 'react-router';
import { UpdatePlaceRequest } from '../../common/slices/updatePlace/updatePlaceRequest';
import CustomBackdrop from '../../common/compontents/customBackdrop/backdrop';
import Summary from '../components/placeWizardSteps/Summary/summary';
import Details from '../components/placeWizardSteps/Details/details';
import ActionSelect from '../components/placeWizardSteps/ActionSelect/actionSelect';

export const UpdatePlace = () => {
  const { id } = useParams();

  const [updatePlace] = useUpdatePlaceMutation();
  const { data: places, isLoading: placesLoading } = useGetPlacesQuery();
  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.updatePlaceSchema)
  });

  const onUpdate = async () => {
    const request: UpdatePlaceRequest = {
      id: +methods.getValues('id'),
      name: methods.getValues('name')
    };
    const result = await updatePlace(request);
    return result;
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
      title: 'Select Action',
      component: <ActionSelect />,
      validators: ['flow'],
      order: 0
    },
    {
      title: 'Place Details',
      component: <Details />,
      validators: ['name'],
      order: 0
    },
    {
      title: 'Place Summary',
      component: <Summary />,
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
