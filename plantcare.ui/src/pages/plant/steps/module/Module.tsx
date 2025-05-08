import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import {
  AlertColor,
  Box,
  Card,
  CardContent,
  CircularProgress,
  InputLabel,
  MenuItem,
  Select,
  Typography
} from '@mui/material';
import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { PlantContext, PlantFlowType } from '../../interfaces';
import CustomAlert from '../../../../common/components/customAlert/customAlert';
import { Controller, useForm } from 'react-hook-form';
import React, { useMemo } from 'react';
import { useGetModulesQuery } from '../../../../common/RTK/getModules/getModules';
import styles from './module.styles';
import { yupResolver } from '@hookform/resolvers/yup';
import validators from '../../../../common/services/Validators';

const Module = ({ wizardController }: WizardStepProps<PlantContext>) => {
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

  const CurrentModule = useMemo(() => {
    if (wizardController.context.flowType === PlantFlowType.CREATE || !modules) return undefined;
    const module = modules.find((m) => m.id === wizardController.context.module);
    if (!module) return undefined;
    return `${module.name} - ${module.id}`;
  }, [modules]);

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
                    "You don't have any module without plant assinged to it. To create new plant,\n" +
                    '                  please add new module.'
                  }
                />
              </>
            ) : (
              <>
                {CurrentModule && (
                  <Card>
                    <CardContent>
                      <Typography gutterBottom variant="h5">
                        Currently selected Module:
                      </Typography>
                      <Typography variant="body1">{CurrentModule}</Typography>
                    </CardContent>
                  </Card>
                )}
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
