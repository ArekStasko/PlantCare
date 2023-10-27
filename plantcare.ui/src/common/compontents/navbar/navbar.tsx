import React from "react";
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
} from "@mui/material";
import DashboardIcon from '@mui/icons-material/Dashboard';
import styles from './navbar.styles';
import AddIcon from '@mui/icons-material/Add';
import EditIcon from '@mui/icons-material/Edit';
import CustomMenu from "../customMenu/customMenu";
import {useNavigate} from "react-router";
import RoutingConstants, {ActionsToPerform, ActionsTranslation} from "../../../app/routing/routingConstants";
import {useAppDispatch, useAppSelector} from "../../hooks";
import {update} from "../../slices/routeSlice/routeSlice"

export const Navbar = () => {
    const currentRoute = useAppSelector(state => state.route.currentRoute)
    const dispatch = useAppDispatch();
    const [openMenu, setOpenMenu] = React.useState(false);
    const navigate = useNavigate();

    const redirectUser = (pathToRedirect: string) => {
        setOpenMenu(!openMenu);
        dispatch(update(pathToRedirect))
        navigate(pathToRedirect);
    }

    const menuActions = () =>
        ActionsToPerform.map((action, index) => {
            if(action === currentRoute) return null

            return (
                        <>
                            <MenuItem onClick={() => redirectUser(action)}>
                                <ListItemIcon>
                                    <DashboardIcon fontSize="small" />
                                </ListItemIcon>
                                <ListItemText>{ActionsTranslation[action]}</ListItemText>
                            </MenuItem>
                            <Divider />
                        </>
                    )
        })


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