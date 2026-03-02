import { Box, Card, FormControl, InputLabel, MenuItem, Select, Typography } from "@mui/material";
import styles from './statistics.styles';
import React, { useState } from 'react';
import { TelemetryType } from './telemetry/TelemetryType';
import GeneralHumidityMeasurementsChart from './telemetry/GeneralHumidityMeasurementsChart';
import AverageHumidityMeasurementsChart from './telemetry/AverageHumidityMeasurementsChart';

export interface StatisticsProps {
  moduleId: string;
}

const getTelemetryBasedOnType = (type: TelemetryType, moduleId: string) => {
  switch (type) {
    case TelemetryType.Average:
      return <AverageHumidityMeasurementsChart moduleId={moduleId} />;
    case TelemetryType.General:
      return <GeneralHumidityMeasurementsChart moduleId={moduleId} />;
  }
};

const Statistics = ({ moduleId }: StatisticsProps) => {
  const [telemetryType, setTelemetryType] = useState<TelemetryType>(TelemetryType.General);

  return (
    <Card variant="outlined" sx={styles.statisticsWrapper}>
      <Box sx={styles.telemetryTypeWrapper}>
        <Typography variant="h4">Telemetry</Typography>
        <FormControl sx={styles.telemetryTypeForm}>
          <InputLabel id="select-telemetry-type-label">Telemetry Type</InputLabel>
          <Select
            variant="standard"
            labelId="select-telemetry-type-label"
            id="select-telemetry-type"
            value={telemetryType}
            label="Telemetry Type"
            onChange={(e) => setTelemetryType(e.target.value as TelemetryType)}
          >
            <MenuItem value={TelemetryType.Average}>Average</MenuItem>
            <MenuItem value={TelemetryType.General}>General</MenuItem>
          </Select>
        </FormControl>
      </Box>
      {getTelemetryBasedOnType(telemetryType, moduleId)}
    </Card>
  );
};

export default Statistics;
