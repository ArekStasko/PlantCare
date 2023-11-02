import * as yup from 'yup';

const createPlantSchema = yup.object().shape({
  name: yup.string().required(),
  description: yup.string().required(),
  plantType: yup.string().required()
});

export default {
  createPlantSchema
};
