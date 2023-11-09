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

interface DeleteDialogProps {
  setOpenDialog: React.Dispatch<React.SetStateAction<boolean>>;
  openDialog: boolean;
  resourceId: number;
  resourceType: string;
}

export const DeleteDialog = ({
  setOpenDialog,
  openDialog,
  resourceId,
  resourceType
}: DeleteDialogProps) => {
  const [deletePlant] = useDeletePlantMutation();
  const [deletePlace] = useDeletePlaceMutation();
  const { refetch } = useGetPlacesQuery();

  const confirmDelete = async () => {
    console.log('RESOURCE ID');
    console.log(resourceId);
    if (resourceType === 'plant') {
      await deletePlant(resourceId);
      refetch();
    }

    if (resourceType === 'place') {
      await deletePlace(resourceId);
      refetch();
    }

    setOpenDialog(!openDialog);
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
