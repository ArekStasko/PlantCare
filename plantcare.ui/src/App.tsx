import React from 'react';
import './App.css';
import { createTheme, ThemeProvider } from '@mui/material';
import MainRouting from './app/routing/mainRouting';

const darkTheme = createTheme({
  palette: {
    mode: 'dark'
  }
});

function App() {
  return (
      <ThemeProvider theme={darkTheme}>
        <div className="App">
          <MainRouting />
        </div>
      </ThemeProvider>
  );
}

export default App;
