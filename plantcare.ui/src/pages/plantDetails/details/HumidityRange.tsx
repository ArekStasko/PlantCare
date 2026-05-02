import {
  Box,
  Dialog, TextField, Typography
} from "@mui/material";
import React from "react";

interface HumidityRangeProps {
  onOpenChange: React.Dispatch<React.SetStateAction<boolean>>;
  open: boolean;
  id: number;
}

export const HumidityRange = ({onOpenChange, open, id}: HumidityRangeProps) => {

  return (
    <Dialog open={open} onClose={() => onOpenChange(false)}>
      <Box>
        <Typography>
          Minimum humidity percent value
        </Typography>
        <TextField/>
        <Typography>
          Maximum humidity percent value
        </Typography>
        <TextField/>
      </Box>
    </Dialog>
  )
}