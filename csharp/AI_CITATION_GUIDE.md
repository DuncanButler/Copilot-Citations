# AI-Powered Citation Tracking for GitHub Copilot (C# Implementation)

This guide explains how to implement and use the C# version of the AI citation tracking system when working with GitHub Copilot.

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

## Getting Started with C# Citation Tracking

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

<PropertyGroup>
  <UpdateCitations>true</UpdateCitations>
</PropertyGroup>
```

This will automatically update your CITATIONS.md file during build.

### 6. Git Hook Integration

The repository includes a PowerShell git hook script that you can use to automatically update citations before committing:

1. Copy the `citation-git-hook.ps1` file to your repository's `.git/hooks/pre-commit` file
2. Ensure PowerShell execution is allowed for the script

## Best Practices for AI Citation

1. **Cite Promptly**: Add citations immediately after adding AI-generated code
2. **Be Specific**: Include exact line numbers and clear descriptions
3. **Include License Information**: Always note the license type when available
4. **Link to Original Sources**: Provide URLs to the original documentation or code
5. **Update When Changing**: When modifying AI-generated code significantly, update the citation
6. **Review Before Publishing**: Always check CITATIONS.md before making code public

## Citation Format Examples

### For Algorithms and Patterns

```csharp
tracker.AddSource(
    sourceId: "quick-sort-algorithm",
    name: "QuickSort Implementation",
    author: "GeeksforGeeks",
    url: "https://www.geeksforgeeks.org/quick-sort/",
    licenseType: "CC BY-SA",
    description: "Efficient implementation of QuickSort algorithm"
);
```

### For API Integration Patterns

```csharp
tracker.AddSource(
    sourceId: "ms-graph-api",
    name: "Microsoft Graph API Authentication Flow",
    url: "https://docs.microsoft.com/en-us/graph/auth/",
    author: "Microsoft Graph Documentation",
    licenseType: "Developer Agreement",
    description: "Implementation pattern for Microsoft Graph API authentication"
);
```

## Conclusion

By integrating these citation tracking practices into your workflow with GitHub Copilot, you ensure proper attribution for code sources, maintain licensing compliance, and create a transparent record of how AI assisted in your development process.
