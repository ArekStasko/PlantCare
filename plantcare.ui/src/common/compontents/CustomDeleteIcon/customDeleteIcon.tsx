import { IconButton, Tooltip } from '@mui/material';
import RoutingConstants from '../../../app/routing/routingConstants';
import React from 'react';
import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline';
import DeleteForeverIcon from '@mui/icons-material/DeleteForever';
import DeleteDialog from '../DeleteDialog/deleteDialog';

interface DeleteIconProps {
  resourceName: string;
  resourceId: number;
  resourceType: string;
}

export const CustomDeleteIcon = (props: DeleteIconProps) => {
  const [open, setOpen] = React.useState(false);

  return (
    <>
      <Tooltip title={`Delete ${props.resourceName}`} arrow>
        <IconButton onClick={() => setOpen(!open)} size="large" sx={{ mr: 5 }} color="error">
          {props.resourceType === 'plant' ? <DeleteOutlineIcon /> : <DeleteForeverIcon />}
        </IconButton>
      </Tooltip>
      <DeleteDialog
        openDialog={open}
        setOpenDialog={setOpen}
        resourceId={props.resourceId}
        resourceType={props.resourceType}
      />
    </>
  );
};

export default CustomDeleteIcon;
