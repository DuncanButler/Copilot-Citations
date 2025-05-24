"""
Unit tests for the citation_tracker module.
"""
import os
import sys
import json
import tempfile
import unittest
from datetime import datetime

# Add parent directory to python path to import the module under test
sys.path.append(os.path.dirname(os.path.dirname(os.path.abspath(__file__))))
from citation_tracker import CitationTracker


class TestCitationTracker(unittest.TestCase):
    """Test cases for CitationTracker class."""

    def setUp(self):
        """Set up test fixtures."""
        # Create a temporary directory for the test
        self.test_dir = tempfile.TemporaryDirectory()
        self.project_path = self.test_dir.name
        self.tracker = CitationTracker(self.project_path)
        
    def tearDown(self):
        """Tear down test fixtures."""
        self.test_dir.cleanup()
        
    def test_add_source(self):
        """Test adding a source to the citation tracker."""
        self.tracker.add_source(
            source_id="test-source",
            name="Test Source",
            url="https://example.com",
            author="Test Author",
            license_type="MIT",
            description="Test description"
        )
        
        # Check that the source was added to the citations
        self.assertIn("test-source", self.tracker.citations["sources"])
        source = self.tracker.citations["sources"]["test-source"]
        self.assertEqual(source["name"], "Test Source")
        self.assertEqual(source["url"], "https://example.com")
        self.assertEqual(source["author"], "Test Author")
        self.assertEqual(source["license_type"], "MIT")
        self.assertEqual(source["description"], "Test description")
        
    def test_cite_in_file(self):
        """Test citing a source in a file."""
        # Add a test source
        self.tracker.add_source(
            source_id="test-source",
            name="Test Source"
        )
        
        # Cite the source in a file
        test_file_path = os.path.join(self.project_path, "test_file.py")
        with open(test_file_path, "w") as f:
            f.write("# Test file")
            
        self.tracker.cite_in_file(
            file_path=test_file_path,
            source_ids="test-source",
            line_start=1,
            line_end=10,
            comment="Test comment"
        )
        
        # Check that the citation was added
        rel_path = os.path.relpath(test_file_path, self.project_path)
        self.assertIn(rel_path, self.tracker.citations["file_citations"])
        
        citation = self.tracker.citations["file_citations"][rel_path][0]
        self.assertEqual(citation["source_ids"], ["test-source"])
        self.assertEqual(citation["line_start"], 1)
        self.assertEqual(citation["line_end"], 10)
        self.assertEqual(citation["comment"], "Test comment")
        
    def test_generate_attribution_comment(self):
        """Test generating an attribution comment."""
        self.tracker.add_source(
            source_id="test-source",
            name="Test Source",
            url="https://example.com",
            author="Test Author",
            license_type="MIT"
        )
        
        comment = self.tracker.generate_attribution_comment("test-source")
        self.assertIn("Test Source", comment)
        self.assertIn("Test Author", comment)
        self.assertIn("https://example.com", comment)
        self.assertIn("MIT", comment)
        
    def test_export_citations_markdown(self):
        """Test exporting citations to a markdown file."""
        # Add a source and cite it
        self.tracker.add_source(
            source_id="test-source",
            name="Test Source"
        )
        
        test_file_path = os.path.join(self.project_path, "test_file.py")
        with open(test_file_path, "w") as f:
            f.write("# Test file")
            
        self.tracker.cite_in_file(
            file_path=test_file_path,
            source_ids="test-source",
            line_start=1,
            line_end=10
        )
        
        # Export citations to markdown
        md_path = self.tracker.export_citations_markdown()
        
        # Check that the file was created and contains the source
        self.assertTrue(os.path.exists(md_path))
        with open(md_path, "r") as f:
            content = f.read()
            self.assertIn("Test Source", content)
            self.assertIn("test_file.py", content)


if __name__ == "__main__":
    unittest.main()
