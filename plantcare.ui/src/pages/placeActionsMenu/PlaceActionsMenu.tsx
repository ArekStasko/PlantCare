import React, { useState } from 'react';
import {
  Backdrop,
  Button,
  CircularProgress,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle
} from '@mui/material';
import { Place } from '../../common/models/Place';
import useInvalidateCache from '../../common/hooks/useInvalidateCache';
import { useNavigate } from 'react-router';
import { ActionsMenuContext } from './interfaces';
import { ActionType } from '../../common/interfaces';
import CustomAlert from '../../common/components/customAlert/customAlert';
import RoutingConstants from '../../app/routing/routingConstants';
import { useDeletePlaceMutation } from '../../common/RTK/deletePlace/deletePlace';
import { ActionsMenu } from '../../common/components/ActionsMenu/ActionsMenu';
import { PlaceContext, PlaceFlowType } from '../place/interfaces';

interface PlaceActionsMenuProps {
  closeDialog: () => void;
  openDialog: boolean;
  place: Place;
}

const PlaceActionsMenu = ({ closeDialog, openDialog, place }: PlaceActionsMenuProps) => {
  const [deletePlace, { data, isLoading, isError }] = useDeletePlaceMutation();
  const { invalidateCache } = useInvalidateCache();
  const navigate = useNavigate();

  const [context, setContext] = useState<ActionsMenuContext>({
    action: undefined,
    place: place
  } as ActionsMenuContext);

  const onActionChange = (action: ActionType) => setContext((prev) => ({ ...prev, action }));

  const onCancel = () => {
    setContext((prev) => ({ ...prev, action: undefined }) as ActionsMenuContext);
    closeDialog();
  };

  const onSubmit = async () => {
    if (data !== undefined || isError) {
      invalidateCache();
      onCancel();
      return;
    }

    if (context.action === ActionType.DELETE) {
      await deletePlace({ placeId: place.id! });
      return;
    }

    const placeContext = {
      flowType: PlaceFlowType.UPDATE,
      id: place.id,
      name: place.name
    } as PlaceContext;

    navigate(RoutingConstants.createPlace, { state: placeContext });
  };

  return (
    <Dialog open={openDialog} onClose={() => onCancel()}>
      <Backdrop open={isLoading}>
        <CircularProgress color="secondary" size={20} />
      </Backdrop>
      <DialogTitle>Update place</DialogTitle>
      <DialogContent>
        <ActionsMenu action={context.action} onActionChange={onActionChange} />
      </DialogContent>
      <DialogContent sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
        {data && (
          <CustomAlert message="Successfully deleted place." type="success" sx={{ width: '70%' }} />
        )}
        {isError && (
          <CustomAlert
            sx={{ width: '70%' }}
            message="Something went wrong, please try again later."
            type="error"
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

export default PlaceActionsMenu;
