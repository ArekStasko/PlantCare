# PlantCare
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
- Create backend Structure ( API, DataAccess, Services )
- Add docker support via docker-compose and configure docker services (API and mssql database)
- Add Plant model
- Add Plant repository
- Add create, get, delete, edit methods to Plant repository
- Add Plant Service
- Add service methods :
  - Create
  - Get
  - Get via Id
  - Delete
  - Edit
- Add Plant API Controllers :
  - Create
  - Get
  - Get via Id
  - Delete
  - Edit

Fronted App :
- Create frontend App Structure ( ReactApp + Redux + TS )
- Add Dashboard layout
- Perform plants fetching for dashboard ( via slices in react redux )
- Add delete popup for deletion flow
- Add delete action via redux slices and redirect user to portfolio
- Make details page with plant description, diagram of moisture etc, there should also be option to edit plant data
- Perfom plant details fetching via plant Id
- Add plant creation page with wizard view : 
  - Add plant name, description etc
  - Informations about plant (what minerals it needs etc)
  - Moisture level
- Add plant create form validation and redirection to dashboard after successfull validation

ESP32 Upgrades :
  Backend API :
  - create esp module controllers file structure
  - Add moisture level edit to esp module controllers
  - Add moisture level edit service and repository for plants
  - Add moisutre level save controller to save data from esp module ( it should collect data like moisture level, time of rehydration )
  - Perform sending back to esp module info about required moisture level
  Fronted App :
  - On details page add statistics about the plant ( mouisture level in different timesheets, times of rehydrations )
  - Add esp module configuration stage to plant creation form
