# AI-Powered Citation Tracking for GitHub Copilot

This project provides tools and libraries for tracking and managing citations when using AI-assisted coding tools like GitHub Copilot. Proper citation tracking ensures that you give appropriate credit to sources when generating code or documents.

## Project Structure

- **Python Implementation**: [`/python`](./python/) - Citation tracking for Python projects
- **C# Implementation**: [`/csharp`](./csharp/) - Citation tracking for C# projects
- **Tools**: [`/tools`](./tools/) - Git hooks and utility scripts

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


## Implementation for C# Projects

If you're working with C# projects, you can use our dedicated C# citation tracking implementation.

### 1. Install via NuGet Package

```bash
Install-Package CitationTracker
```

Or add it via the NuGet Package Manager in Visual Studio.

### 2. Initialize the Tracker in Your Project

```csharp
using CitationTracking;

// Initialize with your project path
var tracker = new CitationTracker();
```

### 3. Workflow: Using Copilot with Citations in C#

#### Step 1: Ask Copilot a Question

When asking Copilot to generate C# code, be specific about what you're looking for:

```
@Copilot: Help me implement a method to handle JWT token validation
```

#### Step 2: Review and Accept Generated Code

Review the code Copilot generates, make necessary adjustments, and then accept it.

#### Step 3: Add Citation Information

Immediately after accepting code from Copilot, add citation information:

```csharp
// Add the source
tracker.AddSource(
    sourceId: "ms-identity-web",
    name: "Microsoft Identity Web Authentication Library",
    url: "https://docs.microsoft.com/en-us/azure/active-directory/develop/",
    author: "Microsoft Identity Team",
    licenseType: "MIT",
    description: "JWT token validation implementation pattern"
);

// Cite the source in your file
tracker.CiteInFile(
    filePath: "Services/AuthService.cs",
    sourceId: "ms-identity-web",
    lineStart: 45,  // Starting line of the relevant code
    lineEnd: 60,    // Ending line of the relevant code
    comment: "Token validation logic adapted from Microsoft Identity documentation"
);
```

#### Step 4: Add Attribution Headers

For files with significant external contributions:

```csharp
// Generate an attribution header
string header = tracker.InsertAttributionHeader("Services/AuthService.cs");

// Manually insert at the top of the file
// Or use the Visual Studio extension to do this automatically
```

#### Step 5: Generate Citations Document

Before sharing or publishing your code:

```csharp
// Generate a markdown file with all citations
tracker.ExportCitationsMarkdown();
```

### 4. Visual Studio Integration

You can enhance your workflow by:

1. Using the Visual Studio extension (conceptual, to be implemented)
2. Setting up MSBuild integration to automatically update citations during build
3. Using Git hooks to ensure citations are updated before commits

### 5. MSBuild Integration

Add the following to your .csproj file:

```xml
<ItemGroup>
  <PackageReference Include="CitationTracker" Version="1.0.0" />
</ItemGroup>

<PropertyGroup>  <UpdateCitations>true</UpdateCitations>
</PropertyGroup>
```

This will automatically update your CITATIONS.md file during build.

## Getting Started

Based on your project type, follow the setup instructions in either:
- [Python Implementation README](./python/README.md)
- [C# Implementation README](./csharp/README.md)

## Basic Workflow

1. When using Copilot to generate code, identify potential sources
2. Add the source using the appropriate method:
   - Python: `tracker.add_source()`
   - C#: `tracker.AddSource()`
3. Cite the source in your file
4. Generate a consolidated CITATIONS.md document

## IDE Integration

Both implementations provide concepts for IDE integration:
- Visual Studio extension concept for C# projects
- VS Code extension concept for any project type

## Build & Git Integration

- C# projects: MSBuild task for build-time citations update
- Git hooks: Pre-commit hooks for automatic CITATIONS.md updates

## Conclusion

By integrating these citation tracking practices into your workflow with GitHub Copilot, you ensure proper attribution for code sources, maintain licensing compliance, and create a transparent record of how AI assisted in your development process.

Remember that citation tracking is not just about legal compliance—it's about responsible use of AI and respect for the broader developer community whose work and knowledge have contributed to the AI's capabilities.
