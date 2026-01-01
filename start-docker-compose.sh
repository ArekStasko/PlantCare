#!/bin/bash

# Purpose of this script is to run the plantcare application and its resources in a docker environment,
# the script should be placed above the database, idp, and plantcare api folders

if [ -z $1 ]; then
    echo Please provide database directory name
    exit -1;
fi

if [ -z $2 ]; then
    echo Please provide Identity Provider directory name
    exit -1;
fi

if [ -z $3 ]; then
    echo Please provide Plantcare API directory name
    exit -1;
fi

elements=(*)
database_dir=$1
idp_dir=$2
plantcare_dir=$3

echo -e "\e[32mWelcome to Plantcare starter\e[0m"
echo -e "\e[33mDirectories in current location:\e[0m"
for element in ${elements[@]}; do
    if [ -d $element ]; then
        echo -e "\e[33m$element\e[0m"
    fi
done

echo
echo ---
echo Entering Database directory: $database_dir
cd $database_dir
sudo docker-compose up -d
cd ..
echo ---

echo
echo ---
echo Entering Identity Provider directory: $idp_dir
cd $idp_dir
echo -e "\e[33mRemoving IdP containers\e[0m"
sudo docker container stop identityprovidersystem-logger
sudo docker container rm identityprovidersystem-logger
sudo docker container stop identityprovidersystem-api
sudo docker container rm identityprovidersystem-api
echo Starting new IdP containers
sudo docker-compose up -d
cd ..
echo ---

echo
echo ---
echo Entering Plantcare API directory: $plantcare_dir
cd $plantcare_dir
echo -e "\e[33mRemoving Plantcare API containers\e[0m"
sudo docker-compose down
echo Starting new Plantcare API containers
sudo docker-compose up -d
echo -e "\e[33mWaiting for Plantcare API ...\e[0m"
sleep 15
sudo docker-compose restart plantcare-api
cd ..
echo ---

echo
echo -e "\e[32mPlantCare containers are ready to use\e[0m"
echo -e Plantcare API logger is available under: "\e[33mhttp://192.168.1.40:5341/\e[0m"
echo -e Identity Provider logger is available under: "\e[33mhttp://192.168.1.40:5342/\e[0m"

read -p "Do you want to run UI applications? (Y/N): " runUi
if [[ "$runUi" == "Y" ]]; then
    pkill -f "serve -s"
    read -p "Provide Plantcare UI directory name: " PlantcareUI
    cd $PlantcareUI
    nohup serve -s build -l 3001 > react.log 2>&1 &
    cd ..
    read -p "Provide Identity Provider UI directory name: " IdpUI
    cd $IdpUI
    nohup serve -s build -l 3000 > react.log 2>&1 &
    cd ..

    echo -e "\e[32mPlantCare UI applications are ready to use\e[0m"
    echo -e Plantcare APP is available under: "\e[33mhttp://192.168.1.40:3001/\e[0m"
    echo -e Identity Provider APP is available under: "\e[33mhttp://192.168.1.40:3000/\e[0m"
fi
