# C# Citation Tracker

This folder contains the C# implementation of the Citation Tracker for AI-generated code.

## Components

- `CitationTracker/`: Core class library that provides citation tracking functionality
  - Uses modern file-scoped namespaces for cleaner code organization (C# 10+ style)
- `CitationExample/`: Console application demonstrating how to use the citation tracker
- `CitationMSBuild/`: MSBuild task for integrating citation updates into the build process
- `build/`: MSBuild targets file for automatic citations generation during build
- `VisualStudioExtensionConcept.cs`: Conceptual implementation of a Visual Studio extension

## Installation

The CitationTracker is designed to be distributed as a NuGet package. Once implemented, you can install it via:

```
Install-Package CitationTracker
```

Or add it to your project file:

```xml
<ItemGroup>
  <PackageReference Include="CitationTracker" Version="1.0.0" />
</ItemGroup>
```

If you're using central package versioning (recommended):

```xml
<!-- In Directory.Packages.props -->
<ItemGroup>
  <PackageVersion Include="CitationTracker" Version="1.0.0" />
</ItemGroup>

<!-- In your .csproj file -->
<ItemGroup>
  <PackageReference Include="CitationTracker" />
</ItemGroup>
```

> **Note**: This project uses central package versioning with `Directory.Packages.props` to manage dependency versions consistently across projects.

## Basic Usage

Initialize the tracker:
```csharp
using CitationTracking;

// With modern file-scoped namespace
namespace YourNamespace;

// Your code goes here
var tracker = new CitationTracker();
```

> **Note**: The project uses modern file-scoped namespaces (introduced in C# 10), which replace the traditional namespace blocks with a simpler syntax that applies to the entire file.

Add a source:
```csharp
tracker.AddSource(
    sourceId: "msft-graph-api",
    name: "Microsoft Graph API",
    url: "https://docs.microsoft.com/en-us/graph/api/overview",
    author: "Microsoft Corporation",
    licenseType: "MIT",
    description: "Microsoft Graph REST API documentation used for authentication flow implementation"
);
```

Cite a source in a file:
```csharp
tracker.CiteInFile(
    filePath: "Services/AuthService.cs",
    sourceId: "msft-graph-api",
    lineStart: 45,
    lineEnd: 120,
    comment: "Authentication flow implementation based on Microsoft Graph API examples"
);
```

Generate citations document:
```csharp
tracker.ExportCitationsMarkdown();
```

## MSBuild Integration

To automatically update citations during builds, add the following to your .csproj file:

```xml
<PropertyGroup>
  <UpdateCitations>true</UpdateCitations>
</PropertyGroup>
```

This will trigger the `UpdateCitationsTask` during build, which will automatically generate or update your `CITATIONS.md` file based on the citations stored in your project.

### How MSBuild Integration Works

1. The `CitationMSBuild.targets` file defines a task that runs after the build
2. When enabled via the `UpdateCitations` property, it processes all citations
3. It outputs a formatted markdown file with all citations in your project

## Project Requirements

- .NET 9.0
- Central Package Management via Directory.Packages.props
- Modern C# features including file-scoped namespaces

## Visual Studio Integration

The `VisualStudioExtensionConcept.cs` file provides a conceptual implementation of how citation tracking could be integrated into Visual Studio as an extension. This is not a functional extension but demonstrates the concepts.

## Git Integration

A pre-commit git hook is available in the `tools` directory. To use it:

1. Copy `../tools/citation-git-hook.ps1` to your project's `.git/hooks/pre-commit`
2. Ensure PowerShell is installed and can execute scripts

This will automatically update your `CITATIONS.md` file before each commit.
