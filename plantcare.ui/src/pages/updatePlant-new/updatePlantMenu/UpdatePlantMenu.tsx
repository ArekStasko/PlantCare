import {
  DialogContentText,
  FormControlLabel,
  Radio,
  RadioGroup
} from '@mui/material';
import React from 'react';
import { UpdatePlantActionType, UpdatePlantContext } from "../interfaces";
import { useNavigate } from 'react-router';
import { DialogWizardStep } from "../../../common/dialogWizard/dialogWizardStep/DialogWizardStep";
import { DialogWizardStepProps } from "../../../common/dialogWizard/interfaces";


const UpdatePlantMenu = ({dialogWizardController}: DialogWizardStepProps<UpdatePlantContext>) => {
  const [action, setAction] = React.useState<UpdatePlantActionType | undefined>(undefined);
  const navigate = useNavigate();

  const onNext = () => {
    console.log(action);
  };

  return (
    <DialogWizardStep
      nextButton={{
        onClick: onNext,
        isDisabled: false,
        title: 'Next'
      }}
      cancelButton={{
        onClick: () => dialogWizardController.onCancel(),
        isDisabled: false,
        title: 'Cancel'
      }}
      backButton={{
        onClick: () => {},
        isDisabled: true,
        title: 'Back'
      }}
      title="Update Plant Menu"
    >
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
    </DialogWizardStep>
  );
};

export default UpdatePlantMenu;
