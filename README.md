kobieta# PlantCare
Web App that will allows me to control ESP32 modules to irrigate my plants and check their soil moisture levels

This Web App have to connect to my ESP32 modules via API. What is more me as a user should be able to see moisture levels of my plants in a user interface,
i need to have an option of changing irrigating level via my web app for different plants. 

What is more i want to see the statistics of moisture in different levels, for example in minutes, hours, days and weeks.

How should it work with ESP32 ?
- ESP32 will call our Web App API with moisture parameters and it will receive current options like irrigating level etc.
- Our API will save data to database based on data from ESP32.

TODO: 
[ This list will be continuously modified due to changes in app ]

Backend API :
- [x] Create backend Structure ( API, DataAccess, Services )
- [x] Add docker support via docker-compose and configure docker services (API and mssql database)
- [x] Add Plant mode
- [x] Add error handling 
- [x] Add MediatR configuration
- [x] Add Plant repository
- [x] Add create, get, delete, edit methods to Plant repository
- [x] Add Plant Service
- [x] Add service methods :
  - [x] Create
  - [x] Get
  - [x] Get via Id
  - [x] Delete
  - [x] Edit
- [x] Add Plant API Controllers :
  - [x] Create
  - [x] Get
  - [x] Get via Id
  - [x] Delete
  - [x] Edit
- [ ] Add Performance Tests
- [ ] Add Unit tests to :
  - [ ] Create command
  - [ ] Edit command
  - [ ] Delete command
  - [ ] Get query
  - [ ] GetAll query
- [x] Add Postman collection

Fronted App :
- [ ] Create frontend App Structure ( ReactApp + Redux + TS )
- [ ] Add Dashboard layout
- [ ] Perform plants fetching for dashboard ( via slices in react redux )
- [ ] Add delete popup for deletion flow
- [ ] Add delete action via redux slices and redirect user to portfolio
- [ ] Make details page with plant description, diagram of moisture etc, there should also be option to edit plant data
- [ ] Perfom plant details fetching via plant Id
- [ ] Add plant creation page with wizard view : 
  - [ ] Add plant name, description etc
  - [ ] Informations about plant (what minerals it needs etc)
  - [ ] Moisture level
- [ ] Add plant create form validation and redirection to dashboard after successfull validation

ESP32 Upgrades :
  Backend API :
  - [ ] create esp module controllers file structure
  - [ ] Add moisture level edit to esp module controllers
  - [ ] Add moisture level edit service and repository for plants
  - [ ] Add moisutre level save controller to save data from esp module ( it should collect data like moisture level, time of rehydration )
  - [ ] Perform sending back to esp module info about required moisture level

  Fronted App :
  - [ ] On details page add statistics about the plant ( mouisture level in different timesheets, times of rehydrations )
  - [ ] Add esp module configuration stage to plant creation form


UI Flow Diagram : 
![UIFlow_Diagram](https://github.com/ArekStasko/PlantCare/blob/master/PlantCareDiagram.png?raw=true)


Also here is Figma with App design : 
[PlantCare Figma](https://www.figma.com/file/1ysZcEEvvSVtgEayMXtAmA/PlantCare?type=design&node-id=0%3A1&mode=design&t=wdd6EHnrYPlem866-1)
