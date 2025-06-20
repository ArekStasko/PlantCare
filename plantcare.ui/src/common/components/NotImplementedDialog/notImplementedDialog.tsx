import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle
} from '@mui/material';
import React from 'react';
import { useNavigate } from 'react-router';
import RoutingConstants from '../../../app/routing/routingConstants';

export const CancelDialog = () => {
  const navigate = useNavigate();

  return (
    <Dialog open={true}>
      <DialogTitle id="alert-dialog-title">{`This functionality is not implemented`}</DialogTitle>
      <DialogContent>
        <DialogContentText>
          This functionality is not implemented yet. It will be implemented in future versions of
          application. Click Back button to go back to dashboard
        </DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button variant="outlined" onClick={() => navigate(RoutingConstants.root)}>
          Back
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default CancelDialog;
