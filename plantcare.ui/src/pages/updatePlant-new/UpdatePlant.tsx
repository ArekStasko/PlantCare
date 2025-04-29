import React, { useState } from "react";
import { UpdatePlantActionType, UpdatePlantContext } from "./interfaces";
import {
  Alert,
  Backdrop,
  Button,
  CircularProgress,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle
} from "@mui/material";
import { UpdatePlantMenu } from "./updatePlantMenu/UpdatePlantMenu";
import { Plant } from "../../common/models/Plant";
import { useDeletePlantMutation } from "../../common/RTK/deletePlant/deletePlant";
import { PopupStatus } from "../../common/components/popup/Popup";
import { useDispatch } from "react-redux";
import plantcareApi from "../../app/api/plantcareApi";

interface UpdatePlantProps {
  setOpenDialog: React.Dispatch<React.SetStateAction<boolean>>;
  openDialog: boolean;
  plant: Plant;
}

const UpdatePlant = ({ setOpenDialog, openDialog, plant }: UpdatePlantProps) => {
  const dispatch = useDispatch();
  const [deletePlant, {data, isLoading, isError}] = useDeletePlantMutation();
  const [context, setContext] = useState<UpdatePlantContext>({
    action: undefined,
    plant: plant
  } as UpdatePlantContext)

  const onActionChange = (action: UpdatePlantActionType) => setContext(prev => ({ ...prev, action }));

  const onCancel = () => {
    setContext(prev => ({ ...prev, action: undefined }));
    setOpenDialog(false)
  }

  const onSubmit = async () => {
    if(data !== undefined || isError) {
      dispatch(plantcareApi.util.resetApiState())
      onCancel();
      return;
    }
    if(context.action === UpdatePlantActionType.DELETE) {
      await deletePlant({plantId: plant.id!});
    }
  }

  return (
    <Dialog open={openDialog} onClose={() => onCancel()}>
      <Backdrop open={isLoading}>
        <CircularProgress color="secondary" size={20} />
      </Backdrop>
      <DialogTitle>Update plant</DialogTitle>
      <DialogContent>
        <UpdatePlantMenu action={context.action} onActionChange={onActionChange} />
      </DialogContent>
      {
        data && (
          <Alert variant="outlined" severity='success'>
            Successfully deleted plant.
          </Alert>
        )
      }
      {
        isError && (
          <Alert variant="outlined" severity='error'>
           Something went wrong, please try again later.
          </Alert>
        )
      }
      <DialogActions>
        <Button
          variant="outlined"
          disabled={false}
          onClick={() => onCancel()}
        >
          Cancel
        </Button>
        <Button
          variant="contained"
          disabled={!context.action || isLoading }
          onClick={() => onSubmit()}
        >
          {(data !== undefined || isError) ? 'Close' : 'Submit'}
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default UpdatePlant;
