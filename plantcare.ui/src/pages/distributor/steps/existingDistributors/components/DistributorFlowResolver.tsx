import { Box, InputLabel, MenuItem, Select } from '@mui/material';
import { Distributor } from '@arekstasko/plantcare-api-client';
import { Controller, useForm } from 'react-hook-form';
import styles from '../../../../plant/steps/place/place.styles';
import React from 'react';
import { yupResolver } from '@hookform/resolvers/yup';
import validators from '../../../../../common/services/Validators';

type DistributorFlowResolverProps = {
  distributors: Distributor[];
  onDistributorSelect: (id: number) => void;
  distributorId?: number;
};

export const DistributorFlowResolver = ({
  distributors,
  onDistributorSelect,
  distributorId
}: DistributorFlowResolverProps) => {
  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.selectDistributorSchema),
    defaultValues: {
      distributor: distributorId ?? ''
    }
  });

  const {
    getValues,
    formState: { isValid },
    control
  } = methods;

  return (
    <Box>
      {distributors.length === 0 ? (
        <Box></Box>
      ) : (
        <Box>
          <InputLabel id="SelectDistributor">
            Choose a distributor to which your plant will be assigned
          </InputLabel>
          <Controller
            control={control}
            name="distributorId"
            render={({ field: { onChange, value }, formState: { errors } }) => (
              <Select
                sx={styles.typeSelect}
                onChange={onChange}
                value={value}
                defaultValue={distributorId ?? ''}
                id="distributorId"
                error={!!errors.place}
                labelId="SelectDistributor"
              >
                {distributors!.map((d) => (
                  <MenuItem value={d.id}>{d.name}</MenuItem>
                ))}
              </Select>
            )}
          />
        </Box>
      )}
    </Box>
  );
};
