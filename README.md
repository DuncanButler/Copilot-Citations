# Copilot-Citations: AI-Powered Citation Tracking

This project provides tools and libraries for tracking and managing citations when using AI-assisted coding tools like GitHub Copilot. Proper citation tracking ensures that you give appropriate credit to sources when generating code or documents.

## Project Structure

- **Python Implementation**: [`/python`](./python/) - Citation tracking for Python projects
- **C# Implementation**: [`/csharp`](./csharp/) - Citation tracking for C# projects
- **Shared Resources**: [`/shared`](./shared/) - Example files and common resources
- **Tools**: [`/tools`](./tools/) - Git hooks and utility scripts
- **Documentation**: [`AI_CITATION_GUIDE.md`](./AI_CITATION_GUIDE.md) - Comprehensive citation guide

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

## Quick Start

### For Python Projects

1. **Install the Citation Tracker**

```bash
# From the repository root
cd python
pip install -e .
```

2. **Using the Citation Tracker**

```python
from citation_tracker import CitationTracker

# Initialize tracker
tracker = CitationTracker()

# Add a source
tracker.add_source(
    source_id="example-source",
    name="Example Library Documentation",
    url="https://example.com/docs",
    author="Example Author",
    license_type="MIT",
    description="Pattern for handling example functionality"
)

# Cite in file
tracker.cite_in_file(
    file_path="my_file.py",
    source_ids=["example-source"],
    line_start=10,
    line_end=20,
    comment="Implementation based on example library documentation"
)

# Generate citations file
tracker.export_citations_markdown()
```

For more detailed Python usage, see the [Python Implementation README](./python/README.md).

### For C# Projects

1. **Install the Citation Tracker**

```bash
# Using NuGet Package Manager Console
Install-Package CitationTracker
```

2. **Using the Citation Tracker**

```csharp
using CitationTracking;

// Initialize tracker
var tracker = new CitationTracker();
```

# Add a source
tracker.AddSource(
    sourceId: "example-source",
    name: "Example Library Documentation",
    url: "https://example.com/docs",
    author: "Example Author",
    licenseType: "MIT",
    description: "Pattern for handling example functionality"
);

# Cite in file
tracker.CiteInFile(
    filePath: "MyFile.cs",
    sourceId: "example-source",
    lineStart: 10,
    lineEnd: 20,
    comment: "Implementation based on example library documentation"
);

# Generate citations file
tracker.ExportCitationsMarkdown();
```

For more detailed C# usage, see the [C# Implementation README](./csharp/README.md).

## Git Hook Integration

This repository provides Git hooks for both Python and PowerShell to automatically update citations before each commit:

- **Python Git Hook**: [citation-git-hook.py](./tools/citation-git-hook.py)
- **PowerShell Git Hook**: [citation-git-hook.ps1](./tools/citation-git-hook.ps1)

To install a Git hook:

1. Copy the appropriate hook to your repository's `.git/hooks/pre-commit` file
2. Make it executable (for Python hook on Unix systems)

## Comprehensive Documentation

For detailed usage instructions and best practices, refer to:

- [AI Citation Guide](./AI_CITATION_GUIDE.md) - Complete documentation on citation practices
- [Python Implementation Guide](./python/AI_CITATION_GUIDE.md) - Python-specific instructions
- [C# Implementation Guide](./csharp/AI_CITATION_GUIDE.md) - C#-specific instructions
- [Installation Guide](./INSTALL.md) - Detailed installation instructions
## Example Files

Check out the [shared/examples](./shared/examples/) directory for:

- Example citations JSON file
- Example CITATIONS.md output
- Sample implementation patterns

## IDE Integration

Both implementations provide concepts for IDE integration:
- Visual Studio extension concept for C# projects
- VS Code extension concept for any project type

## Build Integration for C# Projects

For C# projects, MSBuild integration allows automatic citation updates during build:

```xml
<ItemGroup>
  <PackageReference Include="CitationTracker" />
</ItemGroup>

<PropertyGroup>
  <UpdateCitations>true</UpdateCitations>
</PropertyGroup>
```

The package versions are managed centrally through the `Directory.Packages.props` file.

## License

This project is licensed under the MIT License - see the [LICENSE](./LICENSE) file for details.
