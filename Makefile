# Makefile for MatchingEngineSolution

# Variables
SOLUTION_NAME := MatchingEngineSolution
SOLUTION_DIR := $(CURDIR)
MATCHING_ENGINE_DIR := $(SOLUTION_DIR)/MatchingEngine
MATCHING_ENGINE_TESTS_DIR := $(SOLUTION_DIR)/MatchingEngine.Tests

# Targets
.PHONY: all clean build test

all: clean build test

clean:
	@echo "Cleaning MatchingEngine project..."
	@dotnet clean $(MATCHING_ENGINE_DIR)
	@rm -rf $(MATCHING_ENGINE_DIR)/bin
	@rm -rf $(MATCHING_ENGINE_DIR)/obj
	@echo "Cleaning MatchingEngine.Tests project..."
	@dotnet clean $(MATCHING_ENGINE_TESTS_DIR)
	@rm -rf $(MATCHING_ENGINE_TESTS_DIR)/bin
	@rm -rf $(MATCHING_ENGINE_TESTS_DIR)/obj
	@echo "Cleanup complete."

build:
	@echo "Restoring packages..."
	@dotnet restore $(SOLUTION_DIR)
	@echo "Building MatchingEngine project..."
	@dotnet build $(MATCHING_ENGINE_DIR)
	@echo "Building MatchingEngine.Tests project..."
	@dotnet build $(MATCHING_ENGINE_TESTS_DIR)
	@echo "Build complete."

test:
	@echo "Running tests..."
	@dotnet test $(MATCHING_ENGINE_TESTS_DIR)
