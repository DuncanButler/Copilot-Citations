using System;
using System.IO;
using Microsoft.Build.Framework;

// Add an alias for the local CitationTracker
using LocalCitationTracker = CitationTracking.CitationTracker;

namespace CitationMSBuild;

/// <summary>
/// MSBuild task to update citations document during build process
/// </summary>
public class UpdateCitationsTask : Microsoft.Build.Utilities.Task
{
    /// <summary>
    /// The root directory of the project
    /// </summary>
    [Required]
    public string ProjectDirectory { get; set; }

    /// <summary>
    /// Optional path to output the citations markdown file
    /// </summary>
    public string OutputPath { get; set; }

    /// <summary>
    /// Execute the task to update citations
    /// </summary>
    /// <returns>True if successful, false otherwise</returns>
    public override bool Execute()
    {
        try
        {
            var tracker = new LocalCitationTracker(ProjectDirectory);
            string outputPath = OutputPath ?? Path.Combine(ProjectDirectory, "CITATIONS.md");
            
            string result = tracker.ExportCitationsMarkdown(outputPath);
            Log.LogMessage(MessageImportance.Normal, $"Citations document updated at: {result}");
            
            return true;
        }
        catch (Exception ex)
        {
            Log.LogError($"Failed to update citations: {ex.Message}");
            return false;
        }
    }
}
