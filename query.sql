CREATE TABLE AtalhosdeAplicativos (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nome TEXT NOT NULL,
    Caminho TEXT NOT NULL,
    Parametro TEXT NOT NULL,
    Imagem BLOB NOT NULL,
    Icon BLOB NOT NULL,
    DataUltimaSessao TEXT,
    TempoUltimaSessao TEXT
);
CREATE TABLE AplicativosExtras (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nome TEXT NOT NULL,
    Caminho TEXT NOT NULL,
    Icon BLOB NOT NULL
);

SELECT Id FROM AtalhosdeAplicativos;

SELECT * FROM AtalhosdeAplicativos;
SELECT * FROM AplicativosExtras;

DELETE FROM sqlite_sequence WHERE name='AtalhosdeAplicativos';
DELETE FROM AplicativosExtras WHERE id = 1;

DROP TABLE AtalhosdeAplicativos;
DROP TABLE AplicativosExtras;

INSERT INTO AtalhosdeAplicativos (Nome, Caminho, Parametro, Imagem, Icon) VALUES ("Need for Speed - Most Wanted", "C:/", "", "", "");
INSERT INTO AplicativosExtras (Nome, Caminho, Icon) VALUES ("Spotify", "spotify:playlist:6Jdt0jq9Ws3cKZE8ujrZlv?si=e26e69baf7d74a79", "");

CREATE TABLE AtalhosdeAplicativos_Temp (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nome TEXT NOT NULL,
    Caminho TEXT NOT NULL,
    Parametro TEXT NOT NULL,
    Imagem BLOB NOT NULL,
    Icon BLOB NOT NULL,
    DataUltimaSessao TEXT,
    TempoUltimaSessao TEXT,
    DataTodasSessoes TEXT,
    TempoTodasSessoes TEXT
);

INSERT INTO AtalhosdeAplicativos_Temp (Id, Nome, Caminho, Parametro, Imagem, Icon, DataUltimaSessao, TempoUltimaSessao, DataTodasSessoes, TempoTodasSessoes) SELECT Id, Nome, Caminho, Parametro, Imagem, Icon, DataUltimaSessao, TempoUltimaSessao, DataUltimaSessao, TempoUltimaSessao FROM AtalhosdeAplicativos;

DROP TABLE AtalhosdeAplicativos;

ALTER TABLE AtalhosdeAplicativos_Temp RENAME TO AtalhosdeAplicativos;
/*
spotify:playlist:6Jdt0jq9Ws3cKZE8ujrZlv?si=e26e69baf7d74a79
discord://discord.com/channels/736715367254327388/885653191147020338

https://open.spotify.com/playlist/6Jdt0jq9Ws3cKZE8ujrZlv?si=e26e69baf7d74a79
https://discord.com/channels/736715367254327388/885653191147020338
https://youtube.com/playlist?list=PLY8RhIof8dtIRXdH7lWfnHeUi9OnwiBw6&si=U4XvDgv-zyBKMoRO
*/