#!/bin/bash
# Simple script to run the Python citation example

# Ensure we're in the right directory
cd "$(dirname "$0")"

# Run the example
python citation_example.py

echo
echo "Run completed. Press Enter to exit..."
read
