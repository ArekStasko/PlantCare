import { Box } from "@mui/material";
import Wizard from "../../common/wizard/Wizard";
import { CreatePlaceContext } from "./interfaces";


const CreatePlace = () => {
  const initialContext: CreatePlaceContext = {};

  return (
    <Wizard initialContext={initialContext} steps={[]} />
  )
}

export default CreatePlace