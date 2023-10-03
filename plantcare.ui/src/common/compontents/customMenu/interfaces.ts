import React from "react";

export interface CustomMenuProps {
    setOpenMenu:  React.Dispatch<React.SetStateAction<boolean>>;
    openMenu: boolean;
    menuActions: Function;
}
