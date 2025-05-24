# AI-Powered Citation Tracking for GitHub Copilot (Python Implementation)

This guide explains how to implement and use the Python version of the AI citation tracking system when working with GitHub Copilot.

## Why Track Citations in AI-Generated Content?

When using AI tools like GitHub Copilot to assist with code generation, the AI might base its suggestions on existing code from various sources, including:

1. Open-source libraries and frameworks
2. Documentation examples
3. Stack Overflow answers
4. GitHub repositories
5. Academic papers and tutorials

Properly tracking these sources is important for:

- **Legal compliance** with licensing requirements
- **Ethical transparency** about the origins of code
- **Acknowledging contributions** from the community
- **Enabling verification** of implementation details
- **Building trust** in your development process

## Getting Started with Python Citation Tracking

### 1. Install the Citation Tracker

```bash
# Option 1: Install from PyPI
pip install copilot-citation-tracker

# Option 2: Install from source
pip install -e .
```

Or copy the `citation_tracker.py` module directly into your project.

### 2. Initialize the Tracker in Your Project

```python
from citation_tracker import CitationTracker

# Initialize with your project path
tracker = CitationTracker()
```

### 3. Workflow: Using Copilot with Citations

#### Step 1: Ask Copilot a Question

When asking Copilot to generate code, be specific about what you're looking for:

```
@Copilot: Help me implement a function to parse OAuth tokens from an Amazon API response
```

#### Step 2: Review and Accept Generated Code

Review the code Copilot generates, make necessary adjustments, and then accept it.

#### Step 3: Add Citation Information

Immediately after accepting code from Copilot, add citation information:

```python
# Add the source
tracker.add_source(
    source_id="amazon-oauth-docs",
    name="Amazon OAuth Implementation Guide",
    url="https://developer.amazon.com/docs/login-with-amazon/authorization-code-grant.html",
    author="Amazon Developer Documentation",
    license_type="Amazon Developer Terms",
    description="Implementation pattern for OAuth token parsing"
)

# Cite the source in your file
tracker.cite_in_file(
    file_path="auth_utils.py",
    source_ids=["amazon-oauth-docs"],
    line_start=45,  # Starting line of the relevant code
    line_end=60,    # Ending line of the relevant code
    comment="Token parsing logic adapted from Amazon documentation"
)
```

#### Step 4: Add Attribution Headers

For files with significant external contributions:

```python
# Generate an attribution header
header = tracker.insert_attribution_header("auth_utils.py")

# Manually insert at the top of the file
# Or use an editor extension to do this automatically
```

#### Step 5: Generate Citations Document

Before sharing or publishing your code:

```python
# Generate a markdown file with all citations
tracker.export_citations_markdown()
```

## Automating Citation Tracking

### Using Git Hooks

The repository includes a Python git hook script that you can use to automatically update citations before committing:

1. Copy `citation-git-hook.py` to your repository's `.git/hooks/pre-commit` file
2. Make it executable: `chmod +x .git/hooks/pre-commit`

### Using VS Code Extension

The conceptual VS Code extension (to be implemented) would provide:

1. Commands to quickly insert citations
2. A sidebar to view all citations in the workspace
3. Hover information over citation markers
4. Automatic updating of the citations document

## Best Practices for AI Citation

1. **Cite Promptly**: Add citations immediately after adding AI-generated code
2. **Be Specific**: Include exact line numbers and clear descriptions
3. **Include License Information**: Always note the license type when available
4. **Link to Original Sources**: Provide URLs to the original documentation or code
5. **Update When Changing**: When modifying AI-generated code significantly, update the citation
6. **Review Before Publishing**: Always check CITATIONS.md before making code public

## Citation Format Examples

### For Algorithms and Patterns

```python
tracker.add_source(
    source_id="quick-sort-algorithm",
    name="QuickSort Implementation",
    author="GeeksforGeeks",
    url="https://www.geeksforgeeks.org/quick-sort/",
    license_type="CC BY-SA",
    description="Efficient implementation of QuickSort algorithm"
)
```

### For API Integration Patterns

```python
tracker.add_source(
    source_id="twitter-api-oauth",
    name="Twitter API OAuth Flow",
    url="https://developer.twitter.com/en/docs/authentication/oauth-1-0a",
    author="Twitter Developer Documentation",
    license_type="Developer Agreement",
    description="OAuth 1.0a implementation pattern for Twitter API"
)
```

## Conclusion

By integrating these citation tracking practices into your workflow with GitHub Copilot, you ensure proper attribution for code sources, maintain licensing compliance, and create a transparent record of how AI assisted in your development process.
