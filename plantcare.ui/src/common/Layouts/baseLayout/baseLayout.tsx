import React, { FunctionComponent, ReactElement, useEffect, useState } from 'react';
import { Box } from '@mui/material';
import Navbar from '../../compontents/navbar/navbar';
import styles from './baseLayout.styles';

type BaseLayoutProps = {
  children: ReactElement;
};

export const BaseLayout: FunctionComponent<BaseLayoutProps> = ({ children }) => {
  return (
    <Box sx={styles.container}>
      <Navbar />
      <Box sx={styles.wrapper}>{children}</Box>
    </Box>
  );
};

export default BaseLayout;
