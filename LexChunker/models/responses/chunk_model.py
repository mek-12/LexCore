from pydantic import BaseModel
from typing import Optional

class Chunk(BaseModel):
    chunkIndex: int
    text: str
    tokenCount: int
    section: Optional[str] = None
