import React from 'react';
import { Menu } from '@mui/material';
import styles from './customMenu.styles';

interface CustomMenuProps {
  setOpenMenu: React.Dispatch<React.SetStateAction<boolean>>;
  openMenu: boolean;
  menuActions: Function;
}

export const CustomMenu = ({ setOpenMenu, openMenu, menuActions }: CustomMenuProps) => {
  return (
    <Menu
      open={openMenu}
      onClose={() => setOpenMenu(!openMenu)}
      MenuListProps={{
        'aria-labelledby': 'basic-button'
      }}
      anchorOrigin={{
        vertical: 'top',
        horizontal: 'left'
      }}
      transformOrigin={{
        vertical: 'top',
        horizontal: 'left'
      }}
      sx={styles.customMenuWrapper}
    >
      {menuActions()}
    </Menu>
  );
};

export default CustomMenu;
