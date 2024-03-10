import React, { useEffect } from 'react';
import {
  Box,
  FormControl,
  FormControlLabel,
  FormLabel,
  Radio,
  RadioGroup,
  Typography
} from '@mui/material';
import styles from './actionSelect.styles';
import DeleteDialog from '../../../../common/compontents/DeleteIcon/DeleteDialog/deleteDialog';
import { Controller, useFormContext } from 'react-hook-form';
import CustomAlert from '../../../../common/compontents/customAlert/customAlert';

export const ActionSelect = () => {
  const [openDialog, setOpenDialog] = React.useState(false);
  const [isError, setIsError] = React.useState(false);

  const { control, getValues } = useFormContext();

  return (
    <>
      {isError && <CustomAlert message="Sorry, something went wrong." type="error" />}
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
              <FormControlLabel
                value="delete"
                control={<Radio onClick={() => setOpenDialog(!openDialog)} />}
                label="Delete"
              />
            </RadioGroup>
          )}
        />
      </Box>
      <DeleteDialog
        openDialog={openDialog}
        setOpenDialog={setOpenDialog}
        resourceId={getValues('id')}
        resourceType={'place'}
        redirectToPortfolio
        callIsError={setIsError}
      />
    </>
  );
};

export default ActionSelect;
