const dashboardContainer = {
  width: '80%',
  display: 'flex',
  flexDirection: 'column',
  justifyContent: 'center',
  alignItems: 'center',
  border: '1px solid blue',
  p: 4,
  backgroundColor: 'rgba(0, 0, 0, 0.5)'
};

const dashboardWrapper = {
  display: 'flex',
  justifyContent: 'center',
  flexDirection: 'column',
  alignItems: 'center'
};

const alertWrapper = {
  mt: 15,
  display: 'flex',
  justifyContent: 'center',
  alignItems: 'center'
};

const placesAccordionWrapper = {
  width: '80%'
};

const placesAccordionSummary = {
  width: '100%',
  display: 'flex',
  justifyContent: 'space-between',
  alignItems: 'center'
};

const plantsAccordionDetailsWrapper = {
  display: 'flex',
  justifyContent: 'space-between',
  alignItems: 'center',
  borderTop: 1,
  borderColor: '#4C4E52'
};

const plantsAccordionDetailsInfo = {
  display: 'flex',
  justifyContent: 'space-around',
  alignItems: 'center'
};

const plantsAccordionDetailsButtons = {
  width: '50%',
  display: 'flex',
  justifyContent: 'flex-end',
  alignItems: 'center'
};

export default {
  dashboardContainer,
  dashboardWrapper,
  alertWrapper,
  placesAccordionWrapper,
  placesAccordionSummary,
  plantsAccordionDetailsWrapper,
  plantsAccordionDetailsInfo,
  plantsAccordionDetailsButtons
};
