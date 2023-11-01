import React from "react";
import {Box, InputLabel, MenuItem, Select, SelectChangeEvent, TextField} from "@mui/material";
import {ICreatePlantState} from "../../interfaces";
import styles from './plantDetails.styles'
import {PlantType} from "../../../../common/models/plantTypes";
import {FormProvider, useForm } from "react-hook-form";
import {yupResolver} from "@hookform/resolvers/yup";
import validators from "../../../../common/services/Validators";

export const PlantDetails = ({state}: ICreatePlantState) => {

    const methods = useForm({
        mode: "onChange",
        resolver: yupResolver(validators.createPlantSchema)
    })

    const {
        trigger,
        setValue,
        formState: {errors},
    } = methods;

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>, id: "name" | "description") => {
        const {
            target: { value },
        } = e;
        console.log(id)
        console.log(value)
        setValue(id, value);
        trigger(id);
    }

    return(
        <Box sx={styles.plantDetailsWrapper}>
            <FormProvider {...methods}>
                <InputLabel id="SelectPlace">Provide your Plant name</InputLabel>
            <TextField
                label="Name"
                id="name"
                error={!!errors.name}
                variant="filled"
                onChange={e => handleChange(e, "name")}
            />
            <InputLabel id="SelectPlace">Describe your plant</InputLabel>
            <TextField
                error={!!errors.description}
                onChange={e => handleChange(e, "description")}
                label="Description"
                id="description"
                multiline
            />
            <InputLabel id="SelectPlace">Select Place</InputLabel>
            <Select
                id="plantType"
                labelId="SelectPlace"
            >
                <MenuItem value={PlantType.Vegetable}>Vegetable</MenuItem>
                <MenuItem value={PlantType.Fruit}>Fruit</MenuItem>
                <MenuItem value={PlantType.Decorative}>Decorative</MenuItem>
            </Select>
            </FormProvider>
        </Box>
    )
}

export default PlantDetails;