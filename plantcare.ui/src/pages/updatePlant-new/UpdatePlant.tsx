import React from 'react';
import { Dialog } from '@mui/material';
import UpdatePlantMenu from './updatePlantMenu/UpdatePlantMenu';

interface UpdatePlantProps {
  setOpenDialog: React.Dispatch<React.SetStateAction<boolean>>;
  openDialog: boolean;
}

const UpdatePlant = ({ setOpenDialog, openDialog }: UpdatePlantProps) => {
  const steps = [
    {
      order: 1,
      component: () => <UpdatePlantMenu />
    }
  ];

  return <Dialog open={openDialog} onClose={() => setOpenDialog(false)}></Dialog>;
};

export default UpdatePlant;
