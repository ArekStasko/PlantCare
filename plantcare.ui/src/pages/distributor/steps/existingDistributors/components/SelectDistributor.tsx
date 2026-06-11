import { Box, InputLabel, MenuItem, Select } from '@mui/material';
import { Distributor } from '@arekstasko/plantcare-api-client';
import { Controller, useForm } from 'react-hook-form';
import styles from '../../../../plant/steps/place/place.styles';
import React from 'react';
import { yupResolver } from '@hookform/resolvers/yup';
import validators from '../../../../../common/services/Validators';

type SelectDistributorProps = {
  distributors: Distributor[];
  onDistributorSelect: (id: number) => void;
  distributorId?: number;
};

export const SelectDistributor = ({
  distributors,
  onDistributorSelect,
  distributorId
}: SelectDistributorProps) => {
  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.selectDistributorSchema),
    defaultValues: {
      distributor: distributorId?.toString() ?? ''
    }
  });

  const {
    getValues,
    formState: { isValid },
    control
  } = methods;

  return (
    <Box>
      <InputLabel id="SelectDistributor">
        Choose a distributor to which your plant will be assigned
      </InputLabel>
      <Controller
        control={control}
        name="distributor"
        render={({ field: { onChange, value }, formState: { errors } }) => (
          <Select
            sx={styles.typeSelect}
            onChange={onChange}
            value={value}
            defaultValue={distributorId ?? ''}
            id="distributor"
            error={!!errors.distributor}
            labelId="SelectDistributor"
          >
            {distributors!.map((d) => (
              <MenuItem value={d.id}>{d.name}</MenuItem>
            ))}
          </Select>
        )}
      />
    </Box>
  );
};
