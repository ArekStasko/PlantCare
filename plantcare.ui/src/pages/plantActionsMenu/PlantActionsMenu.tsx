import React, { useState } from 'react';
import { ActionType, ActionsMenuContext } from './interfaces';
import {
  Alert,
  Backdrop,
  Button,
  CircularProgress,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle
} from '@mui/material';
import { ActionsMenu } from './actionsMenu/ActionsMenu';
import { Plant } from '../../common/models/Plant';
import { useDeletePlantMutation } from '../../common/RTK/deletePlant/deletePlant';
import useInvalidateCache from '../../common/hooks/useInvalidateCache';

interface PlantActionsMenu {
  setOpenDialog: React.Dispatch<React.SetStateAction<boolean>>;
  openDialog: boolean;
  plant: Plant;
}

const PlantActionsMenu = ({ setOpenDialog, openDialog, plant }: PlantActionsMenu) => {
  const { invalidateCache } = useInvalidateCache();
  const [deletePlant, { data, isLoading, isError }] = useDeletePlantMutation();
  const [context, setContext] = useState<ActionsMenuContext>({
    action: undefined,
    plant: plant
  } as ActionsMenuContext);

  const onActionChange = (action: ActionType) => setContext((prev) => ({ ...prev, action }));

  const onCancel = () => {
    setContext((prev) => ({ ...prev, action: undefined }));
    setOpenDialog(false);
  };

  const onSubmit = async () => {
    if (data !== undefined || isError) {
      invalidateCache();
      onCancel();
      return;
    }
    if (context.action === ActionType.DELETE) {
      await deletePlant({ plantId: plant.id! });
    }
  };

  return (
    <Dialog open={openDialog} onClose={() => onCancel()}>
      <Backdrop open={isLoading}>
        <CircularProgress color="secondary" size={20} />
      </Backdrop>
      <DialogTitle>Update plant</DialogTitle>
      <DialogContent>
        <ActionsMenu action={context.action} onActionChange={onActionChange} />
      </DialogContent>
      {data && (
        <Alert variant="outlined" severity="success">
          Successfully deleted plant.
        </Alert>
      )}
      {isError && (
        <Alert variant="outlined" severity="error">
          Something went wrong, please try again later.
        </Alert>
      )}
      <DialogActions>
        <Button variant="outlined" disabled={false} onClick={() => onCancel()}>
          Cancel
        </Button>
        <Button
          variant="contained"
          disabled={!context.action || isLoading}
          onClick={() => onSubmit()}
        >
          {data !== undefined || isError ? 'Close' : 'Submit'}
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default PlantActionsMenu;
