# Installation Guide

This guide provides detailed instructions on how to install and set up the Citation Tracker in your projects.

## Python Implementation

### Requirements
- Python 3.7 or higher

### Installation Options

#### 1. Direct Copy
The simplest way to use the Citation Tracker is to copy `citation_tracker.py` directly into your project:

```bash
cp python/citation_tracker.py /path/to/your/project/
```

#### 2. Install as a Package
You can also install the Citation Tracker as a Python package:

```bash
# From the repository root
cd python
pip install -e .
```

#### 3. Install from GitHub
```bash
pip install git+https://github.com/yourusername/Copilot-Citations.git#subdirectory=python
```

### Usage

```python
from citation_tracker import CitationTracker

# Initialize the tracker
tracker = CitationTracker()

# See python/README.md for more detailed usage examples
```

## C# Implementation

### Requirements
- .NET 9.0 or higher
- Visual Studio 2022 or higher (optional, for development)

### Installation Options

#### 1. Build and Reference the DLL
1. Build the C# project:
   ```bash
   cd csharp
   dotnet build
   ```
2. Reference the built DLL in your project.

#### 2. Reference the Project
Add a project reference to `CitationTracker.csproj` in your own solution.

#### 3. Using Central Package Versioning
We recommend using central package versioning to manage dependencies:

1. Create a `Directory.Packages.props` file in your solution root:
   ```xml
   <Project>
     <PropertyGroup>
       <ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>
     </PropertyGroup>
     <ItemGroup>
       <PackageVersion Include="CitationTracker" Version="1.0.0" />
     </ItemGroup>
   </Project>
   ```

2. Reference the package without version in your project file:
   ```xml
   <ItemGroup>
     <PackageReference Include="CitationTracker" />
   </ItemGroup>
   ```

#### 4. NuGet Package (Future)
When published to NuGet:
```
Install-Package CitationTracker
```

### Usage

```csharp
using CitationTracking;

// With modern file-scoped namespace (C# 10+)
namespace YourNamespace;

// Initialize the tracker
var tracker = new CitationTracker();

// See csharp/README.md for more detailed usage examples
```

## Git Hooks Integration

### Python Projects
1. Copy the Git hook to your project:
   ```bash
   cp tools/citation-git-hook.py /path/to/your/project/.git/hooks/pre-commit
   chmod +x /path/to/your/project/.git/hooks/pre-commit
   ```

### C# Projects
1. Copy the Git hook to your project:
   ```powershell
   Copy-Item tools/citation-git-hook.ps1 /path/to/your/project/.git/hooks/pre-commit
   ```

## IDE Integration
See the respective README files for information about IDE integration:
- [Python VS Code Integration](./python/README.md)
- [C# Visual Studio Integration](./csharp/README.md)
