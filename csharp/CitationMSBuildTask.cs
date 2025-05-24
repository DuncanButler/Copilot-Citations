using System;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using CitationTracking;

namespace CitationMSBuild
{
    /// <summary>
    /// MSBuild task to update citations document during build process
    /// </summary>
    public class UpdateCitationsTask : Task
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
                Log.LogMessage(MessageImportance.Normal, "Updating citations document...");
                
                var tracker = new CitationTracker(ProjectDirectory);
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
}

/*
 * Example .targets file content:
 * 
 * <Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
 *   <UsingTask TaskName="CitationMSBuild.UpdateCitationsTask" 
 *             AssemblyFile="$(MSBuildThisFileDirectory)..\tools\CitationMSBuild.dll" />
 *             
 *   <Target Name="UpdateCitations" AfterTargets="Build" Condition="'$(UpdateCitations)' != 'false'">
 *     <UpdateCitationsTask 
 *       ProjectDirectory="$(MSBuildProjectDirectory)"
 *       OutputPath="$(MSBuildProjectDirectory)\CITATIONS.md" />
 *   </Target>
 * </Project>
 */
