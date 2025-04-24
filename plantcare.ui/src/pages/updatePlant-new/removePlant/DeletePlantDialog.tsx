import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  FormControlLabel,
  Radio,
  RadioGroup
} from '@mui/material';
import React from 'react';
import { UpdatePlantActionType } from '../interfaces';
import { useNavigate } from 'react-router';

const DeletePlantDialog = () => {
  const navigate = useNavigate();

  const onSubmit = () => {
    console.log(action);
  };

  return (
    <Dialog open={openDialog} onClose={() => setOpenDialog(false)}>
      <DialogTitle>Update Plant</DialogTitle>
      <DialogContent>
        <DialogContentText>
          Choose menu option depending on what operations you want to perform
        </DialogContentText>
        <RadioGroup>
          <FormControlLabel
            value={UpdatePlantActionType.UPDATE}
            control={<Radio onClick={() => setAction(UpdatePlantActionType.UPDATE)} />}
            label="Update"
          />
          <FormControlLabel
            value={UpdatePlantActionType.DELETE}
            control={<Radio onClick={() => setAction(UpdatePlantActionType.DELETE)} />}
            label="Delete"
          />
        </RadioGroup>
      </DialogContent>
      <DialogActions>
        <Button variant="outlined" onClick={() => setOpenDialog(!openDialog)}>
          Back
        </Button>
        <Button variant="contained" onClick={() => onNext()}>
          Next
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default DeletePlantDialog;
