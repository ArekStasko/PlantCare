import * as yup from 'yup';

const createPlantSchema = yup.object().shape({
  name: yup.string().required(),
  description: yup.string().required(),
  plantType: yup.string().required(),
  plantPlace: yup.string().required(),
  plantModule: yup.string().required()
});

const createPlantDetailsSchema = yup.object().shape({
  name: yup.string().required(),
  description: yup.string().required(),
  plantType: yup.number().required()
});

const selectPlaceSchema = yup.object().shape({
  place: yup.string().required()
});

const selectModuleSchema = yup.object().shape({
  module: yup.string().required()
});

const updatePlantSchema = yup.object().shape({
  id: yup.string().required(),
  name: yup.string().required(),
  description: yup.string().required(),
  plantType: yup.string().required(),
  plantPlace: yup.string().required(),
  flow: yup.string().required()
});

const createPlaceSchema = yup.object().shape({
  name: yup.string().required()
});

const updatePlaceSchema = yup.object().shape({
  id: yup.string().required(),
  name: yup.string().required(),
  flow: yup.string().required()
});

const addModuleSchema = yup.object().shape({
  wifiName: yup.string().required(),
  wifiPassword: yup.string().required()
});

const addModuleNameSchema = yup.object().shape({
  moduleName: yup.string().required()
});

export default {
  createPlantSchema,
  createPlaceSchema,
  updatePlantSchema,
  updatePlaceSchema,
  createPlantDetailsSchema,
  addModuleSchema,
  selectPlaceSchema,
  selectModuleSchema,
  addModuleNameSchema
};
