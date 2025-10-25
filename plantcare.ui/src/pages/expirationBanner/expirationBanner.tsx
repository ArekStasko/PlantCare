import { Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle } from "@mui/material";
import { ExpirationBannerInterface } from "identity-provider-client";

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
      </DialogContent>
      <DialogActions>
        <Button onClick={onRefresh}>Refresh</Button>
        <Button onClick={onLogout} autoFocus>
          Logout
        </Button>
      </DialogActions>
    </Dialog>
  );
}

export default ExpirationBanner;