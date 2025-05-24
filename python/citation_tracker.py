"""
Citation Tracker for AI-Generated Content
This module provides utilities to track and properly attribute sources when using
AI-assisted code generation tools like GitHub Copilot.
"""
import json
import os
from typing import Dict, List, Optional, Union
from datetime import datetime


class CitationTracker:
    """A class for tracking citations in AI-generated content."""
    
    def __init__(self, project_path: str = None):
        """
        Initialize the citation tracker.
        
        Args:
            project_path: Root directory of the project for storing citation data
        """
        self.project_path = project_path or os.getcwd()
        self.citations_file = os.path.join(self.project_path, 'citations.json')
        self.citations = self._load_citations()
        
    def _load_citations(self) -> Dict:
        """Load existing citations from the citations file."""
        if os.path.exists(self.citations_file):
            try:
                with open(self.citations_file, 'r', encoding='utf-8') as f:
                    return json.load(f)
            except json.JSONDecodeError:
                print(f"Warning: Citations file corrupted, creating new one.")
        
        # Initialize a new citations structure
        return {
            "project_info": {
                "name": os.path.basename(self.project_path),
                "created_at": datetime.now().isoformat()
            },
            "sources": {},
            "file_citations": {}
        }
    
    def add_source(self, 
                  source_id: str, 
                  name: str, 
                  url: Optional[str] = None, 
                  author: Optional[str] = None,
                  license_type: Optional[str] = None,
                  description: Optional[str] = None) -> None:
        """
        Add a new citation source.
        
        Args:
            source_id: Unique identifier for the source
            name: Name of the source (e.g., library name, article title)
            url: URL to the source
            author: Author of the source
            license_type: License type (e.g., MIT, Apache 2.0)
            description: Brief description of the source
        """
        self.citations["sources"][source_id] = {
            "name": name,
            "url": url,
            "author": author,
            "license_type": license_type,
            "description": description,
            "added_at": datetime.now().isoformat()
        }
        self._save_citations()
        
    def cite_in_file(self, file_path: str, source_ids: Union[str, List[str]], 
                    line_start: Optional[int] = None, 
                    line_end: Optional[int] = None,
                    comment: Optional[str] = None) -> None:
        """
        Record that a file uses content from specified sources.
        
        Args:
            file_path: Path to the file
            source_ids: Source ID or list of source IDs
            line_start: Optional starting line number
            line_end: Optional ending line number
            comment: Optional comment about how the source was used
        """
        rel_path = os.path.relpath(file_path, self.project_path)
        
        if isinstance(source_ids, str):
            source_ids = [source_ids]
            
        if rel_path not in self.citations["file_citations"]:
            self.citations["file_citations"][rel_path] = []
            
        citation = {
            "source_ids": source_ids,
            "cited_at": datetime.now().isoformat()
        }
        
        if line_start is not None:
            citation["line_start"] = line_start
        if line_end is not None:
            citation["line_end"] = line_end
        if comment:
            citation["comment"] = comment
            
        self.citations["file_citations"][rel_path].append(citation)
        self._save_citations()
        
    def generate_attribution_comment(self, source_id: str) -> str:
        """
        Generate a code comment for attribution.
        
        Args:
            source_id: Source ID to generate comment for
            
        Returns:
            Formatted comment string for attribution
        """
        if source_id not in self.citations["sources"]:
            return f"# Attribution: Unknown source ({source_id})"
            
        source = self.citations["sources"][source_id]
        comment = f"# Attribution: {source['name']}"
        
        if source.get("author"):
            comment += f", by {source['author']}"
        if source.get("url"):
            comment += f"\n# Source: {source['url']}"
        if source.get("license_type"):
            comment += f"\n# License: {source['license_type']}"
            
        return comment
        
    def insert_attribution_header(self, file_path: str) -> str:
        """
        Generate an attribution header for a file.
        
        Args:
            file_path: Path to the file
            
        Returns:
            Formatted header with all attributions for the file
        """
        rel_path = os.path.relpath(file_path, self.project_path)
        if rel_path not in self.citations["file_citations"]:
            return "# No recorded attributions for this file."
            
        file_citations = self.citations["file_citations"][rel_path]
        cited_sources = set()
        for citation in file_citations:
            cited_sources.update(citation["source_ids"])
            
        header = [
            "\"\"\"",
            "This file contains code derived from or inspired by the following sources:",
            ""
        ]
        
        for source_id in cited_sources:
            if source_id in self.citations["sources"]:
                source = self.citations["sources"][source_id]
                header.append(f"- {source['name']}")
                if source.get("url"):
                    header[-1] += f" ({source['url']})"
                if source.get("author"):
                    header.append(f"  Author: {source['author']}")
                if source.get("license_type"):
                    header.append(f"  License: {source['license_type']}")
                header.append("")
        
        header.append("\"\"\"")
        return "\n".join(header)
    
    def generate_citations_markdown(self) -> str:
        """
        Generate a Markdown document with all citations.
        
        Returns:
            Markdown formatted citations document
        """
        md = ["# Project Citations", "", "This document lists all external sources used in this project."]
        
        if not self.citations["sources"]:
            md.append("\n*No citations recorded.*")
            return "\n".join(md)
            
        md.append("\n## Sources\n")
        
        for source_id, source in self.citations["sources"].items():
            md.append(f"### {source['name']}")
            if source.get("author"):
                md.append(f"**Author:** {source['author']}")
            if source.get("url"):
                md.append(f"**URL:** [{source['url']}]({source['url']})")
            if source.get("license_type"):
                md.append(f"**License:** {source['license_type']}")
            if source.get("description"):
                md.append(f"\n{source['description']}")
            md.append("")
            
        md.append("## Usage by File\n")
        
        for file_path, citations in self.citations["file_citations"].items():
            md.append(f"### {file_path}")
            for citation in citations:
                source_names = []
                for source_id in citation["source_ids"]:
                    source = self.citations["sources"].get(source_id, {"name": f"Unknown ({source_id})"})
                    source_names.append(source["name"])
                    
                locations = ""
                if "line_start" in citation and "line_end" in citation:
                    locations = f" (lines {citation['line_start']}-{citation['line_end']})"
                elif "line_start" in citation:
                    locations = f" (from line {citation['line_start']})"
                    
                md.append(f"- Uses: {', '.join(source_names)}{locations}")
                if citation.get("comment"):
                    md.append(f"  - Note: {citation['comment']}")
            md.append("")
            
        return "\n".join(md)
        
    def export_citations_markdown(self, output_path: Optional[str] = None) -> str:
        """
        Export citations to a markdown file.
        
        Args:
            output_path: Optional path for the markdown file
            
        Returns:
            Path to the created markdown file
        """
        if not output_path:
            output_path = os.path.join(self.project_path, "CITATIONS.md")
            
        md_content = self.generate_citations_markdown()
        
        with open(output_path, 'w', encoding='utf-8') as f:
            f.write(md_content)
            
        return output_path
    
    def _save_citations(self) -> None:
        """Save citations to the citations file."""
        with open(self.citations_file, 'w', encoding='utf-8') as f:
            json.dump(self.citations, f, indent=2)
