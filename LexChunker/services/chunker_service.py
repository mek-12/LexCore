import base64
from models.requests.request_model import ChunkRequest
from models.responses.chunk_model import Chunk
from services import html_parser, pdf_parser
from utils.tokenizer import count_tokens

def chunk_document(request: ChunkRequest):
    decoded_bytes = base64.b64decode(request.fileBytes)
    text = ""

    if request.fileType.lower() == "html":
        text = html_parser.parse_html(decoded_bytes)
    elif request.fileType.lower() == "pdf":
        text = pdf_parser.parse_pdf(decoded_bytes)
    else:
        raise ValueError("Unsupported file type")

    paragraphs = [p.strip() for p in text.split("\n") if p.strip()]

    chunks = []
    for i, para in enumerate(paragraphs):
        chunks.append(Chunk(
            chunkIndex=i,
            text=para,
            tokenCount=count_tokens(para)
        ))

    return {
        "documentId": request.documentId,
        "chunks": chunks
    }
