import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { AddDistributorContext } from '../../interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import { Box, CircularProgress, Typography } from '@mui/material';
import { useNavigate } from 'react-router';
import RoutingPaths from '../../../../app/routing/routingConstants';
import { useGetDistributorsQuery } from '../../../../common/RTK/Distributor/Distributor';
import { useEffect, useMemo } from 'react';
import { SelectDistributor } from './components/SelectDistributor';

const ExistingDistributors = ({ wizardController }: WizardStepProps<AddDistributorContext>) => {
  const navigate = useNavigate();
  const { data: distributors, isFetching: areDistributorsLoading } = useGetDistributorsQuery();

  useEffect(() => {
    wizardController.onLoading(areDistributorsLoading);
    if (!distributors) return;
    if (distributors.length === 0) {
      wizardController.onVisibleStepsChange([0, 1, 2, 3, 4, 5]);
    }
  }, [areDistributorsLoading]);

  const areThereAnyDistributors = useMemo(() => distributors !== undefined && distributors.length > 0, [distributors]);

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
    >
      <Box>
        {areThereAnyDistributors ? (
          <SelectDistributor
            distributors={distributors!}
            onDistributorSelect={onDistributorSelect}
          />
        ) : (
          <Typography>There are no existing distributors</Typography>
        )}
      </Box>
    </WizardStep>
  );
};

export default ExistingDistributors;
