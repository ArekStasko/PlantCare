import { WizardStep } from "../../../../common/wizard/components/wizardStep/WizardStep";
import { Box, InputLabel, MenuItem, Select, TextField, Typography } from "@mui/material";
import { WizardStepProps } from "../../../../common/wizard/interfaces";
import { PlantContext, PlantFlowType } from "../../interfaces";
import { Controller, useForm } from "react-hook-form";
import { PlantType } from "../../../../common/models/plantTypes";
import Vegetable from "../../../../app/images/Vegetable.png";
import Fruit from "../../../../app/images/Fruit.png";
import Decorative from "../../../../app/images/Decorative.png";
import React, { useState } from "react";
import styles from "./details.styles";
import { yupResolver } from "@hookform/resolvers/yup";
import validators from "../../../../common/services/Validators";
import Popup from "../../../../common/components/popup/Popup";

const Details = ({ wizardController }: WizardStepProps<PlantContext>) => {
  const [openPopup, setOpenPopup] = useState(false);
  const methods = useForm({
    mode: 'onChange',
    resolver: yupResolver(validators.createPlantDetailsSchema),
    defaultValues: {
      name: wizardController.context.name ?? '',
      description: wizardController.context.description ?? '',
      plantType: wizardController.context.type
    }
  });

  const {
    register,
    getValues,
    formState: { errors, isValid },
    control
  } = methods;

  const checkIfValuesAreTheSame = (): boolean => {
    const newName = getValues('name');
    const newDescription = getValues('description');
    const newType = getValues('plantType') as PlantType;

    const {type, name, description} = wizardController.context
    return type === newType && newDescription === description && newName === name;
  }

  const onNext = () => {

    if(wizardController.context.flowType === PlantFlowType.UPDATE){
      const areValuesTheSame = checkIfValuesAreTheSame();
      if(areValuesTheSame){
        return;
      }
    }

    wizardController.updateContext({
      ...wizardController.context,
      name: getValues('name'),
      description: getValues('description'),
      type: getValues('plantType') as PlantType
    });
    wizardController.goToNextStep();
  }

  return (
    <WizardStep
      nextButton={{
        onClick: () => onNext(),
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
        isDisabled: true,
        title: 'Back'
      }}
      title={'Details'}
    >
      <Popup openPopup={openPopup} confirmAction={() => openPopup(false)}>
      <Box sx={styles.nameNtypeWrapper}>
        <Box sx={styles.inputWrapper}>
          <InputLabel id="SelectPlace">Provide your Plant name</InputLabel>
          <TextField
            sx={styles.nameInput}
            label="Name"
            id="name"
            error={!!errors.name}
            helperText={errors?.name?.message?.toString()}
            variant="filled"
            {...register('name')}
          />
        </Box>
        <Box sx={styles.inputWrapper}>
          <InputLabel id="SelectType">Select Type</InputLabel>
          <Controller
            control={control}
            name="plantType"
            render={({ field: { onChange, value }, formState: { errors } }) => (
              <Select
                sx={styles.typeSelect}
                onChange={onChange}
                value={value}
                id="plantType"
                error={!!errors.plantType}
                labelId="SelectType"
              >
                <MenuItem value={PlantType.Vegetable}>
                  <Box sx={styles.option}>
                    <Typography>Vegetable</Typography>
                    <Box
                      component="img"
                      sx={{
                        height: 30,
                        width: 30,
                        maxHeight: { xs: 30, md: 30 },
                        maxWidth: { xs: 30, md: 30 },
                        borderRadius: 2
                      }}
                      alt="Plant_Type"
                      src={Vegetable}
                    />
                  </Box>
                </MenuItem>
                <MenuItem value={PlantType.Fruit}>
                  <Box sx={styles.option}>
                    <Typography>Fruit</Typography>
                    <Box
                      component="img"
                      sx={{
                        height: 30,
                        width: 30,
                        maxHeight: { xs: 30, md: 30 },
                        maxWidth: { xs: 30, md: 30 },
                        borderRadius: 2
                      }}
                      alt="Plant_Type"
                      src={Fruit}
                    />
                  </Box>
                </MenuItem>
                <MenuItem value={PlantType.Decorative}>
                  <Box sx={styles.option}>
                    <Typography>Decorative</Typography>
                    <Box
                      component="img"
                      sx={{
                        height: 30,
                        width: 30,
                        maxHeight: { xs: 30, md: 30 },
                        maxWidth: { xs: 30, md: 30 },
                        borderRadius: 2
                      }}
                      alt="Plant_Type"
                      src={Decorative}
                    />
                  </Box>
                </MenuItem>
              </Select>
            )}
          />
        </Box>
      </Box>
      <Box sx={styles.descriptionWrapper}>
        <InputLabel id="descriptionLabel">Describe your plant</InputLabel>
        <TextField
          sx={styles.descriptionInput}
          error={!!errors.description}
          helperText={errors?.description?.message?.toString()}
          id="description"
          multiline
          rows={8}
          {...register('description')}
        />
      </Box>
    </WizardStep>
  );
};

export default Details;
