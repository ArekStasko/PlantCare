import React from "react";
import {
    AppBar,
    Box,
    Button,
    Divider,
    IconButton,
    ListItemIcon,
    ListItemText,
    MenuItem,
    Toolbar,
    Typography
} from "@mui/material";
import MenuIcon from '@mui/icons-material/Menu';
import styles from './navbar.styles';
import AddIcon from '@mui/icons-material/Add';
import EditIcon from '@mui/icons-material/Edit';
import CustomMenu from "../customMenu/customMenu";

export const Navbar = () => {
    const [openMenu, setOpenMenu] = React.useState(false);

    const menuActions = () => (
        <>
            <MenuItem onClick={() => setOpenMenu(!openMenu)}>
                <ListItemIcon>
                    <AddIcon fontSize="small" />
                </ListItemIcon>
                <ListItemText>Create Plant</ListItemText>
            </MenuItem>
            <Divider />
            <MenuItem onClick={() => setOpenMenu(!openMenu)}>
                <ListItemIcon>
                    <EditIcon fontSize="small" />
                </ListItemIcon>
                <ListItemText>Update Plant</ListItemText>
            </MenuItem>
        </>
    )

    return(
        <Box sx={styles.wrapper}>
            <AppBar position="static">
                <Toolbar>
                    <Button
                        variant="contained"
                        onClick={() => setOpenMenu(!openMenu)}
                    >
                        Actions
                    </Button>
                    <CustomMenu setOpenMenu={setOpenMenu} openMenu={openMenu} menuActions={menuActions}/>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        PlantCare
                    </Typography>
                </Toolbar>
            </AppBar>
        </Box>
    )
}

export default Navbar;