const summaryList = {
  display: 'flex',
  flexDirection: 'column',
  alignItems: 'center',
  pt: 1,
  pb: 1,
  width: '100%',
  height: '70%'
};

const summaryListElement = {
  display: 'flex',
  flexDirection: 'column',
  alignItems: 'center',
  justifyContent: 'flex-start',
  pt: 1,
  width: '100%',
  height: '20%'
};

const summaryListText = {
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'space-between',
  width: '75%'
};

const summaryListTitle = {
  fontSize: '16px',
  fontWeight: 'bold'
};

const summaryListPassword = (showPassword: boolean) => ({
  width: '40%',
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'space-between',
  fontWeight: 'bold',
  WebkitTextSecurity: showPassword ? 'none' : 'disc',
  textSecurity: showPassword ? 'none' : 'disc',
  m: 1
});

export default {
  summaryList,
  summaryListElement,
  summaryListText,
  summaryListTitle,
  summaryListPassword
};
