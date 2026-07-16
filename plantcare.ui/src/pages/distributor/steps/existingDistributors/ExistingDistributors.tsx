import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { AddDistributorContext } from '../../interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import { Alert, Box, Typography } from '@mui/material';
import { useNavigate } from 'react-router';
import RoutingPaths from '../../../../app/routing/routingConstants';
import { useGetDistributorsQuery } from '../../../../common/RTK/Distributor/Distributor';
import React, { useEffect, useMemo } from 'react';
import { SelectDistributor } from './components/SelectDistributor';
import styles from './existingDistributors.styles';
import CustomAlert from "../../../../common/components/customAlert/customAlert";

const ExistingDistributors = ({ wizardController }: WizardStepProps<AddDistributorContext>) => {
  const navigate = useNavigate();
  const { data: distributors, isFetching: areDistributorsLoading, isError } = useGetDistributorsQuery();

  useEffect(() => {
    wizardController.onLoading(areDistributorsLoading);
    if (!distributors) return;
    if (distributors.length === 0) {
      wizardController.onVisibleStepsChange([0, 1, 2, 3, 4, 5]);
    }
  }, [areDistributorsLoading]);

  const areThereAnyDistributors = useMemo(
    () => distributors !== undefined && distributors.length > 0,
    [distributors]
  );

  const isBtnDisabled = useMemo(() => {
    return (
      areDistributorsLoading ||
      (areThereAnyDistributors && wizardController.context.distributorId === undefined)
    );
  }, [areThereAnyDistributors, areDistributorsLoading]);

  const onDistributorSelect = (id: number) => {
    wizardController.updateContext({
      ...wizardController.context,
      distributorId: id
    });
  };

  const onNext = () => {
    if (areThereAnyDistributors) {
      wizardController.goToStep(5);
      return;
    }

    wizardController.goToNextStep();
  };

  return (
    <WizardStep
      nextButton={{
        onClick: () => onNext(),
        isDisabled: isBtnDisabled,
        title: 'Next'
      }}
      cancelButton={{
        onClick: () => wizardController.onCancel(),
        isDisabled: false,
        title: 'Cancel'
      }}
      backButton={{
        onClick: () =>
          navigate(
            `${RoutingPaths.plantDetails}/${wizardController.context.plantId}/${wizardController.context.moduleId}`
          ),
        isDisabled: false,
        title: 'Back'
      }}
      title={'Distributors'}
      errorAlert={isError ? (
        <CustomAlert
          message={"Something went wrong while fetching distributors, please try again later"}
          type={'error'}
        />
      ) : undefined}
    >
      <Box sx={styles.container}>
        {areThereAnyDistributors ? (
          <SelectDistributor
            distributors={distributors!}
            onDistributorSelect={onDistributorSelect}
          />
        ) : (
          <Box sx={styles.noDistributorsWrapper}>
            <Typography>There are no existing distributors</Typography>
            <Alert severity="info">
              By continuing you will be able to add a new distributor by making a Bluetooth
              connection with it and providing the required information
            </Alert>
          </Box>
        )}
      </Box>
    </WizardStep>
  );
};

export default ExistingDistributors;
