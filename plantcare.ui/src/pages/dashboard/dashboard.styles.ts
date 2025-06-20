const dashboardContainer = {
  width: '80%',
  height: '80%',
  border: '1px solid #203b78',
  borderRadius: 4,
  p: 4,
  backgroundColor: 'rgba(0, 0, 0, 0.4)'
};

const dashboardWrapper = {
  width: '100%',
  height: '100%',
  display: 'flex',
  flexDirection: 'column',
  justifyContent: 'space-between',
  alignItems: 'center'
};

const accordionWrapper = {
  width: '100%',
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
  accordionWrapper,
  alertWrapper,
  placesAccordionWrapper,
  placesAccordionSummary,
  plantsAccordionDetailsWrapper,
  plantsAccordionDetailsInfo,
  plantsAccordionDetailsButtons
};
