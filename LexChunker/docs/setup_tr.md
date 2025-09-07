LexChunker Kurulum ve Çalıştırma Rehberi
========================================

1. GEREKSİNİMLER
----------------
- Python 3.8 veya üzeri
- pip (Python package manager)
- Git (isteğe bağlı)
- Linux / MacOS / WSL ortamı önerilir

2. PROJE KLASÖRÜNE GİRİŞ
------------------------
cd lexchunker

# Açıklama:
# Proje dosyalarının bulunduğu klasöre girilir.

3. SANAL PYTHON ORTAMI OLUŞTURMA
--------------------------------
python3 -m venv venv

# Açıklama:
# Projeye özel bir Python ortamı oluşturur. Böylece tüm bağımlılıklar bu klasörde izole tutulur.
# venv adında bir klasör oluşur, sistem genelini etkilemez.

source venv/bin/activate
# (Windows'ta çalışıyorsan: venv\Scripts\activate)

# Açıklama:
# Oluşturduğun sanal ortamı aktif hale getirir. Komut satırında (venv) olarak görünür.

4. GEREKLİ PAKETLERİ YÜKLEME
----------------------------
pip install -r requirements.txt

# Açıklama:
# Projede tanımlı tüm Python bağımlılıklarını yükler.
# Bu dosyada örneğin şunlar bulunabilir:
# fastapi, uvicorn, beautifulsoup4, pdfplumber, pydantic[dotenv]

Eğer `requirements.txt` yoksa şu komutla elle yükleyebilirsin:

pip install fastapi uvicorn beautifulsoup4 pdfplumber pydantic[dotenv]

5. PROJEYİ BAŞLATMA
-------------------
uvicorn app:app --reload

# Açıklama:
# FastAPI uygulamasını çalıştırır.
# `app:app` → ilk 'app' = app.py dosyası, ikinci 'app' = FastAPI() nesnesi
# `--reload` → kodda değişiklik yaptığında sunucu otomatik yeniden başlar

Sunucu başarılı şekilde başladıysa şu adrese git:
5. 1. BAĞIMLILIKLARI requirements.txt DOSYASINA KAYDET (Opsiyonel)
-----------------------------------------------------------------
pip freeze > requirements.txt

# Açıklama:
# Bu komut, sanal ortamda kurulu tüm paketleri ve versiyonlarını `requirements.txt` dosyasına yazar.
# Projeyi başkalarıyla paylaşırken veya production ortamına aktarırken büyük kolaylık sağlar.

http://localhost:8000/docs

# Açıklama:
# FastAPI'nin otomatik olarak oluşturduğu Swagger UI'dır.
# API endpoint’lerini buradan test edebilirsin.

6. TEST
-------
- `/chunk` endpoint’ine `POST` isteği göndererek test yapabilirsin.
- Giriş:
  - Base64 formatında HTML veya PDF dosyası
  - Document türü ("CaseLaw", "Legislation")
- Çıkış:
  - Belgeye ait anlamlı bölgelere ayrılmış `chunks` listesi

7. DİZİN YAPISI (AÇIKLAMALI)
----------------------------
<details> <summary>Click to view in plain code block style</summary>
<pre lang="plaintext"><code> ```plaintext lexchunker/ ├── venv/ → Virtual environment (isolated Python packages) ├── app.py → FastAPI application entry point ├── routers/ → API route definitions │ └── chunker.py → Defines the `/chunk` endpoint ├── services/ → Business logic (e.g., parser selection, chunk splitting) │ ├── chunker_service.py → Selects and runs appropriate parser (HTML or PDF) │ ├── html_parser.py → Extracts text from HTML documents │ └── pdf_parser.py → Extracts text from PDF documents ├── models/ → Data models (input/output for the API) │ ├── request_models.py → Pydantic model for chunking request │ └── chunk_model.py → Pydantic model for chunk output ├── utils/ → Helper functions (e.g., token counters) │ └── tokenizer.py → Simple token/word count function ├── requirements.txt → Python dependency list ├── setup.txt → Project setup instructions (this file) └── Dockerfile → Optional: for containerization ``` </code></pre>
</details>

8. NOTLAR
---------
- Bu servis sadece chunking yapar. Embedding ayrı bir servis tarafından yapılacaktır.
- LexHarvester uygulaması bu servise HTTP POST ile belge gönderebilir.
- Base64 dönüşümü .NET tarafında yapılmalıdır (byte[] → base64 string).