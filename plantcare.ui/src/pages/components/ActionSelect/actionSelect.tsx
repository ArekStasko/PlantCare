import React from 'react';
import { Box, FormControlLabel, Radio, RadioGroup, Typography } from '@mui/material';
import styles from './actionSelect.styles';
import { Controller, useFormContext } from 'react-hook-form';

export const ActionSelect = () => {
  const { control, getValues } = useFormContext();

  return (
    <>
      <Box sx={styles.actionSelectWrapper}>
        <Box sx={styles.actionSelectSubtitle}>
          <Typography variant="h6">Select Action</Typography>
          <Typography variant="subtitle2">
            By clicking Update you will be able to change your data.
          </Typography>
          <Typography variant="subtitle2">By clicking Delete you will remove your data.</Typography>
        </Box>
        <Controller
          control={control}
          name="flow"
          render={({ field }) => (
            <RadioGroup {...field}>
              <FormControlLabel value="update" control={<Radio />} label="Update" />
              <FormControlLabel value="delete" control={<Radio />} label="Delete" />
            </RadioGroup>
          )}
        />
      </Box>
    </>
  );
};

export default ActionSelect;
