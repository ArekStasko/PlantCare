import { DialogContentText, FormControlLabel, Radio, RadioGroup } from '@mui/material';
import React, { useEffect } from 'react';
import { ActionType } from '../interfaces';

export interface UpdatePlantProps {
  action?: ActionType;
  onActionChange: (action: ActionType) => void;
}

export const ActionsMenu = ({ action, onActionChange }: UpdatePlantProps) => {
  const [selectedAction, setSelectedAction] = React.useState<ActionType | undefined>(action);

  useEffect(() => {
    if (!selectedAction) return;
    onActionChange(selectedAction);
  }, [selectedAction]);

  return (
    <>
      <DialogContentText>
        Choose menu option depending on what operations you want to perform
      </DialogContentText>
      <RadioGroup>
        <FormControlLabel
          value={ActionType.UPDATE}
          control={<Radio onClick={() => setSelectedAction(ActionType.UPDATE)} />}
          label="Update"
        />
        <FormControlLabel
          value={ActionType.DELETE}
          control={<Radio onClick={() => setSelectedAction(ActionType.DELETE)} />}
          label="Delete"
        />
      </RadioGroup>
    </>
  );
};
