import { Box, Typography } from '@mui/material';
import { useEffect, useState } from 'react';

export interface CountdownTimerProps {
  timeToCountDown: number;
  onTimeOut: () => void;
}

export const CountdownTimer = ({ timeToCountDown, onTimeOut }: CountdownTimerProps) => {
  const [timeLeft, setTimeLeft] = useState(timeToCountDown);

  useEffect(() => {
    if (timeLeft <= 0) {
      onTimeOut();
      return;
    }

    const interval = setInterval(() => {
      setTimeLeft((prev) => prev - 1);
    }, 1000);

    return () => window.clearInterval(interval);
  });

  const minutes = Math.floor(timeLeft / 60);
  const seconds = timeLeft % 60;
  const formattedTime = `${minutes}:${seconds < 10 ? '0' : ''}${seconds}`;

  return (
    <Typography variant="h3" sx={{ mt: 5 }}>
      {formattedTime}
    </Typography>
  );
};
