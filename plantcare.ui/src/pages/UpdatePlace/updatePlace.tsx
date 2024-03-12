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
import UpdateSummary from '../components/placeWizardSteps/UpdateSummary/updateSummary';
import Details from '../components/placeWizardSteps/Details/details';
import ActionSelect from '../components/placeWizardSteps/ActionSelect/actionSelect';
import DeleteSummary from '../components/placeWizardSteps/DeleteSummary/deleteSummary';
import { useDeletePlaceMutation } from '../../common/slices/deletePlace/deletePlace';
import { FetchBaseQueryError } from '@reduxjs/toolkit/query';
import { SerializedError } from '@reduxjs/toolkit';

export const UpdatePlace = () => {
  const { id } = useParams();

  const [updatePlace] = useUpdatePlaceMutation();
  const [deletePlace] = useDeletePlaceMutation();
  const { data: places, isLoading: placesLoading } = useGetPlacesQuery();
  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.updatePlaceSchema)
  });

  const onSubmit = async (): Promise<
    { data: boolean } | { error: FetchBaseQueryError | SerializedError }
  > => {
    const flow = methods.getValues('flow');

    let result = undefined;
    console.log(flow);
    if (flow === 'delete') {
      const id = +methods.getValues('id');
      result = await deletePlace(id);
    }

    if (flow === 'update') {
      const request: UpdatePlaceRequest = {
        id: +methods.getValues('id'),
        name: methods.getValues('name')
      };
      result = await updatePlace(request);
    }

    if (!result) return { data: false };
    return result;
  };

  const getNextStepByFlow = () => {
    const flow = methods.getValues('flow');
    if (flow === 'delete') return 1;
    return 2;
  };

  const getIsStepVisible = (stepFlow: string) => {
    const flow = methods.getValues('flow');
    return stepFlow === flow;
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
      order: 0,
      nextStep: getNextStepByFlow(),
      isStepVisible: true,
      isFinal: false
    },
    {
      title: 'Delete Place Summary',
      component: <DeleteSummary />,
      validators: [],
      order: 1,
      isStepVisible: getIsStepVisible('delete'),
      isFinal: true
    },
    {
      title: 'Place Details',
      component: <Details />,
      validators: ['name'],
      order: 2,
      nextStep: 3,
      isStepVisible: getIsStepVisible('update'),
      isFinal: false
    },
    {
      title: 'Update Place Summary',
      component: <UpdateSummary />,
      validators: [],
      order: 3,
      isStepVisible: getIsStepVisible('update'),
      isFinal: true
    }
  ];

  return placesLoading ? (
    <>
      <CustomBackdrop isLoading={placesLoading} />
    </>
  ) : (
    <>
      <WizardContext onSubmit={onSubmit} steps={steps} methods={methods} />;
    </>
  );
};

export default UpdatePlace;
