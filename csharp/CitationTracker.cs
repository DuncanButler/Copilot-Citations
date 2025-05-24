using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CitationTracking
{
    /// <summary>
    /// A class for tracking citations in AI-generated content for C# projects.
    /// </summary>
    public class CitationTracker
    {
        private string projectPath;
        private string citationsFile;
        private CitationData citations;

        /// <summary>
        /// Initialize the citation tracker.
        /// </summary>
        /// <param name="projectPath">Root directory of the project for storing citation data</param>
        public CitationTracker(string projectPath = null)
        {
            this.projectPath = projectPath ?? Directory.GetCurrentDirectory();
            this.citationsFile = Path.Combine(this.projectPath, "citations.json");
            this.citations = LoadCitations();
        }

        /// <summary>
        /// Load existing citations from the citations file.
        /// </summary>
        /// <returns>Loaded citation data</returns>
        private CitationData LoadCitations()
        {
            if (File.Exists(citationsFile))
            {
                try
                {
                    string jsonString = File.ReadAllText(citationsFile);
                    return JsonSerializer.Deserialize<CitationData>(jsonString) ?? CreateNewCitationData();
                }
                catch (JsonException)
                {
                    Console.WriteLine("Warning: Citations file corrupted, creating new one.");
                }
            }

            return CreateNewCitationData();
        }

        /// <summary>
        /// Create a new citations data structure.
        /// </summary>
        /// <returns>New citation data object</returns>
        private CitationData CreateNewCitationData()
        {
            return new CitationData
            {
                ProjectInfo = new ProjectInfo
                {
                    Name = Path.GetFileName(projectPath),
                    CreatedAt = DateTime.Now
                },
                Sources = new Dictionary<string, SourceInfo>(),
                FileCitations = new Dictionary<string, List<Citation>>()
            };
        }

        /// <summary>
        /// Add a new citation source.
        /// </summary>
        /// <param name="sourceId">Unique identifier for the source</param>
        /// <param name="name">Name of the source (e.g., library name, article title)</param>
        /// <param name="url">URL to the source</param>
        /// <param name="author">Author of the source</param>
        /// <param name="licenseType">License type (e.g., MIT, Apache 2.0)</param>
        /// <param name="description">Brief description of the source</param>
        public void AddSource(string sourceId, string name, string url = null, 
            string author = null, string licenseType = null, string description = null)
        {
            citations.Sources[sourceId] = new SourceInfo
            {
                Name = name,
                Url = url,
                Author = author,
                LicenseType = licenseType,
                Description = description,
                AddedAt = DateTime.Now
            };
            
            SaveCitations();
        }

        /// <summary>
        /// Record that a file uses content from specified sources.
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <param name="sourceIds">Source ID or array of source IDs</param>
        /// <param name="lineStart">Optional starting line number</param>
        /// <param name="lineEnd">Optional ending line number</param>
        /// <param name="comment">Optional comment about how the source was used</param>
        public void CiteInFile(string filePath, string[] sourceIds, 
            int? lineStart = null, int? lineEnd = null, string comment = null)
        {
            string relPath = Path.GetRelativePath(projectPath, filePath);

            if (!citations.FileCitations.ContainsKey(relPath))
            {
                citations.FileCitations[relPath] = new List<Citation>();
            }

            var citation = new Citation
            {
                SourceIds = sourceIds,
                CitedAt = DateTime.Now,
                LineStart = lineStart,
                LineEnd = lineEnd,
                Comment = comment
            };

            citations.FileCitations[relPath].Add(citation);
            SaveCitations();
        }

        /// <summary>
        /// Record that a file uses content from a specified source.
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <param name="sourceId">Source ID</param>
        /// <param name="lineStart">Optional starting line number</param>
        /// <param name="lineEnd">Optional ending line number</param>
        /// <param name="comment">Optional comment about how the source was used</param>
        public void CiteInFile(string filePath, string sourceId,
            int? lineStart = null, int? lineEnd = null, string comment = null)
        {
            CiteInFile(filePath, new[] { sourceId }, lineStart, lineEnd, comment);
        }

        /// <summary>
        /// Generate a code comment for attribution.
        /// </summary>
        /// <param name="sourceId">Source ID to generate comment for</param>
        /// <returns>Formatted comment string for attribution</returns>
        public string GenerateAttributionComment(string sourceId)
        {
            if (!citations.Sources.ContainsKey(sourceId))
            {
                return $"// Attribution: Unknown source ({sourceId})";
            }

            var source = citations.Sources[sourceId];
            string comment = $"// Attribution: {source.Name}";

            if (!string.IsNullOrEmpty(source.Author))
                comment += $", by {source.Author}";
            if (!string.IsNullOrEmpty(source.Url))
                comment += $"\n// Source: {source.Url}";
            if (!string.IsNullOrEmpty(source.LicenseType))
                comment += $"\n// License: {source.LicenseType}";

            return comment;
        }

        /// <summary>
        /// Generate an attribution header for a file.
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <returns>Formatted header with all attributions for the file</returns>
        public string InsertAttributionHeader(string filePath)
        {
            string relPath = Path.GetRelativePath(projectPath, filePath);
            if (!citations.FileCitations.ContainsKey(relPath))
            {
                return "// No recorded attributions for this file.";
            }

            var fileCitations = citations.FileCitations[relPath];
            var citedSources = new HashSet<string>();
            foreach (var citation in fileCitations)
            {
                foreach (var sourceId in citation.SourceIds)
                {
                    citedSources.Add(sourceId);
                }
            }

            var header = new List<string>
            {
                "/*",
                " * This file contains code derived from or inspired by the following sources:",
                " *"
            };

            foreach (var sourceId in citedSources)
            {
                if (citations.Sources.ContainsKey(sourceId))
                {
                    var source = citations.Sources[sourceId];
                    header.Add($" * - {source.Name}" + (!string.IsNullOrEmpty(source.Url) ? $" ({source.Url})" : ""));
                    if (!string.IsNullOrEmpty(source.Author))
                        header.Add($" *   Author: {source.Author}");
                    if (!string.IsNullOrEmpty(source.LicenseType))
                        header.Add($" *   License: {source.LicenseType}");
                    header.Add(" *");
                }
            }

            header.Add(" */");
            return string.Join("\n", header);
        }

        /// <summary>
        /// Generate a Markdown document with all citations.
        /// </summary>
        /// <returns>Markdown formatted citations document</returns>
        public string GenerateCitationsMarkdown()
        {
            var md = new List<string>
            {
                "# Project Citations",
                "",
                "This document lists all external sources used in this project."
            };

            if (citations.Sources.Count == 0)
            {
                md.Add("\n*No citations recorded.*");
                return string.Join("\n", md);
            }

            md.Add("\n## Sources\n");

            foreach (var source in citations.Sources)
            {
                md.Add($"### {source.Value.Name}");
                if (!string.IsNullOrEmpty(source.Value.Author))
                    md.Add($"**Author:** {source.Value.Author}");
                if (!string.IsNullOrEmpty(source.Value.Url))
                    md.Add($"**URL:** [{source.Value.Url}]({source.Value.Url})");
                if (!string.IsNullOrEmpty(source.Value.LicenseType))
                    md.Add($"**License:** {source.Value.LicenseType}");
                if (!string.IsNullOrEmpty(source.Value.Description))
                    md.Add($"\n{source.Value.Description}");
                md.Add("");
            }

            md.Add("## Usage by File\n");

            foreach (var fileCitation in citations.FileCitations)
            {
                md.Add($"### {fileCitation.Key}");
                foreach (var citation in fileCitation.Value)
                {
                    var sourceNames = new List<string>();
                    foreach (var sourceId in citation.SourceIds)
                    {
                        if (citations.Sources.TryGetValue(sourceId, out var source))
                            sourceNames.Add(source.Name);
                        else
                            sourceNames.Add($"Unknown ({sourceId})");
                    }

                    string locations = "";
                    if (citation.LineStart.HasValue && citation.LineEnd.HasValue)
                        locations = $" (lines {citation.LineStart}-{citation.LineEnd})";
                    else if (citation.LineStart.HasValue)
                        locations = $" (from line {citation.LineStart})";

                    md.Add($"- Uses: {string.Join(", ", sourceNames)}{locations}");
                    if (!string.IsNullOrEmpty(citation.Comment))
                        md.Add($"  - Note: {citation.Comment}");
                }
                md.Add("");
            }

            return string.Join("\n", md);
        }

        /// <summary>
        /// Export citations to a markdown file.
        /// </summary>
        /// <param name="outputPath">Optional path for the markdown file</param>
        /// <returns>Path to the created markdown file</returns>
        public string ExportCitationsMarkdown(string outputPath = null)
        {
            outputPath ??= Path.Combine(projectPath, "CITATIONS.md");
            string mdContent = GenerateCitationsMarkdown();
            File.WriteAllText(outputPath, mdContent);
            return outputPath;
        }

        /// <summary>
        /// Save citations to the citations file.
        /// </summary>
        private void SaveCitations()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(citations, options);
            File.WriteAllText(citationsFile, jsonString);
        }
    }

    #region Data Classes
    public class CitationData
    {
        [JsonPropertyName("project_info")]
        public ProjectInfo ProjectInfo { get; set; }

        [JsonPropertyName("sources")]
        public Dictionary<string, SourceInfo> Sources { get; set; }

        [JsonPropertyName("file_citations")]
        public Dictionary<string, List<Citation>> FileCitations { get; set; }
    }

    public class ProjectInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
    }

    public class SourceInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("license_type")]
        public string LicenseType { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("added_at")]
        public DateTime AddedAt { get; set; }
    }

    public class Citation
    {
        [JsonPropertyName("source_ids")]
        public string[] SourceIds { get; set; }

        [JsonPropertyName("cited_at")]
        public DateTime CitedAt { get; set; }

        [JsonPropertyName("line_start")]
        public int? LineStart { get; set; }

        [JsonPropertyName("line_end")]
        public int? LineEnd { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }
    }
    #endregion
}
