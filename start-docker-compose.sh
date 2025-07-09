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

echo Welcome to Plantcare starter
echo Directories in current location:

for element in ${elements[@]}; do
    if [ -d $element ]; then
        echo $element
    fi
done

echo Starting script
echo Entering Database directory: $database_dir
cd $database_dir
sudo docker-compose up -d
database-services=$(sudo docker-compose ps --services)
echo Waiting for healthy Database services
for service in ${database-services[@]}; do
    status=$(sudo docker inspect --format='{{.State.Health.Status}}' "$container_id" 2>/dev/null || echo "no-healthcheck")
    if [ "$status" == "healthy" ] || [ "$status" == "no-healthcheck" ]; then
        echo "$service is ready (status: $status)"
        break
    else
        sleep 1
    fi
done
echo Database services are healthy
cd ..

echo Starting script
echo Entering Identity Provider directory: $idp_dir
cd $idp_dir
sudo docker-compose up -d
idp-services=$(sudo docker-compose ps --services)
echo Waiting for healthy Identity Provider services
for service in ${idp-services[@]}; do
    status=$(sudo docker inspect --format='{{.State.Health.Status}}' "$container_id" 2>/dev/null || echo "no-healthcheck")
    if [ "$status" == "healthy" ] || [ "$status" == "no-healthcheck" ]; then
        echo "$service is ready (status: $status)"
        break
    else
        sleep 1
    fi
done
echo Identity Provider services are healthy
cd ..

echo Starting script
echo Entering Plantcare API directory: $plantcare_dir
cd $plantcare_dir
sudo docker-compose up -d
plantcare-services=$(sudo docker-compose ps --services)
echo Waiting for healthy PlantCare services
for service in ${plantcare-services[@]}; do
    status=$(sudo docker inspect --format='{{.State.Health.Status}}' "$container_id" 2>/dev/null || echo "no-healthcheck")
    if [ "$status" == "healthy" ] || [ "$status" == "no-healthcheck" ]; then
        echo "$service is ready (status: $status)"
        break
    else
        sleep 1
    fi
done
echo PlantCare services are healthy
cd ..

echo PlantCare containers are ready to use