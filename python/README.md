# Python Citation Tracker

This folder contains the Python implementation of the Citation Tracker for AI-generated code.

## Components

- `citation_tracker.py`: Core module that provides citation tracking functionality
- `citation_example.py`: Example demonstrating how to use the citation tracker
- `vscode_citation_extension.py`: Conceptual implementation of a VS Code extension

## Installation

1. Copy `citation_tracker.py` to your project
2. Import the module in your code:
```python
from citation_tracker import CitationTracker
```

## Basic Usage

Initialize the tracker:
```python
tracker = CitationTracker()
```

Add a source:
```python
tracker.add_source(
    source_id="github-repo-example",
    name="Example Algorithm Repository",
    url="https://github.com/example/algorithm-repo",
    author="Example Author",
    license_type="MIT",
    description="Contains implementation of sorting algorithms"
)
```

Cite a source in a file:
```python
tracker.cite_in_file(
    file_path="sorting.py", 
    source_ids=["github-repo-example"],
    line_start=10, 
    line_end=50,
    comment="QuickSort implementation adapted from repository"
)
```

Generate citations document:
```python
tracker.export_citations_markdown()
```

## Integration with VS Code

The `vscode_citation_extension.py` file provides a conceptual implementation of how citation tracking could be integrated into VS Code as an extension. This is not a functional extension but demonstrates the concepts.

## Git Integration

A pre-commit git hook is available in the `tools` directory. To use it:

1. Copy `../tools/citation-git-hook.py` to your project's `.git/hooks/pre-commit`
2. Make it executable: `chmod +x .git/hooks/pre-commit`

This will automatically update your `CITATIONS.md` file before each commit.
