import React from "react";
import {Menu, MenuItem} from "@mui/material";
import {CustomMenuProps} from "./interfaces";

export const CustomMenu = ({setOpenMenu, openMenu, menuActions} : CustomMenuProps) => {

    return(
        <Menu
            id="basic-menu"
            open={openMenu}
            onClose={() => setOpenMenu(!openMenu)}
            MenuListProps={{
                'aria-labelledby': 'basic-button',
            }}
        >
            {menuActions()}
        </Menu>
    )
}

export default CustomMenu;