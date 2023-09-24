import http from 'k6/http'
import {sleep} from 'k6'

export let options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    scenarios: {
        heavy_load: {
            executor: 'constant-arrival-rate',
            preAllocatedVUs: 60,
            duration: '30s',
            rate: 60,
            timeUnit: '1s'
        },
        moderate_load: {
            executor: 'constant-arrival-rate',
            preAllocatedVUs: 30,
            duration: '30s',
            rate: 30,
            timeUnit: '1s'
        },
        low_load: {
            executor: 'constant-arrival-rate',
            preAllocatedVUs: 15,
            duration: '30s',
            rate: 15,
            timeUnit: '1s'
        }
    }
};

export default () => {
    const createPlantPayload = JSON.stringify({
        name: "Test Name",
        description: "Test Description",
        type: 0,
        criticalMoistureLevel: 70,
        requiredMoistureLevel: 50,
        moduleId: "48205409-27ad-43fc-aed4-100357951924"
    })

    http.post('http://localhost:8080/api/plants/Create', createPlantPayload)
    sleep(1);

    http.get('http://localhost:8080/api/plants/GetAll')
    sleep(1);

}