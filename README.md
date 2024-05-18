# Trade Matching Engine
This command line application serves as a basic exchange or an "order matcher", processing limit order input from standard input (stdin), matching orders, and outputting trade information to standard output (stdout). The application follows specific requirements outlined below.

## Requirements

- **Limit Orders**: Orders to buy or sell a quantity of an instrument at or better than a specified price (limit price).
    - Format: `<buyer/sellerid>:<instrument>:<signedquantity>:<limitprice>`
    - Example: `A:AUDUSD:-100:1.47` (Sell 100 AUD against USD at a price of 1.47 or more)
- **Matching Criteria**:
    - A match occurs when two limit orders meet the following conditions:
        - Both orders are for the same instrument.
        - One order is a buy (positive signed quantity), and the other is a sell (negative signed quantity).
        - The buy limit price is equal to or higher than the sell limit price.
    - Result of a match: A trade at the match price and quantity.
        - Match price: The limit price of the first order input of the two orders.
        - Match quantity: Minimum of the two unsigned order quantities.
- **Processing Order**: Orders are processed and matched in the order of receipt.
- **Matching Priority**: Matching occurs against the most aggressively priced candidate.
    - For sells: Lowest price
    - For buys: Highest price
    - If multiple candidates have the most aggressive price, matching occurs against the candidate first received.
- **Self-Trading**: A buyer is allowed to trade with itself.
- **Usage**:
    ```
    ./TradeMatchingEngine < orders.txt > trades.txt
    ```
### Examples

**Input**:
```
A:AUDUSD:100:1.47
B:AUDUSD:-50:1.45
```

**Output**:
```
A:B:AUDUSD:50:1.47
```

**Input**:
```
A:GBPUSD:100:1.66
B:EURUSD:-100:1.11
F:EURUSD:-50:1.1
C:GBPUSD:-10:1.5
C:GBPUSD:-20:1.6
C:GBPUSD:-20:1.7
D:EURUSD:100:1.11
```

**Output**:
```
A:C:GBPUSD:10:1.66
A:C:GBPUSD:20:1.66
D:F:EURUSD:50:1.1
D:B:EURUSD:50:1.11
```

## Implementation Details

- The application processes limit orders, matches them based on the specified criteria, and outputs trade information accordingly.
- Written in C#.
- The source code and any necessary scripts or project files are provided for compilation and usage.
- Instructions for compiling and running the application are provided.
- External dependencies are minimized, focusing on demonstrating language and standard library skills.
- Unit tests are included for further validation and reliability.

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
