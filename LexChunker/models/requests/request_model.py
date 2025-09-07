from pydantic import BaseModel
from typing import Optional, Dict

class ChunkRequest(BaseModel):
    documentId: str
    documentType: str  # "CaseLaw" or "Legislation"
    fileType: str      # "html" or "pdf"
    fileBytes: str     # Base64 encoded content
    metadata: Optional[Dict[str, str]] = None