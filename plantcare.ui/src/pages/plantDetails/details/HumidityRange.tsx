import {
  Box,
  Button,
  CircularProgress,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  Slider,
  TextField,
  Typography
} from '@mui/material';
import React from 'react';
import styles from './details.styles';
import { useSetHumidityValuesMutation } from '../../../common/RTK/Plant/Plant';
import { UpdatePlantHumidityValues } from '@arekstasko/plantcare-api-client';

interface HumidityRangeProps {
  onOpenChange: React.Dispatch<React.SetStateAction<boolean>>;
  open: boolean;
  id: number;
}

export const HumidityRange = ({ onOpenChange, open, id }: HumidityRangeProps) => {
  const [setHumidityValues, { isLoading }] = useSetHumidityValuesMutation();
  const [value, setValue] = React.useState<number[]>([0, 100]);

  const handleChange = (event: Event, value: number | number[], activeThumb: number) => {
    if (typeof value === 'number') {
      setValue([value]);
      return;
    }
    setValue(value);
  };

  const onConfirm = async () => {
    const body = {
      plantId: id,
      minHumidity: value[0],
      maxHumidity: value[1]
    } as UpdatePlantHumidityValues;
    await setHumidityValues(body);
  };

  return (
    <Dialog open={open} onClose={() => onOpenChange(false)}>
      <DialogTitle>Humidity range values</DialogTitle>
      <DialogContent>
        <DialogContentText>
          Provide the maximum and minimum percentage value for your plant
        </DialogContentText>
        <Box sx={styles.humidityRangeWrapper}>
          <Slider
            getAriaLabel={() => 'Humidity range'}
            value={value}
            onChange={handleChange}
            valueLabelDisplay="auto"
          />
          <Typography>{`Humidity range: ${value[0]}% - ${value[1]}%`}</Typography>
        </Box>
      </DialogContent>
      <DialogActions>
        <Button variant="contained" onClick={() => onOpenChange(false)}>
          Back
        </Button>
        {isLoading ? (
          <CircularProgress />
        ) : (
          <Button variant="outlined" color="success" onClick={async () => await onConfirm()}>
            Confirm
          </Button>
        )}
      </DialogActions>
    </Dialog>
  );
};
