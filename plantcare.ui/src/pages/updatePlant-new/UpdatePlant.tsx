import React from 'react';
import { Dialog } from '@mui/material';

interface UpdatePlantProps {
  setOpenDialog: React.Dispatch<React.SetStateAction<boolean>>;
  openDialog: boolean;
}

const UpdatePlant = ({ setOpenDialog, openDialog }: UpdatePlantProps) => {

  return <Dialog open={openDialog} onClose={() => setOpenDialog(false)}></Dialog>;
};

export default UpdatePlant;
