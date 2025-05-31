**T.C. Adalet Bakanlığı UyapMevzuat API İncelemesi ve Veri Çekme Yapısı**

---

## ✨ ANA KATEGORİLER
- **1. Mevzuat**
- **2. İçtihat**

---

## Ⅰ MEVZUAT KATEGORİSİ

### 🔗 1. Mevzuat Türlerini Listeleme
- **URL**: `POST https://bedesten.adalet.gov.tr/mevzuat/mevzuatTypes`
- **Payload**:
```json
{ "applicationName": "UyapMevzuat" }
```
- **Response**: Mevzuat türleri listesi: `mevzuatTurId`, `mevzuatTur`, `mevzuatTurAdi`, `count`, `lastOperationDate` gibi alanlar

### 🔗 2. Mevzuat Dokümanlarını Listeleme
- **URL**: `POST https://bedesten.adalet.gov.tr/mevzuat/searchDocuments`
- **Payload**:
```json
{
  "data": {
    "pageSize": 20,
    "pageNumber": 1,
    "mevzuatTurList": [ "KANUN" ],
    "sortFields": ["RESMI_GAZETE_TARIHI"],
    "sortDirection": "desc"
  },
  "applicationName": "UyapMevzuat",
  "paging": true
}
```
- **Response**: `mevzuatId`, `mevzuatAdi`, `mevzuatNo`, `resmiGazeteTarihi`, `url`, `mevzuatTur`, vb.

### 🔗 3. Mevzuat Detay (Base64 HTML)
- **URL**: `POST https://bedesten.adalet.gov.tr/mevzuat/getDocumentContent`
- **Payload**:
```json
{
  "data": {
    "documentType": "MEVZUAT",
    "id": "341567"
  },
  "applicationName": "UyapMevzuat"
}
```
- **Response**: `content` (base64 encoded HTML), `mimeType`, `version`

---

## Ⅱ İÇTİHAT KATEGORİSİ

### 🔗 1. İçtihat Türlerini Listeleme
- **URL**: `POST https://bedesten.adalet.gov.tr/emsal-karar/getItemTypes`
- **Payload**:
```json
{ "applicationName": "UyapMevzuat" }
```
- **Response**: `name`, `description`, `count`

### 🔗 2. Birim Listeleme (Yargıtay)
- **URL**: `POST https://bedesten.adalet.gov.tr/emsal-karar/getBirimler`
- **Payload**:
```json
{
  "data": { "itemType": "YARGITAYKARARI" },
  "applicationName": "UyapMevzuat"
}
```

### 🔗 3. Birim Listeleme (Danıştay)
- **URL**: `POST https://bedesten.adalet.gov.tr/emsal-karar/getBirimler`
- **Payload**:
```json
{
  "data": { "itemType": "DANISTAYKARAR" },
  "applicationName": "UyapMevzuat"
}
```

### 🔗 4. Doküman Listeleme (Yargıtay/Danıştay)
- **URL**: `POST https://bedesten.adalet.gov.tr/emsal-karar/searchDocuments`
- **Payload (Yargıtay örneği)**:
```json
{
  "data": {
    "pageSize": 1,
    "pageNumber": 1,
    "itemTypeList": ["YARGITAYKARARI"],
    "birimAdi": "Hukuk Genel Kurulu",
    "sortFields": ["KARAR_TARIHI"],
    "sortDirection": "desc"
  },
  "applicationName": "UyapMevzuat",
  "paging": true
}
```
- **Payload (Danıştay örneği)**:
```json
{
  "data": {
    "pageSize": 1,
    "pageNumber": 1,
    "itemTypeList": ["DANISTAYKARAR"],
    "birimAdi": "İdare Dava Daireleri Kurulu",
    "sortFields": ["KARAR_TARIHI"],
    "sortDirection": "desc"
  },
  "applicationName": "UyapMevzuat",
  "paging": true
}
```

### 🔗 5. Tarih Aralığı ile Doküman Listeleme (Diğer İçtihatlar)
- **Payload örneği**:
```json
{
  "data": {
    "pageSize": 20,
    "pageNumber": 1,
    "itemTypeList": ["YERELHUKUK"],
    "kararTarihiStart": "1989-01-02T01:00:00.000Z",
    "kararTarihiEnd": "2025-04-23T00:00:00.000Z",
    "sortFields": ["KARAR_TARIHI"],
    "sortDirection": "desc"
  },
  "applicationName": "UyapMevzuat",
  "paging": true
}
```

### 🔗 6. Tekil İçtihat Belgesi Çekme
- **URL**: `POST https://bedesten.adalet.gov.tr/emsal-karar/getDocumentContent`
- **Payload**:
```json
{
  "data": {
    "documentId": "1131920200"
  },
  "applicationName": "UyapMevzuat"
}
```
- **Response**: `content`, `mimeType`, `version`

---

Bu belge, T.C. Adalet Bakanlığı UyapMevzuat portalından veri çekmek isteyen geliştiriciler için endpoint yapılarını, istek ve yanıt örneklerini ayrıntılı şekilde sunar.

