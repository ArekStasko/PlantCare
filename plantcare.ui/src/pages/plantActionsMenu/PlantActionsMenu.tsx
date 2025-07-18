import React, { useState } from 'react';
import { ActionsMenuContext } from './interfaces';
import {
  Backdrop,
  Button,
  CircularProgress,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle
} from '@mui/material';
import { Plant } from '../../common/models/Plant';
import { useDeletePlantMutation } from '../../common/RTK/deletePlant/deletePlant';
import useInvalidateCache from '../../common/hooks/useInvalidateCache';
import CustomAlert from '../../common/components/customAlert/customAlert';
import { useNavigate } from 'react-router';
import RoutingConstants from '../../app/routing/routingConstants';
import { PlantContext, PlantFlowType } from '../plant/interfaces';
import { ActionType } from '../../common/interfaces';
import { ActionsMenu } from '../../common/components/ActionsMenu/ActionsMenu';

interface PlantActionsMenu {
  closeDialog: () => void;
  openDialog: boolean;
  plant: Plant;
}

const PlantActionsMenu = ({ closeDialog, openDialog, plant }: PlantActionsMenu) => {
  const { invalidateCache } = useInvalidateCache();
  const navigate = useNavigate();
  const [deletePlant, { data, isLoading, isError }] = useDeletePlantMutation();
  const [context, setContext] = useState<ActionsMenuContext>({
    action: undefined,
    plant: plant
  } as ActionsMenuContext);

  const onActionChange = (action: ActionType) => setContext((prev) => ({ ...prev, action }));

  const onCancel = () => {
    setContext((prev) => ({ ...prev, action: undefined }));
    closeDialog();
  };

  const onSubmit = async () => {
    if (data !== undefined || isError) {
      invalidateCache();
      onCancel();
      return;
    }

    if (context.action === ActionType.DELETE) {
      await deletePlant({ plantId: plant.id! });
      return;
    }

    const plantContext = {
      flowType: PlantFlowType.UPDATE,
      name: plant.name,
      description: plant.description,
      plantId: plant.id,
      place: plant.placeId.toString(),
      module: plant.moduleId,
      currentModule: plant.moduleId,
      type: plant.type
    } as PlantContext;

    navigate(RoutingConstants.plant, { state: plantContext });
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
      <DialogContent sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
        {data && (
          <CustomAlert message="Successfully deleted plant." type="success" sx={{ width: '70%' }} />
        )}
        {isError && (
          <CustomAlert
            message="Something went wrong, please try again later."
            type="error"
            sx={{ width: '70%' }}
          />
        )}
      </DialogContent>
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
