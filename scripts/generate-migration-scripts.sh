#!/bin/bash

if [ "$#" -ne 2 ]; then
  echo "Usage: $0 <project_path> <output_filename>"
  exit 1
fi

PROJECT_PATH=$1
OUTPUT_FILENAME=$2

# Install the dotnet-ef tool
dotnet tool install --global dotnet-ef

# Add the dotnet tools to the PATH
export PATH="$PATH:/root/.dotnet/tools"

# Generate the migration script
dotnet ef migrations script --idempotent -p $1 -o /sql/$OUTPUT_FILENAME
