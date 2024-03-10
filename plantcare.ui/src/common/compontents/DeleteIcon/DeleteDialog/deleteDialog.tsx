import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle
} from '@mui/material';
import React, { useEffect } from 'react';
import { useGetPlacesQuery } from '../../../slices/getPlaces/getPlaces';
import { useDeletePlantMutation } from '../../../slices/deletePlant/deletePlant';
import { useDeletePlaceMutation } from '../../../slices/deletePlace/deletePlace';
import { useGetPlantsQuery } from '../../../slices/getPlants/getPlants';
import { useNavigate } from 'react-router';
import routingConstants from '../../../../app/routing/routingConstants';

interface DeleteDialogProps {
  setOpenDialog: React.Dispatch<React.SetStateAction<boolean>>;
  openDialog: boolean;
  resourceId: number;
  resourceType: string;
  redirectToPortfolio?: boolean;
  callIsError?: React.Dispatch<React.SetStateAction<boolean>>;
}

export const DeleteDialog = ({
  setOpenDialog,
  openDialog,
  resourceId,
  resourceType,
  redirectToPortfolio,
  callIsError
}: DeleteDialogProps) => {
  const navigate = useNavigate();
  const [deletePlant] = useDeletePlantMutation();
  const [deletePlace] = useDeletePlaceMutation();
  const { refetch: refetchPlaces } = useGetPlacesQuery();
  const { refetch: refetchPlants } = useGetPlantsQuery();

  const callDelete = async (): Promise<boolean> => {
    let result;
    if (resourceType === 'plant') {
      result = await deletePlant(resourceId);

      refetchPlants();
    }

    if (resourceType === 'place') {
      result = await deletePlace(resourceId);
      refetchPlaces();
    }

    if (!result) return false;

    if ('data' in result) {
      return result.data;
    } else {
      return false;
    }
  };

  const confirmDelete = async () => {
    const result = await callDelete();

    setOpenDialog(!openDialog);
    if (result! && redirectToPortfolio && callIsError) {
      callIsError(true);
      return;
    }
    if (redirectToPortfolio) navigate(routingConstants.root);
  };

  return (
    <Dialog open={openDialog} onClose={setOpenDialog}>
      <DialogTitle id="alert-dialog-title">
        {`Do you want do delete this ${resourceType} ?`}
      </DialogTitle>
      <DialogContent>
        {resourceType === 'plant' ? (
          <DialogContentText>
            By clicking confirm this plant will be deleted permanently. You won't be able to restore
            the deleted resource.
          </DialogContentText>
        ) : (
          <DialogContentText>
            By clicking confirm this place and plants that belongs to it will be deleted
            permanently. You won't be able to restore the deleted resource.
          </DialogContentText>
        )}
      </DialogContent>
      <DialogActions>
        <Button variant="contained" onClick={() => setOpenDialog(!openDialog)}>
          Cancel
        </Button>
        <Button variant="outlined" color="error" onClick={async () => await confirmDelete()}>
          Confirm
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default DeleteDialog;
