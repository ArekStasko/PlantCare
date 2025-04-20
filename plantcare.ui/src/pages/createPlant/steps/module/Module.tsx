import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import {
  AlertColor,
  Box,
  CircularProgress,
  InputLabel,
  MenuItem,
  Select,
  Typography
} from '@mui/material';
import { buttonAction, WizardStepProps } from '../../../../common/wizard/interfaces';
import { CreatePlantContext } from '../../interfaces';
import CustomAlert from '../../../../common/components/customAlert/customAlert';
import { Controller, useForm } from 'react-hook-form';
import React from 'react';
import { useGetModulesQuery } from '../../../../common/RTK/getModules/getModules';
import styles from './module.styles';
import { yupResolver } from '@hookform/resolvers/yup';
import validators from '../../../../common/services/Validators';

const Module = ({ wizardController }: WizardStepProps<CreatePlantContext>) => {
  const { data: modules, isLoading: modulesLoading } = useGetModulesQuery();

  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.selectModuleSchema),
    defaultValues: {
      module: wizardController.context.module ?? ''
    }
  });

  const {
    getValues,
    formState: { isValid },
    control
  } = methods;

  return (
    <WizardStep
      nextButton={{
        onClick: () => {
          wizardController.updateContext({
            ...wizardController.context,
            module: getValues('module')
          });
          wizardController.goToNextStep();
        },
        isDisabled: !isValid,
        title: 'Next'
      }}
      cancelButton={{
        onClick: () => wizardController.onCancel(),
        isDisabled: false,
        title: 'Cancel'
      }}
      backButton={{
        onClick: () => wizardController.goToPreviousStep(),
        isDisabled: false,
        title: 'Back'
      }}
      title={'Module'}
    >
      <Box sx={styles.moduleSelectWrapper}>
        {modulesLoading ? (
          <CircularProgress />
        ) : (
          <>
            {modules!.filter((m) => m.plant == null).length == 0 ? (
              <>
                <CustomAlert
                  type={'error' as AlertColor}
                  message={
                    "You don't have any module without Plant assinged to it. To create new Plant,\n" +
                    '                  please add new module.'
                  }
                />
              </>
            ) : (
              <>
                <InputLabel id="SelectPlantModule">
                  Choose a module that will monitor your plant moisture
                </InputLabel>
                <Controller
                  control={control}
                  name="module"
                  render={({ field: { onChange, value }, formState: { errors } }) => (
                    <Select
                      sx={styles.typeSelect}
                      onChange={onChange}
                      value={value}
                      id="plantModule"
                      labelId="SelectPlantPlace"
                    >
                      {modules!
                        .filter((m) => m.plant == null)
                        .map((m) => (
                          <MenuItem value={m.id}>
                            {m.name} - {m.id}
                          </MenuItem>
                        ))}
                    </Select>
                  )}
                />
              </>
            )}
          </>
        )}
      </Box>
    </WizardStep>
  );
};

export default Module;
