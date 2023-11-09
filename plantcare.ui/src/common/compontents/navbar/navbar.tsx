import React from 'react';
import {
  AppBar,
  Box,
  Button,
  Divider,
  ListItemIcon,
  ListItemText,
  MenuItem,
  Toolbar,
  Typography
} from '@mui/material';
import DashboardIcon from '@mui/icons-material/Dashboard';
import styles from './navbar.styles';
import CustomMenu from '../customMenu/customMenu';
import { useNavigate } from 'react-router';
import { ActionsToPerform, ActionsTranslation } from '../../../app/routing/routingConstants';

export const Navbar = () => {
  const [openMenu, setOpenMenu] = React.useState(false);
  const navigate = useNavigate();

  const redirectUser = (pathToRedirect: string) => {
    setOpenMenu(!openMenu);
    navigate(pathToRedirect);
  };

  const menuActions = () =>
    ActionsToPerform.map((action) => (
      <Box key={action}>
        <MenuItem onClick={() => redirectUser(action)}>
          <ListItemIcon>
            <DashboardIcon fontSize="small" />
          </ListItemIcon>
          <ListItemText>{ActionsTranslation[action]}</ListItemText>
        </MenuItem>
        <Divider variant="inset" />
      </Box>
    ));

  return (
    <Box sx={styles.wrapper}>
      <AppBar position="static">
        <Toolbar>
          <Button variant="contained" onClick={() => setOpenMenu(!openMenu)}>
            Actions
          </Button>
          <CustomMenu setOpenMenu={setOpenMenu} openMenu={openMenu} menuActions={menuActions} />
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            PlantCare
          </Typography>
        </Toolbar>
      </AppBar>
    </Box>
  );
};

export default Navbar;
