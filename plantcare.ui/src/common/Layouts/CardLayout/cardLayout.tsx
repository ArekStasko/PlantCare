import {Button, Card, CardActions, CardContent, Typography } from "@mui/material";
import React, {ReactElement} from "react";
import styles from './cardLayout.styles'

interface cardLayoutProps {
    children: ReactElement
}

export const CardLayout = ({children}: cardLayoutProps) => {

    return(
        <Card sx={styles.card}>
        <CardContent>
            {children}
        </CardContent>
        <CardActions>
            <Button size="large">Test action</Button>
        </CardActions>
        </Card>
    )
}

export default CardLayout;