LexChunker – Installation and Run Guide
=======================================

1. REQUIREMENTS
---------------
- Python 3.8+
- pip (Python package manager)
- Git (optional)
- Linux / macOS / WSL is recommended

2. NAVIGATE TO THE PROJECT FOLDER
---------------------------------
cd lexchunker

# Explanation:
# Move into the root folder of the project.

3. CREATE A PYTHON VIRTUAL ENVIRONMENT
--------------------------------------
python3 -m venv venv

# Explanation:
# Creates a virtual environment named `venv` to isolate dependencies from the global Python installation.

source venv/bin/activate
# On Windows: venv\Scripts\activate

# Explanation:
# Activates the virtual environment. You’ll see (venv) at the beginning of your terminal prompt.

4. INSTALL DEPENDENCIES
-----------------------
pip install -r requirements.txt

# Explanation:
# Installs all required Python packages listed in requirements.txt

If `requirements.txt` is not available yet, you can install dependencies manually:

pip install fastapi uvicorn beautifulsoup4 pdfplumber pydantic[dotenv]

5. RUN THE PROJECT
------------------
uvicorn app:app --reload

# Explanation:
# Starts the FastAPI server.
# `app:app` means → first "app" is the file (app.py), second "app" is the FastAPI() instance.
# `--reload` enables auto-reload when code changes.

After running the server, visit:

http://localhost:8000/docs

# Explanation:
# Opens Swagger UI where you can interactively test the API endpoints.
5. 1. SAVE DEPENDENCIES TO requirements.txt (Optional)
------------------------------------------------------
pip freeze > requirements.txt

# Explanation:
# This command saves the currently installed packages in your virtual environment to a `requirements.txt` file.
# This is useful for tracking exact versions and sharing the project with others.
6. TESTING
----------
- Send a POST request to `/chunk`
- Input:
  - Base64-encoded content of an HTML or PDF file
  - `documentType`: "CaseLaw" or "Legislation"
- Output:
  - A list of meaningful text chunks extracted from the document

7. PROJECT STRUCTURE (EXPLAINED)
--------------------------------
<details> <summary>Click to view in plain code block style</summary>
<pre lang="plaintext"><code> ```plaintext lexchunker/ ├── venv/ → Virtual environment (isolated Python packages) ├── app.py → FastAPI application entry point ├── routers/ → API route definitions │ └── chunker.py → Defines the `/chunk` endpoint ├── services/ → Business logic (e.g., parser selection, chunk splitting) │ ├── chunker_service.py → Selects and runs appropriate parser (HTML or PDF) │ ├── html_parser.py → Extracts text from HTML documents │ └── pdf_parser.py → Extracts text from PDF documents ├── models/ → Data models (input/output for the API) │ ├── request_models.py → Pydantic model for chunking request │ └── chunk_model.py → Pydantic model for chunk output ├── utils/ → Helper functions (e.g., token counters) │ └── tokenizer.py → Simple token/word count function ├── requirements.txt → Python dependency list ├── setup.txt → Project setup instructions (this file) └── Dockerfile → Optional: for containerization ``` </code></pre>
</details>
8. NOTES
--------
- This service only performs chunking. Embedding will be handled by a separate service.
- The LexHarvester (.NET) system should send documents to this service via HTTP POST.
- Files should be sent as Base64-encoded strings (converted from byte arrays).