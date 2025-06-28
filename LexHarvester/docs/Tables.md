## LegislationTypes
- Mevzuat belge türlerini saklar
- 

``` sql
SELECT TOP (1000) [Id] ,[LegislationTypeId] ,[LegislationTypeCode] ,[LegislationTypeTitle] ,[OrderNumber],[LastOperationDate] ,[Count] FROM [LexHarvesterDb].[dbo].[LegislationTypes]
```
| Id  | LegislationTypeId | LegislationTypeCode        | LegislationTypeTitle | OrderNumber | LastOperationDate      | Count |
|-----|-------------------|----------------------------|----------------------|-------------|------------------------|-------|
| 1   | 19                | KANUN                      | Kanunlar             | 1           | 2025-06-28 10:00:00    | 912   |


### 📄 HarvestingState Tablosu

**Açıklama:**  
Bu tablo, döküman hasatlama (harvesting) sürecinde sistemin en son hangi sayfada kaldığını ve toplam kaç sayfa veri okuduğunu takip eder.  
- `DocumentType`: Mevzuat mı İçtihat mı olduğunu belirtir (enum).  
- `SubType`: Kanun, Yönetmelik gibi türler. İlgili referans tablodan okunur.  
- `CurrentPage`: Sistem şu an kaçıncı sayfada kaldı.  
- `TotalPage`: Sistem kaç sayfalık veri bekliyor.  
- Eğer `TotalPage`, subtype için `Count` ile eşleşmezse, sistem yeni dökümanlar geldiğini varsayarak tüm verileri baştan çeker.  
- `LastErrorMessage` alanı son başarısız isteğin hata mesajını tutar.  
- `IsCompleted` true ise subtype için çekme tamamlanmıştır.  
- `LastUpdated` ve `CreatedAt` zaman damgalarıdır.

---

### 📊 HarvestingState Tablosu (Örnek Satır)

| Id | DocumentType | SubType    | CurrentPage | TotalPage | IsCompleted | LastUpdated           | LastErrorMessage | CreatedAt             |
|----|--------------|------------|-------------|-----------|-------------|------------------------|------------------|------------------------|
| 1  | Legislation  | Kanun      | 12          | 50        | false       | 2025-06-28T08:45:00Z   | null             | 2025-06-27T16:20:00Z   |
