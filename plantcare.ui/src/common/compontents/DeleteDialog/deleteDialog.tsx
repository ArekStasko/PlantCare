import {Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Typography} from "@mui/material";
import React from "react";

interface DeleteDialogProps{
    setOpenDialog:  React.Dispatch<React.SetStateAction<boolean>>;
    openDialog: boolean;
    resourceId: number;
    resourceType: string;
}

export const DeleteDialog = (props: DeleteDialogProps) => {

    return(
        <Dialog
            open={props.openDialog}
            onClose={props.setOpenDialog}
        >
            <DialogTitle id="alert-dialog-title">
                {`Do you want do delete this ${props.resourceType} ?`}
            </DialogTitle>
            <DialogContent>
                    {
                        props.resourceType === "plant" ? (
                            <DialogContentText>
                                By clicking confirm this plant will be deleted permanently.
                                You won't be able to restore the deleted resource.
                            </DialogContentText>
                        ) : (
                            <DialogContentText>
                                By clicking confirm this place and plants that belongs to it will be deleted permanently.
                                You won't be able to restore the deleted resource.
                            </DialogContentText>
                        )
                    }
            </DialogContent>
            <DialogActions>
                <Button variant="contained" onClick={() => props.setOpenDialog(!props.openDialog)}>Cancel</Button>
                <Button variant="outlined" color="error" onClick={() => props.setOpenDialog(!props.openDialog)} >
                    Confirm
                </Button>
            </DialogActions>
        </Dialog>
    )
}

export default DeleteDialog;