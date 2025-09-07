from fastapi import APIRouter
from models.requests.request_model import ChunkRequest
from services.chunker_service import chunk_document

router = APIRouter(
    prefix="/chunk",
    tags=["Chunker"]
)

@router.post("/")
def chunk(request: ChunkRequest):
    return chunk_document(request)