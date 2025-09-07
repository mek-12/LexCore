Change Database Name:

use master
go
ALTER DATABASE [<OldDatabaseName>] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
go
ALTER DATABASE [<OldDatabaseName>] MODIFY NAME = [<NewDatabaseName>];
go
ALTER DATABASE [<NewDatabaseName>] SET MULTI_USER;