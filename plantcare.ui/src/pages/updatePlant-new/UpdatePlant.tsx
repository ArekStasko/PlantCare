import React from 'react';
import DialogWizard from "../../common/dialogWizard/DialogWizard";
import { DialogWizardController, DialogWizardStep } from "../../common/dialogWizard/interfaces";
import { UpdatePlantContext } from "./interfaces";
import UpdatePlantMenu from "./updatePlantMenu/UpdatePlantMenu";

interface UpdatePlantProps {
  setOpenDialog: React.Dispatch<React.SetStateAction<boolean>>;
  openDialog: boolean;
}

const UpdatePlant = ({ setOpenDialog, openDialog }: UpdatePlantProps) => {
  const initialContext = {} as UpdatePlantContext

  const steps = [
    {
      order: 0,
      title: 'Menu',
      getStep: (wizardController: DialogWizardController<UpdatePlantContext>) => (
        <UpdatePlantMenu dialogWizardController={wizardController} />
      )
    } as DialogWizardStep<UpdatePlantContext>,
  ];

  return (
    <DialogWizard<UpdatePlantContext>
      initialContext={initialContext}
      steps={steps}
      open={openDialog}
      onClose={(close: boolean) => setOpenDialog(close)}
    />
  )
};

export default UpdatePlant;
