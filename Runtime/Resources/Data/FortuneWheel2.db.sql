BEGIN TRANSACTION;
DROP TABLE IF EXISTS "Ativacao";
CREATE TABLE IF NOT EXISTS "Ativacao" (
	"id"	INTEGER NOT NULL,
	"nome"	TEXT,
	"dataIni"	TEXT,
	"dataFim"	TEXT,
	"atual"	INTEGER DEFAULT 0,
	"createdAt"	DATETIME DEFAULT CURRENT_TIMESTAMP,
	"updatedAt"	DATETIME DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY("id" AUTOINCREMENT)
);
DROP TABLE IF EXISTS "AppConfig";
CREATE TABLE IF NOT EXISTS "AppConfig" (
	"key"	TEXT,
	"value"	TEXT,
	"type"	TEXT,
	"createdAt"	DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	"updatedAt"	DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);
DROP TABLE IF EXISTS "Lead";
CREATE TABLE IF NOT EXISTS "Lead" (
	"id"	INTEGER NOT NULL,
	"cpf"	varchar NOT NULL,
	"name"	varchar,
	"fone"	varchar,
	"email"	varchar,
	"data_nasc"	TEXT,
	"complianceAgree"	integer,
	"createdAt"	DATETIME,
	"updatedAt"	DATETIME,
	PRIMARY KEY("id" AUTOINCREMENT)
);
DROP TABLE IF EXISTS "LeadSorteio";
CREATE TABLE IF NOT EXISTS "LeadSorteio" (
	"id"	INTEGER NOT NULL,
	"lead_id"	INTEGER NOT NULL,
	"premio"	INTEGER,
	"ativacao"	INTEGER,
	"createdAt"	DATETIME,
	"updatedAt"	DATETIME,
	PRIMARY KEY("id" AUTOINCREMENT)
);
DROP TABLE IF EXISTS "Premio";
CREATE TABLE IF NOT EXISTS "Premio" (
	"id"	INTEGER NOT NULL,
	"seq"	INTEGER NOT NULL UNIQUE,
	"nome"	TEXT,
	"descricao"	TEXT,
	"qtde"	NUMERIC,
	"categoria"	INTEGER,
	"createdAt"	DATETIME,
	"updatedAt"	DATETIME,
	FOREIGN KEY("categoria") REFERENCES "Categoria"("id") ON DELETE NO ACTION,
	PRIMARY KEY("id" AUTOINCREMENT)
);
DROP TABLE IF EXISTS "Categoria";
CREATE TABLE IF NOT EXISTS "Categoria" (
	"id"	INTEGER NOT NULL,
	"seq"	INTEGER NOT NULL UNIQUE,
	"nome"	TEXT NOT NULL UNIQUE,
	"probabilidade"	INTEGER,
	"qtde"	INTEGER DEFAULT 0,
	"qtdeSorteio"	INTEGER DEFAULT 0,
	"createdAt"	DATETIME,
	"updatedAt"	DATETIME,
	"logoPath"	TEXT DEFAULT '/Resources/Screens/Roleta/LogoBB.png',
	"parabensPath"	TEXT DEFAULT '/Resources/Screens/Final/1PatabensBB.png',
	PRIMARY KEY("id")
);
INSERT INTO "Ativacao" ("id","nome","dataIni","dataFim","atual","createdAt","updatedAt") VALUES (1,'Expointer 2023','2023-08-01','2023-09-03',0,0,0);
INSERT INTO "Ativacao" ("id","nome","dataIni","dataFim","atual","createdAt","updatedAt") VALUES (2,'Circuito de Surfe','2023-09-14','2023-09-17',1,0,0);
INSERT INTO "AppConfig" ("key","value","type","createdAt","updatedAt") VALUES ('QtdeArraySorteio','100','int',0,0);
INSERT INTO "Premio" ("id","seq","nome","descricao","qtde","categoria","createdAt","updatedAt") VALUES (1,1,'Premio nome 0','Premio Desc 0',100,1,'2023-10-17 23:15:07','2023-11-06 15:27:20');
INSERT INTO "Premio" ("id","seq","nome","descricao","qtde","categoria","createdAt","updatedAt") VALUES (2,2,'Premio nome 1','Premio Desc 1',100,2,'2023-10-17 23:23:58','2023-11-06 15:27:25');
INSERT INTO "Premio" ("id","seq","nome","descricao","qtde","categoria","createdAt","updatedAt") VALUES (3,3,'Premio nome 2','Premio Desc 2',100,3,'2023-10-17 23:24:01','2023-11-06 15:27:25');
INSERT INTO "Premio" ("id","seq","nome","descricao","qtde","categoria","createdAt","updatedAt") VALUES (4,4,'Premio nome 3','Premio Desc 3',100,4,'2023-10-31 21:14:46','2023-11-06 15:27:25');
INSERT INTO "Premio" ("id","seq","nome","descricao","qtde","categoria","createdAt","updatedAt") VALUES (5,5,'Premio nome 4','Premio Desc 4',100,2,'2023-10-31 21:42:51','2023-11-06 15:27:25');
INSERT INTO "Premio" ("id","seq","nome","descricao","qtde","categoria","createdAt","updatedAt") VALUES (6,6,'Premio nome 5','Premio Desc 5',100,3,'2023-10-31 22:01:48','2023-11-06 15:27:25');
INSERT INTO "Premio" ("id","seq","nome","descricao","qtde","categoria","createdAt","updatedAt") VALUES (7,7,'Premio nome 6','Premio Desc 6',100,4,'2023-10-31 22:01:50','2023-11-06 15:27:25');
INSERT INTO "Premio" ("id","seq","nome","descricao","qtde","categoria","createdAt","updatedAt") VALUES (8,8,'Premio nome 7','Premio Desc 7',100,2,'2023-10-31 22:01:53','2023-11-06 15:27:25');
INSERT INTO "Premio" ("id","seq","nome","descricao","qtde","categoria","createdAt","updatedAt") VALUES (9,9,'Premio nome 7','Premio Desc 7',100,3,'2023-11-06 15:27:07','2023-11-06 15:28:16');
INSERT INTO "Premio" ("id","seq","nome","descricao","qtde","categoria","createdAt","updatedAt") VALUES (10,10,'Premio nome 7','Premio Desc 7',100,2,'2023-11-06 15:28:21','2023-11-06 15:28:38');
INSERT INTO "Categoria" ("id","seq","nome","probabilidade","qtde","qtdeSorteio","createdAt","updatedAt","logoPath","parabensPath") VALUES (1,1,'BB',5,100,0,'2023-11-06 15:25:13','2023-11-06 15:25:21','/Resources/Screens/Roleta/LogoBB.png','/Resources/Screens/Final/1PatabensBB.png');
INSERT INTO "Categoria" ("id","seq","nome","probabilidade","qtde","qtdeSorteio","createdAt","updatedAt","logoPath","parabensPath") VALUES (2,2,'Visa',40,100,0,'2023-11-06 15:25:17','2023-11-06 15:25:23','/Resources/Screens/Roleta/LogoVisa.png','/Resources/Screens/Final/2PatabensVisa.png');
INSERT INTO "Categoria" ("id","seq","nome","probabilidade","qtde","qtdeSorteio","createdAt","updatedAt","logoPath","parabensPath") VALUES (3,3,'Ourocard',20,100,0,'2023-11-06 15:25:17','2023-11-06 15:25:24','/Resources/Screens/Roleta/LogoOuroCard.png','/Resources/Screens/Final/3PatabensOuroCard.png');
INSERT INTO "Categoria" ("id","seq","nome","probabilidade","qtde","qtdeSorteio","createdAt","updatedAt","logoPath","parabensPath") VALUES (4,4,'IRancho',35,100,0,'2023-11-06 15:25:17','2023-11-06 15:25:32','/Resources/Screens/Roleta/LogoIRancho.png','/Resources/Screens/Final/4PatabensIRancho.png');

DROP TRIGGER IF EXISTS "After_INSERT_lead";
DROP TRIGGER IF EXISTS "Before_UPDATE_lead";
DROP TRIGGER IF EXISTS "After_UPDATE_lead";
--LEAD
CREATE TRIGGER After_INSERT_lead AFTER INSERT ON lead
    BEGIN
       UPDATE lead
		SET createdAt = datetime('now', 'localtime'),
		updatedAt = datetime('now', 'localtime')
		WHERE rowid = new.rowid;
    END;
CREATE TRIGGER Before_UPDATE_lead BEFORE UPDATE ON lead
    BEGIN
       UPDATE Lead
		SET updatedAt = new.createdAt
		WHERE rowid = new.rowid;
    END;
CREATE TRIGGER After_UPDATE_lead AFTER UPDATE ON lead
    BEGIN
       UPDATE Lead
		SET createdAt = updatedAt,
		updatedAt = datetime('now', 'localtime')
		WHERE rowid = new.rowid;
    END;

--LEADSORTEIO

DROP TRIGGER IF EXISTS "After_INSERT_LeadSorteio";
DROP TRIGGER IF EXISTS "Before_UPDATE_LeadSorteio";
DROP TRIGGER IF EXISTS "After_UPDATE_LeadSorteio";
CREATE TRIGGER After_INSERT_LeadSorteio AFTER INSERT ON LeadSorteio
    BEGIN
       UPDATE LeadSorteio
		SET createdAt = datetime('now', 'localtime'),
		updatedAt = datetime('now', 'localtime')
		WHERE rowid = new.rowid;
    END;
CREATE TRIGGER Before_UPDATE_LeadSorteio BEFORE UPDATE ON LeadSorteio
    BEGIN
       UPDATE LeadSorteio
		SET updatedAt = new.createdAt
		WHERE rowid = new.rowid;
    END;
CREATE TRIGGER After_UPDATE_LeadSorteio AFTER UPDATE ON LeadSorteio
    BEGIN
       UPDATE LeadSorteio
		SET createdAt = updatedAt,
		updatedAt = datetime('now', 'localtime')
		WHERE rowid = new.rowid;
    END;

--CATEGORIA
DROP TRIGGER IF EXISTS "After_INSERT_Categoria";
DROP TRIGGER IF EXISTS "After_UPDATE_Categoria";

CREATE TRIGGER After_INSERT_Categoria AFTER INSERT ON categoria
    BEGIN
       UPDATE categoria
		SET createdAt = datetime('now', 'localtime'),
		updatedAt = datetime('now', 'localtime')
		WHERE rowid = new.rowid;
    END;

CREATE TRIGGER After_UPDATE_Categoria AFTER UPDATE ON categoria
    BEGIN
       UPDATE categoria
		SET createdAt = updatedAt,
		updatedAt = datetime('now', 'localtime')
		WHERE rowid = new.rowid;
    END;

--PREMIO
DROP TRIGGER IF EXISTS "After_INSERT_premio";
DROP TRIGGER IF EXISTS "After_UPDATE_premio";
CREATE TRIGGER After_INSERT_premio AFTER INSERT ON premio
    BEGIN
       UPDATE premio SET createdAt = datetime('now', 'localtime'),
	   updatedAt = datetime('now', 'localtime')
       WHERE rowid = new.rowid;
    END;
CREATE TRIGGER After_UPDATE_premio AFTER UPDATE ON premio
    BEGIN
       UPDATE premio SET updatedAt = datetime('now', 'localtime')
       WHERE rowid = new.rowid;
    END;
COMMIT;

--Ativacao
DROP TRIGGER IF EXISTS "After_INSERT_ativacao";
DROP TRIGGER IF EXISTS "Before_UPDATE_ativacao";
DROP TRIGGER IF EXISTS "After_UPDATE_ativacao";
DROP TRIGGER IF EXISTS "After_UPDATE_ativacao_atual";
DROP TRIGGER IF EXISTS "Before_INSERT_ativacao_atual";

CREATE TRIGGER After_INSERT_ativacao AFTER INSERT ON ativacao
    BEGIN
       UPDATE ativacao
		SET createdAt = datetime('now', 'localtime'),
		updatedAt = datetime('now', 'localtime')
		WHERE rowid = new.rowid;
    END;


CREATE TRIGGER Before_INSERT_ativacao_atual BEFORE INSERT ON ativacao
	WHEN NEW.atual = 1
		BEGIN
			UPDATE Ativacao
			SET atual = 0
			WHERE id != NEW.id;
		END;
	
	
CREATE TRIGGER Before_UPDATE_ativacao BEFORE UPDATE ON ativacao
    BEGIN
       UPDATE ativacao
		SET updatedAt = new.createdAt
		WHERE rowid = new.rowid;
    END;
CREATE TRIGGER After_UPDATE_ativacao AFTER UPDATE ON ativacao
    BEGIN
       UPDATE ativacao
		SET createdAt = updatedAt,
		updatedAt = datetime('now', 'localtime')
		WHERE rowid = new.rowid;
    END;

	

CREATE TRIGGER After_UPDATE_ativacao_atual AFTER UPDATE ON ativacao
 	WHEN NEW.atual = 1
		BEGIN
			UPDATE Ativacao
			SET atual = 0
			WHERE id != NEW.id;
		END;


DROP TRIGGER IF EXISTS "UPDATE_AppConfig";

CREATE TRIGGER UPDATE_AppConfig BEFORE UPDATE ON AppConfig
    BEGIN
       UPDATE AppConfig SET updatedAt = datetime('now', 'localtime')
       WHERE rowid = new.rowid;
    END;


DROP TRIGGER IF EXISTS "INSERT_AppConfig";

CREATE TRIGGER INSERT_AppConfig AFTER INSERT ON AppConfig
    BEGIN
       UPDATE AppConfig SET createdAt = datetime('now', 'localtime')
       WHERE rowid = new.rowid;
    END;

