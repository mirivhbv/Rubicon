#!/bin/bash

if [ "$#" -ne 4 ]; then
  echo "Usage: $0 <dbname> <password> <dotnet_dll> <migration_sql>"
  exit 1
fi

DBNAME=$1
PASSWORD=$2
DOTNET_DLL=$3
MIGRATION_SQL=$4

export PGPASSWORD=$PASSWORD
export PGCONNECT_TIMEOUT=20

if psql -h postgres --username postgres -lqt | cut -d \| -f 1 | grep -qw $DBNAME; then
    echo "$DBNAME database exist, skipping..."
else
    echo "Creating new database $DBNAME"
	psql -h postgres --username postgres -c "CREATE DATABASE $DBNAME WITH ENCODING 'UTF8'"
fi

psql -h postgres --username postgres -d $DBNAME -a -f $MIGRATION_SQL

echo "Running the .NET application..."
dotnet $DOTNET_DLL
