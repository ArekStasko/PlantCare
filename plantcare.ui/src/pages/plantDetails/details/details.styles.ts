const plantTitleWrapper = {
  width: '100%',
  height: '20%',
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'space-around'
};

const plantDescriptionWrapper = {
  width: '100%',
  height: '60%',
  display: 'flex',
  flexDirection: 'column',
  alignItems: 'center',
  justifyContent: 'space-around'
};

const titleCard = {
  minHeight: '15%',
  width: '90%',
  p: 1,
  display: 'flex',
  justifyContent: 'start',
  alignItems: 'center'
};

const descriptionCard = {
  minHeight: '50%',
  width: '90%',
  p: 1,
  display: 'flex',
  justifyContent: 'start',
  alignItems: 'start',
  textAlign: 'start'
};

const details_paper = {
  width: '100%',
  height: '10%',
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'space-around'
};

const details_card = {
  width: '90%',
  p: 1,
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'space-around'
};

const typeCard = {
  width: '40%',
  height: '50%',
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'space-evenly'
};

const humidityRangeWrapper = {
  width: '50%',
  display: 'flex',
  flexDirection: 'column',
  mt: '50px'
};

const waterSupplyProgress = {
  width: '100%',
  height: '100%',
  display: 'flex',
  flexDirection: 'column',
  justifyContent: 'space-between',
  alignItems: 'center',
}

const progress = {
  mt: 1,
  mb: 1,
  width: '80%',
  height: '100%',
}

export default {
  plantTitleWrapper,
  plantDescriptionWrapper,
  titleCard,
  descriptionCard,
  details_paper,
  details_card,
  typeCard,
  humidityRangeWrapper,
  waterSupplyProgress,
  progress
};
