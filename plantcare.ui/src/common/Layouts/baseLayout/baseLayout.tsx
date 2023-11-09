import React, { FunctionComponent, ReactElement } from 'react';
import { Box, Container, Typography } from '@mui/material';
import Navbar from '../../compontents/navbar/navbar';
import styles from './baseLayout.styles';

type BaseLayoutProps = {
  children: ReactElement;
};

export const BaseLayout: FunctionComponent<BaseLayoutProps> = ({ children }) => {
  return (
    <Box sx={styles.container}>
      <Navbar />
      <Box>{children}</Box>
    </Box>
  );
};

export default BaseLayout;
