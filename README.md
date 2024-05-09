# MatchingEngine

## Overview

MatchingEngine is a C# CLI program designed to perform efficient matching of orders in a trading system.

## Prerequisites Installation

Before running MatchingEngine, ensure that you have the necessary prerequisites installed. You can use the provided script `install_prerequisites.sh` to install them. Execute the following command to make the script executable:

`chmod +x install_prerequisites.sh` 

Then, run the script to install the prerequisites:

`./install_prerequisites.sh` 

## Building the Program

To build MatchingEngine, you can use the provided `make` utility. Execute the following command in the root directory of the project:

`make` 

This command will compile the source code and generate the executable for the MatchingEngine program. It will also run the unit tests using the NUnit framework, version 3.14.0.

## Usage

After building the program, you can run it using the generated executable. It will prompt the user to input the trades. Once you have input all the trades, hit enter twice and it will execute the matching algorithm.

Example usage:

`./MatchingEngine`
