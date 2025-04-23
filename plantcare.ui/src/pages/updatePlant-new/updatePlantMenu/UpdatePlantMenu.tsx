import {
  Dialog,
  DialogContent,
  DialogContentText,
  DialogTitle,
  FormControlLabel,
  Radio,
  RadioGroup
} from "@mui/material";
import React from "react";

interface UpdatePlantMenuProps {
  setOpenDialog: React.Dispatch<React.SetStateAction<boolean>>;
  openDialog: boolean;
}

const UpdatePlantMenu = ({setOpenDialog, openDialog}: UpdatePlantMenuProps) => {

  return(
    <Dialog open={openDialog} onClose={() => setOpenDialog(false)}>
      <DialogTitle>Update Plant</DialogTitle>
      <DialogContent>
        <DialogContentText>
          Choose menu option depending on what operations you want to perform
        </DialogContentText>
        <RadioGroup>
          <FormControlLabel value="update" control={<Radio />} label="Update" />
          <FormControlLabel value="delete" control={<Radio />} label="Delete" />
        </RadioGroup>
      </DialogContent>
    </Dialog>
  )
}

export default UpdatePlantMenu;