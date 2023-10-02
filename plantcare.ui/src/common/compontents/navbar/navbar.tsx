import React from "react";
import {AppBar, Box, Button, IconButton, Toolbar, Typography} from "@mui/material";
import MenuIcon from '@mui/icons-material/Menu';
import styles from './navbar.styles';

export const Navbar = () => {

    return(
        <Box sx={styles.wrapper}>
            <AppBar position="static">
                <Toolbar>
                    <IconButton
                        size="large"
                        edge="start"
                        color="inherit"
                        aria-label="menu"
                        sx={{ mr: 2 }}
                    >
                        <MenuIcon />
                    </IconButton>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        PlantCare
                    </Typography>
                </Toolbar>
            </AppBar>
        </Box>
    )
}

export default Navbar;