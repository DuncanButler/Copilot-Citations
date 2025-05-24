using System;
using CitationTracking;

namespace CitationExample
{
    /// <summary>
    /// Example usage of the citation tracker with GitHub Copilot in C# projects.
    /// This demonstrates how to integrate citation tracking into your workflow.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Citation Tracker for C# Example");
            
            // Initialize the citation tracker
            var tracker = new CitationTracker();
            
            // Example 1: Adding citations for a Microsoft API
            tracker.AddSource(
                sourceId: "msft-graph-api",
                name: "Microsoft Graph API",
                url: "https://docs.microsoft.com/en-us/graph/api/overview",
                author: "Microsoft Corporation",
                licenseType: "MIT",
                description: "Microsoft Graph REST API documentation used for authentication flow implementation"
            );
            
            // Example 2: Citation for a C# design pattern
            tracker.AddSource(
                sourceId: "repository-pattern-ef",
                name: "Repository Pattern with Entity Framework Core",
                url: "https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design",
                author: "Microsoft Documentation Team",
                licenseType: "MIT",
                description: "Implementation pattern for data access layer using Repository pattern with EF Core"
            );
            
            // Example 3: Citation for a Stack Overflow answer
            tracker.AddSource(
                sourceId: "stackoverflow-async-pattern",
                name: "Stack Overflow: Best Practices for Async/Await in C#",
                url: "https://stackoverflow.com/questions/38017016/",
                author: "User: StephenCleary",
                licenseType: "CC BY-SA 4.0",
                description: "Approach for handling asynchronous operations with proper exception handling"
            );
            
            // Cite sources in files
            string userServiceFile = @"c:\project\Services\UserService.cs";
            tracker.CiteInFile(
                filePath: userServiceFile,
                sourceId: "msft-graph-api",
                lineStart: 45,
                lineEnd: 120,
                comment: "Authentication flow implementation based on Microsoft Graph API examples"
            );
            
            string repositoryFile = @"c:\project\Data\UserRepository.cs";
            tracker.CiteInFile(
                filePath: repositoryFile, 
                sourceId: "repository-pattern-ef",
                lineStart: 15, 
                lineEnd: 85,
                comment: "Repository implementation based on Microsoft documentation"
            );
            
            string asyncHelperFile = @"c:\project\Helpers\AsyncHelper.cs";
            tracker.CiteInFile(
                filePath: asyncHelperFile, 
                sourceId: "stackoverflow-async-pattern",
                lineStart: 25, 
                lineEnd: 50,
                comment: "Async execution pattern adapted from Stack Overflow answer"
            );
            
            // Generate a header comment for a file
            string attributionHeader = tracker.InsertAttributionHeader(userServiceFile);
            Console.WriteLine("\nGenerated attribution header:");
            Console.WriteLine(attributionHeader);
            
            // Generate and export a markdown file with all citations
            string citationsMdPath = tracker.ExportCitationsMarkdown();
            Console.WriteLine($"\nExported citations to: {citationsMdPath}");
            
            Console.WriteLine("\nCitation tracking workflow in C#:");
            Console.WriteLine("1. When using Copilot to generate code based on a specific source:");
            Console.WriteLine("   - Add the source using tracker.AddSource()");
            Console.WriteLine("   - Immediately cite it in the file using tracker.CiteInFile()");
            Console.WriteLine("2. For each new file, consider adding an attribution header");
            Console.WriteLine("3. Before releasing/publishing, export to CITATIONS.md");
        }
    }
}
