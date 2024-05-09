#!/bin/bash

# Update package lists
sudo apt update

# Install .NET SDK
echo "Installing .NET SDK..."
sudo apt install -y apt-transport-https
sudo apt install -y wget
wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
sudo apt update
sudo apt install -y dotnet-sdk-8.0

# Install NUnit
echo "Installing NUnit..."
dotnet new tool-manifest --force
dotnet tool install --global NUnit.ConsoleRunner.NetCore --version 3.14.0

echo "Prerequisites installation complete."
