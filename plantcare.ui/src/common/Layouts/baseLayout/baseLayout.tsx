import React, {FunctionComponent, ReactElement} from "react";
import {Box, Container, Typography} from "@mui/material";

type BaseLayoutProps = {
    children: ReactElement
}

export const BaseLayout: FunctionComponent<BaseLayoutProps> = ({children}) => {

    return(
        <Container>
            <Box>
                {children}
            </Box>
        </Container>
    )
}

export default BaseLayout