import { Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle } from "@mui/material";
import { ExpirationBannerInterface } from "identity-provider-client";
import {CountdownTimer} from "./CountdownTimer";

const ExpirationBanner: React.FC<ExpirationBannerInterface> = ({open, onClose, onRefresh, onLogout}) => {
  return(
    <Dialog
      open={open}
      onClose={onClose}
    >
      <DialogTitle>
        {"Login session has expired."}
      </DialogTitle>
      <DialogContent>
        <DialogContentText id="alert-dialog-description">
          Click logout button to go to login page.
          If you want to stay login click refresh button.
        </DialogContentText>
        <CountdownTimer timeToCountDown={300} onTimeOut={() => onLogout()} />
      </DialogContent>
      <DialogActions>
        <Button variant="outlined" onClick={onLogout} autoFocus>
          Logout
        </Button>
        <Button variant='contained' onClick={onRefresh}>Stay</Button>
      </DialogActions>
    </Dialog>
  );
}

export default ExpirationBanner;