import * as yup from 'yup';

const createPlantSchema = yup.object().shape({
  name: yup.string().required(),
  description: yup.string().required(),
  plantType: yup.string().required(),
  plantPlace: yup.string().required(),
  plantModule: yup.string().required()
});

const updatePlantSchema = yup.object().shape({
  id: yup.string().required(),
  name: yup.string().required(),
  description: yup.string().required(),
  plantType: yup.string().required(),
  plantPlace: yup.string().required()
});

const createPlaceSchema = yup.object().shape({
  name: yup.string().required()
});

const updatePlaceSchema = yup.object().shape({
  id: yup.string().required(),
  name: yup.string().required(),
  flow: yup.string().required().notOneOf(['delete'])
});

export default {
  createPlantSchema,
  createPlaceSchema,
  updatePlantSchema,
  updatePlaceSchema
};
