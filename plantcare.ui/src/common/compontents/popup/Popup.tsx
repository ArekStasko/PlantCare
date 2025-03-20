import {
  Alert,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle
} from "@mui/material";
import React from "react";

export enum PopupStatus {
  success = "success",
  failure = "error",
  warning = "warning",
  information = "info"
}

interface PopupProps {
  status: PopupStatus,
  openPopup: boolean;
  titleText: string;
  contentText: string;
  confirmText: string;
  confirmAction: () => void;
}

export const Popup = ({ openPopup, titleText, contentText, confirmText, confirmAction, status }: PopupProps) => {

  return (
    <Dialog open={openPopup}>
      <Alert variant="outlined" severity={status}>
      <DialogTitle id="alert-dialog-title">{titleText}</DialogTitle>
      <DialogContent>
        <DialogContentText>
          {contentText}
        </DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button variant="outlined" color={status} onClick={() => confirmAction()}>
          {confirmText}
        </Button>
      </DialogActions>
      </Alert>
    </Dialog>
  );
};

export default Popup;