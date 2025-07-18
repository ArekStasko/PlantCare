import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import { Box, Card, Divider, Typography } from '@mui/material';
import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { PlantContext, PlantFlowType } from '../../interfaces';
import styles from './summary.styles';
import { PlantType } from '../../../../common/models/plantTypes';
import Decorative from '../../../../app/images/Decorative.png';
import Vegetable from '../../../../app/images/Vegetable.png';
import Fruit from '../../../../app/images/Fruit.png';
import React, { useEffect, useState } from 'react';
import { useCreatePlantMutation } from '../../../../common/RTK/createPlant/createPlant';
import { CreatePlantRequest } from '../../../../common/RTK/createPlant/createPlantRequest';
import Popup, { PopupStatus } from '../../../../common/components/popup/Popup';
import RoutingConstants from '../../../../app/routing/routingConstants';
import { useNavigate } from 'react-router';
import { useUpdatePlantMutation } from '../../../../common/RTK/updatePlant/updatePlant';
import { UpdatePlantRequest } from '../../../../common/RTK/updatePlant/updatePlantRequest';
import { useGetPlantQuery } from '../../../../common/RTK/getPlant/getPlant';
import { GetPlantData } from '../../../../common/RTK/getPlant/getPlantData';
import CustomAlert from '../../../../common/components/customAlert/customAlert';

const Summary = ({ wizardController }: WizardStepProps<PlantContext>) => {
  const { data: plant, isLoading: isGetPlantLoading } = useGetPlantQuery(
    { plantId: wizardController.context.plantId?.toString() } as GetPlantData,
    { skip: !wizardController.context.plantId }
  );
  const [isDataNoChanged, setIsDataNoChanged] = useState<boolean>(false);
  const [createPlant, { data, isLoading }] = useCreatePlantMutation();
  const [updatePlant, { data: updatePlantResult, isLoading: updatePlantLoading }] =
    useUpdatePlantMutation();
  const navigate = useNavigate();
  const { name, description, type, place, module, plantId } = wizardController.context;

  useEffect(() => {
    wizardController.onLoading(isLoading || updatePlantLoading || isGetPlantLoading);
  }, [isLoading, updatePlantLoading, isGetPlantLoading]);

  useEffect(() => {
    if (!plant) return;
    const { name, place, type, description, module } = wizardController.context;
    const check =
      name === plant.name &&
      place === plant.placeId.toString() &&
      type === plant.type &&
      description === plant.description &&
      module === plant.moduleId;
    setIsDataNoChanged(check);
  }, [plant, wizardController.context]);

  const onSubmit = async () => {
    if (wizardController.context.flowType === PlantFlowType.UPDATE) {
      const request = {
        name: name,
        description: description,
        type: type as PlantType,
        placeId: place,
        moduleId: module,
        id: plantId
      } as UpdatePlantRequest;
      await updatePlant(request);
      return;
    }

    const request = {
      name: name,
      description: description,
      type: type as PlantType,
      placeId: place,
      moduleId: module
    } as CreatePlantRequest;
    await createPlant(request);
  };

  const plantTypeToImage = (type?: PlantType) => {
    switch (type) {
      case PlantType.Decorative:
        return Decorative;
      case PlantType.Vegetable:
        return Vegetable;
      case PlantType.Fruit:
        return Fruit;
      default:
        return '';
    }
  };

  return (
    <WizardStep
      nextButton={{
        onClick: onSubmit,
        isDisabled: isLoading || updatePlantLoading || isGetPlantLoading || isDataNoChanged,
        title: 'Submit'
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
      title={'Summary'}
      popup={
        <Popup
          titleText={'Success'}
          contentText={
            data || updatePlantResult
              ? 'The new plant has been added successfully.'
              : 'An error occurred while adding a new plant, please try again later.'
          }
          openPopup={(data || updatePlantResult) ?? false}
          confirmText={'Go to dashboard'}
          confirmAction={() => navigate(RoutingConstants.root)}
          status={data || updatePlantResult ? PopupStatus.success : PopupStatus.failure}
        />
      }
    >
      {isDataNoChanged && (
        <CustomAlert
          type="warning"
          message="There is no changes in plant data that you have provided"
        />
      )}
      <Card elevation={5} sx={styles.summaryList}>
        <Box sx={styles.summaryListElement}>
          <Box sx={styles.summaryListText}>
            <Typography variant="button" sx={styles.summaryListTitle}>
              Name
            </Typography>
            <Typography>{wizardController.context.name}</Typography>
          </Box>
          <Divider sx={styles.divider} />
        </Box>
        <Box sx={styles.summaryListElement}>
          <Box sx={styles.summaryListText}>
            <Typography variant="button" sx={styles.summaryListTitle}>
              Type
            </Typography>
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
              src={plantTypeToImage(+(wizardController.context.type ?? 0) as PlantType)}
            />
          </Box>
          <Divider sx={styles.divider} />
        </Box>
        <Box sx={styles.summaryListElement}>
          <Box sx={styles.summaryListText}>
            <Typography variant="button" sx={styles.summaryListTitle}>
              Description
            </Typography>
            <Typography align="justify" sx={{ maxWidth: '60%' }}>
              {wizardController.context.description}
            </Typography>
          </Box>
          <Divider sx={styles.divider} />
        </Box>
        <Box sx={styles.summaryListElement}>
          <Box sx={styles.summaryListText}>
            <Typography variant="button" sx={styles.summaryListTitle}>
              Place
            </Typography>
            <Typography>{wizardController.context.placeName}</Typography>
          </Box>
          <Divider sx={styles.divider} />
        </Box>
        <Box sx={styles.summaryListElement}>
          <Box sx={styles.summaryListText}>
            <Typography variant="button" sx={styles.summaryListTitle}>
              Module
            </Typography>
            <Typography>
              {wizardController.context.moduleName} - {wizardController.context.module}
            </Typography>
          </Box>
          <Divider sx={styles.divider} />
        </Box>
      </Card>
    </WizardStep>
  );
};

export default Summary;
