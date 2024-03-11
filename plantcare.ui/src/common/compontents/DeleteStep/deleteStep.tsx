import { Alert, Box, Typography } from '@mui/material';
import React from 'react';

export interface DeleteStepProps {
  resourceType: string;
}

export const DeleteStep = ({ resourceType }: DeleteStepProps) => {
  return (
    <Alert variant="outlined" severity="warning">
      <Typography>{`Do you want do delete this ${resourceType} ?`}</Typography>
      <Box>
        {resourceType === 'plant' ? (
          <Typography>
            By clicking confirm this plant will be deleted permanently. You won't be able to restore
            the deleted resource.
          </Typography>
        ) : (
          <Typography>
            By clicking confirm this place and plants that belongs to it will be deleted
            permanently. You won't be able to restore the deleted resource.
          </Typography>
        )}
      </Box>
    </Alert>
  );
};

export default DeleteStep;
