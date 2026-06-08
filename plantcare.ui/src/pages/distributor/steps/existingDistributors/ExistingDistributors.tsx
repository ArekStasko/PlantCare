import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { AddDistributorContext } from '../../interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import { Box, CircularProgress, Typography } from "@mui/material";
import { useNavigate } from 'react-router';
import RoutingPaths from '../../../../app/routing/routingConstants';
import { useGetDistributorsQuery } from '../../../../common/RTK/Distributor/Distributor';
import { useMemo } from 'react';
import { SelectDistributor } from './components/SelectDistributor';

const ExistingDistributors = ({ wizardController }: WizardStepProps<AddDistributorContext>) => {
  const navigate = useNavigate();
  const { data: distributors, isFetching: isDistributorsLoading } = useGetDistributorsQuery();

  const areThereAnyDistributors = useMemo(() => distributors.length > 0, [distributors]);

  const isBtnDisabled = useMemo(() => {
    return (
      isDistributorsLoading ||
      (distributors.length !== 0 && wizardController.context.distributorId === undefined)
    );
  }, [distributors, isDistributorsLoading]);

  const onDistributorSelect = (id: number) => {
    wizardController.updateContext({
      ...wizardController.context,
      distributorId: id
    });
  };

  const activeComponent = useMemo(() => {
    if(areThereAnyDistributors) return (
      <SelectDistributor
        distributors={distributors}
        onDistributorSelect={onDistributorSelect}
        distributorId={wizardController.context.distributorId}
      />
    )

    return (
      <Typography>There are no existing distributors list</Typography>
    )
  }, [areThereAnyDistributors])

  return (
    <WizardStep
      nextButton={{
        onClick: () => console.log('ExistingDistributors next btn click'),
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
        {
          isDistributorsLoading ? (
            <CircularProgress />
          ) : activeComponent
        }
      </Box>
    </WizardStep>
  );
};

export default ExistingDistributors;
