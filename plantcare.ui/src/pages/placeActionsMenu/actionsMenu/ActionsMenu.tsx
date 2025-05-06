import { Box } from '@mui/material';
import { ActionType } from '../../../common/interfaces';

export interface PlaceActionsMenuProps {
  action?: ActionType;
  onActionChange: (action: ActionType) => void;
}

export const ActionsMenu = ({ action, onActionChange }: PlaceActionsMenuProps) => {
  return <Box></Box>;
};
