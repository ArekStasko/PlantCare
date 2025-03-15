import { Box, Paper, Typography } from '@mui/material';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import CircleIcon from '@mui/icons-material/Circle';
import RadioButtonUncheckedIcon from '@mui/icons-material/RadioButtonUnchecked';
import { ProgressTileProps } from './interfaces';
import styles from './wizardProgress.styles';

export const ProgressTile = ({ title, active, completed }: ProgressTileProps) => {
  return (
    <Paper elevation={3} sx={styles.progressTile}>
      <Box>
        {active && <CircleIcon color="info" />}
        {completed && <CheckCircleIcon color="success" />}
        {!active && !completed && <RadioButtonUncheckedIcon color="primary" />}
      </Box>
      <Typography>{title}</Typography>
    </Paper>
  );
};
