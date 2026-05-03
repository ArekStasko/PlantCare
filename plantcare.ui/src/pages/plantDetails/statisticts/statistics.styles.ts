const statisticsContainer = {
  pt: 2,
  width: '100%',
  height: '85vh',
  display: 'flex',
  justifyContent: 'space-evenly'
};

const statisticsWrapper = {
  width: '50%',
  height: '100%',
  display: 'flex',
  justifyContent: 'space-around',
  flexDirection: 'column',
  alignItems: 'start',
  border: '1px solid #203b78',
  backgroundColor: 'rgba(0, 0, 0, 0.7)'
};

const measurementsBar = {
  width: '100%',
  height: '20%',
  display: 'flex',
  justifyContent: 'space-around',
  alignItems: 'center'
};

const measurementsBarActions = {
  width: '50%',
  height: '10%',
  display: 'flex',
  justifyContent: 'flex-end',
  alignItems: 'center'
};

const statisticsChartWrapper = {
  height: '100%',
  width: '100%',
  display: 'flex',
  justifyContent: 'center',
  alignItems: 'center'
};

const plantDetailsWrapper = {
  width: '45%',
  height: '100%',
  display: 'flex',
  justifyContent: 'space-between',
  flexDirection: 'column',
  alignItems: 'center',
  border: '1px solid #203b78',
  backgroundColor: 'rgba(0, 0, 0, 0.7)'
};

const loader = {
  width: '100%',
  height: '100%',
  display: 'flex',
  justifyContent: 'center',
  alignItems: 'center'
};

const telemetryTypeWrapper = {
  display: 'flex',
  justifyContent: 'space-between',
  width: '90%',
  m: 4
};

const telemetryTypeForm = {
  width: '50%'
};

const measurementsBody = {
  width: '100%',
  height: '80%',
  display: 'flex',
  justifyContent: 'center',
  alignItems: 'center'
};

export default {
  statisticsWrapper,
  statisticsContainer,
  plantDetailsWrapper,
  measurementsBar,
  measurementsBarActions,
  statisticsChartWrapper,
  loader,
  telemetryTypeWrapper,
  telemetryTypeForm,
  measurementsBody
};
