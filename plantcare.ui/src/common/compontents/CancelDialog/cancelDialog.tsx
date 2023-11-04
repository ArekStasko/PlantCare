import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  Typography
} from '@mui/material';
import React from 'react';
import { useNavigate } from 'react-router';
import RoutingConstants from '../../../app/routing/routingConstants';
import { update } from '../../slices/routeSlice/routeSlice';
import { useAppDispatch } from '../../hooks';

interface cancelDialogProps {
  setOpenDialog: React.Dispatch<React.SetStateAction<boolean>>;
  openDialog: boolean;
}

export const CancelDialog = ({ setOpenDialog, openDialog }: cancelDialogProps) => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const confirmDelete = async () => {
    dispatch(update(RoutingConstants.root));
    navigate(RoutingConstants.root);
  };

  return (
    <Dialog open={openDialog} onClose={setOpenDialog}>
      <DialogTitle id="alert-dialog-title">{`Do you want to cancel this process ?`}</DialogTitle>
      <DialogContent>
        <DialogContentText>
          Are you sure you want to cancel this process ? After clicking Confirm option you will be
          redirected to dashboard and progress will be deleted.
        </DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button variant="contained" onClick={() => setOpenDialog(!openDialog)}>
          Back
        </Button>
        <Button variant="outlined" color="warning" onClick={async () => await confirmDelete()}>
          Confirm
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default CancelDialog;
