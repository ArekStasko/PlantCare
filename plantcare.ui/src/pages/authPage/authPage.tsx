import React, { useEffect } from 'react';
import { Box, Typography } from '@mui/material';
import { useParams } from 'react-router';

export const AuthPage = () => {
  const { id, token } = useParams();

  useEffect(() => {
    if (!id || !token) return;
  }, []);

  return (
    <Box>
      <Typography>AuthPage</Typography>
    </Box>
  );
};

export default AuthPage;
