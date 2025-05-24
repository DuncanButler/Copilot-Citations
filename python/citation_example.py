"""
Example usage of the citation tracker with GitHub Copilot
This demonstrates how to integrate citation tracking into your workflow
when using GitHub Copilot agent mode.
"""
import os
import sys

# Add the current directory to the path so we can import the citation_tracker module
current_dir = os.path.dirname(os.path.abspath(__file__))
if current_dir not in sys.path:
    sys.path.insert(0, current_dir)

from citation_tracker import CitationTracker

# Initialize the citation tracker for the project
tracker = CitationTracker()

# Example 1: Adding citations for an API wrapper
# When you use Copilot to help create code based on documentation
tracker.add_source(
    source_id="amazon-advertising-api",
    name="Amazon Advertising API",
    url="https://advertising.amazon.com/API/docs/en-us/",
    author="Amazon.com, Inc.",
    license_type="Amazon Advertising API License Agreement",
    description="Official Amazon Advertising API documentation used as reference for implementing the client"
)

# Example 2: Citation for a Python code pattern
tracker.add_source(
    source_id="requests-advanced-pattern",
    name="Python Requests Library Advanced Patterns",
    url="https://requests.readthedocs.io/en/latest/user/advanced/",
    author="Kenneth Reitz",
    license_type="Apache 2.0",
    description="Advanced usage patterns for the Python Requests library"
)

# Example 3: Citation for a Stack Overflow answer
tracker.add_source(
    source_id="stackoverflow-token-refresh",
    name="Stack Overflow: OAuth2 Token Refresh Strategy",
    url="https://stackoverflow.com/questions/27771324/",
    author="User: JohnSmith123",
    license_type="CC BY-SA 4.0",
    description="Solution for handling token refresh with a buffer time"
)

# Cite sources in a file
demo_file = os.path.join(os.getcwd(), "ad_api_client.py")
tracker.cite_in_file(
    file_path=demo_file, 
    source_ids=["amazon-advertising-api"],
    line_start=1, 
    line_end=310,
    comment="Overall structure based on Amazon Advertising API documentation"
)

tracker.cite_in_file(
    file_path=demo_file, 
    source_ids=["requests-advanced-pattern"],
    line_start=150, 
    line_end=200,
    comment="Advanced request handling pattern adapted from Python Requests documentation"
)

tracker.cite_in_file(
    file_path=demo_file, 
    source_ids=["stackoverflow-token-refresh"],
    line_start=45, 
    line_end=80,
    comment="Token refresh strategy with 5-minute buffer inspired by Stack Overflow answer"
)

# Generate a header comment for a file
attribution_header = tracker.insert_attribution_header(demo_file)
print("\nGenerated attribution header:")
print(attribution_header)

# Generate and export a markdown file with all citations
citations_md_path = tracker.export_citations_markdown()
print(f"\nExported citations to: {citations_md_path}")

print("\nCitation tracking workflow:")
print("1. When using Copilot to generate code based on a specific source:")
print("   - Add the source using tracker.add_source()")
print("   - Immediately cite it in the file using tracker.cite_in_file()")
print("2. For each new file, consider adding an attribution header")
print("3. Before releasing/publishing, export to CITATIONS.md")
