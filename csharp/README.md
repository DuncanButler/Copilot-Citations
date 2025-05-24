# C# Citation Tracker

This folder contains the C# implementation of the Citation Tracker for AI-generated code.

## Components

- `CitationTracker.cs`: Core class library that provides citation tracking functionality
- `CitationExample.cs`: Example demonstrating how to use the citation tracker
- `VisualStudioExtensionConcept.cs`: Conceptual implementation of a Visual Studio extension
- `CitationMSBuildTask.cs`: MSBuild task for integrating citation updates into the build process

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

## Basic Usage

Initialize the tracker:
```csharp
using CitationTracking;

var tracker = new CitationTracker();
```

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

## Visual Studio Integration

The `VisualStudioExtensionConcept.cs` file provides a conceptual implementation of how citation tracking could be integrated into Visual Studio as an extension. This is not a functional extension but demonstrates the concepts.

## Git Integration

A pre-commit git hook is available in the `tools` directory. To use it:

1. Copy `../tools/citation-git-hook.ps1` to your project's `.git/hooks/pre-commit`
2. Ensure PowerShell is installed and can execute scripts

This will automatically update your `CITATIONS.md` file before each commit.
