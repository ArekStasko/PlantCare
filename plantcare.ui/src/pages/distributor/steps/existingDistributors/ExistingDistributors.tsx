import { WizardStepProps } from '../../../../common/wizard/interfaces';
import { AddDistributorContext } from '../../interfaces';
import { WizardStep } from '../../../../common/wizard/components/wizardStep/WizardStep';
import { Typography } from '@mui/material';
import { useNavigate } from 'react-router';
import RoutingPaths from '../../../../app/routing/routingConstants';

const ExistingDistributors = ({ wizardController }: WizardStepProps<AddDistributorContext>) => {
  const navigate = useNavigate();

  return (
    <WizardStep
      nextButton={{
        onClick: () => console.log('ExistingDistributors next btn click'),
        isDisabled: false,
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
      <Typography>Existing distributors list</Typography>
    </WizardStep>
  );
};

export default ExistingDistributors;
