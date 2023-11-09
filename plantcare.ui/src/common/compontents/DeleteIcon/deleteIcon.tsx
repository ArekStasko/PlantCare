import { IconButton, Tooltip } from '@mui/material';
import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline';
import DeleteForeverIcon from '@mui/icons-material/DeleteForever';
import DeleteDialog from './DeleteDialog/deleteDialog';
import React from 'react';

interface deleteIconProps {
  resourceName: string;
  resourceType: string;
  resourceId: number;
}
export const DeleteIcon = ({ resourceName, resourceType, resourceId }: deleteIconProps) => {
  const [openDialog, setOpenDialog] = React.useState(false);

  return (
    <>
      <Tooltip title={`Delete ${resourceName}`} arrow>
        <IconButton
          onClick={() => setOpenDialog(!openDialog)}
          size="large"
          sx={{ mr: 5 }}
          color="error">
          {resourceType === 'plant' ? (
            <>
              <DeleteForeverIcon />
            </>
          ) : (
            <>
              <DeleteOutlineIcon />
            </>
          )}
        </IconButton>
      </Tooltip>
      <DeleteDialog
        openDialog={openDialog}
        setOpenDialog={setOpenDialog}
        resourceId={resourceId}
        resourceType={resourceType}
      />
    </>
  );
};

export default DeleteIcon;
