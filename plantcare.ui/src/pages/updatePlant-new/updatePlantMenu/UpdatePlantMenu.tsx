import { DialogContentText, FormControlLabel, Radio, RadioGroup } from '@mui/material';
import React, { useEffect } from "react";
import { UpdatePlantActionType } from '../interfaces';

export interface UpdatePlantProps{
  action?: UpdatePlantActionType,
  onActionChange: (action: UpdatePlantActionType) => void;
}

export const UpdatePlantMenu = ({ action, onActionChange }: UpdatePlantProps) => {
  const [selectedAction, setSelectedAction] = React.useState<UpdatePlantActionType | undefined>(action);

  useEffect(() => {
    if(!selectedAction) return;
    onActionChange(selectedAction)
  }, [selectedAction]);

  return (
    <>
      <DialogContentText>
        Choose menu option depending on what operations you want to perform
      </DialogContentText>
      <RadioGroup>
        <FormControlLabel
          value={UpdatePlantActionType.UPDATE}
          control={<Radio onClick={() => setSelectedAction(UpdatePlantActionType.UPDATE)} />}
          label="Update"
        />
        <FormControlLabel
          value={UpdatePlantActionType.DELETE}
          control={<Radio onClick={() => setSelectedAction(UpdatePlantActionType.DELETE)} />}
          label="Delete"
        />
      </RadioGroup>
    </>
  );
};

