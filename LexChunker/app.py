from fastapi import FastAPI
from routers import chunker

app = FastAPI(
    title="LexChunker",
    version="0.1.0",
    description="A service for chunking text into manageable pieces.",
)

app.include_router(chunker.router)

if __name__ == "__main__":
    import uvicorn
    uvicorn.run("app:app", host="0.0.0.0", port=8000, reload=True)