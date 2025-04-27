import { DialogWizardStepProviderProps } from "../interfaces";
import { Button, DialogActions, DialogContent, DialogTitle } from "@mui/material";
import React from "react";


export const DialogWizardStep = <T,>({children,
                                       nextButton,
                                       cancelButton,
                                       backButton,
                                       title,
                                       sx}: DialogWizardStepProviderProps<T>) => {

  return (
    <>
      <DialogTitle>
        {title}
      </DialogTitle>
      <DialogContent>
        {children}
      </DialogContent>
      <DialogActions>
        <Button
          variant="outlined"
          disabled={backButton.isDisabled}
          onClick={() => backButton.onClick}
        >
          {backButton.title}
        </Button>
        <Button
          variant="outlined"
          disabled={cancelButton.isDisabled}
          onClick={() => cancelButton.onClick}
        >
          {cancelButton.title}
        </Button>
        <Button
          variant="contained"
          disabled={nextButton.isDisabled}
          onClick={() => nextButton.onClick}
        >
          {nextButton.title}
        </Button>
      </DialogActions>
    </>
  )
}