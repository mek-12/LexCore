EF Core Code First Migration:

Yeni bir ef core model eklediğimizde veya modifiye ettiğimizde:
1) önce migration dosyasını oluşturuyoruz localde 
    * dotnet ef migrations add <Migration_Name> --project ./src/LexHarvester.Persistence/LexHarvester.Persistence.csproj
2) 'docker compose up --build' komutu ile tekrar container ı ayağa kaldırıyoruz.

Not: Migration işlemini yaptıktan sonra ileride güvenlik problemi olmasın diye ApplicationDbContextFactory de ki ConnectionString i silelim.
