# Project Citations

This document lists all external sources used in this project.

## Sources

### Amazon Advertising API
**Author:** Amazon.com, Inc.
**URL:** [https://advertising.amazon.com/API/docs/en-us/](https://advertising.amazon.com/API/docs/en-us/)
**License:** Amazon Advertising API License Agreement

Official Amazon Advertising API documentation used as reference for implementing the client

### Python Requests Library Advanced Patterns
**Author:** Kenneth Reitz
**URL:** [https://requests.readthedocs.io/en/latest/user/advanced/](https://requests.readthedocs.io/en/latest/user/advanced/)
**License:** Apache 2.0

Advanced usage patterns for the Python Requests library

### Stack Overflow: OAuth2 Token Refresh Strategy
**Author:** User: JohnSmith123
**URL:** [https://stackoverflow.com/questions/27771324/](https://stackoverflow.com/questions/27771324/)
**License:** CC BY-SA 4.0

Solution for handling token refresh with a buffer time

## Usage by File

### ad_api_client.py
- Uses: Amazon Advertising API (lines 1-310)
  - Note: Overall structure based on Amazon Advertising API documentation
- Uses: Python Requests Library Advanced Patterns (lines 150-200)
  - Note: Advanced request handling pattern adapted from Python Requests documentation
- Uses: Stack Overflow: OAuth2 Token Refresh Strategy (lines 45-80)
  - Note: Token refresh strategy with 5-minute buffer inspired by Stack Overflow answer
