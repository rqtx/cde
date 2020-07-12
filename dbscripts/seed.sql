\connect cde

CREATE TABLE IF NOT EXISTS "account"
(
 	"id"       		serial NOT NULL,
 	"name"     		varchar(15) NOT NULL,
 	"email"    		text UNIQUE NOT NULL,
 	"salt"     		text NOT NULL,
 	"passhash" 		text NOT NULL,
 	"created_at"	DATE NOT NULL DEFAULT CURRENT_TIMESTAMP,
 	PRIMARY KEY ( "id" )
);

CREATE TABLE "level"
(
	"id"   smallserial NOT NULL,
 	"name" varchar(50) UNIQUE NOT NULL,
 	PRIMARY KEY ( "id" )
);

CREATE TABLE "system"
(
 	"id"   serial NOT NULL,
 	"name" varchar(50) UNIQUE NOT NULL,
 	PRIMARY KEY ( "id" )
);

CREATE TABLE "log"
(
 	"id"       		serial NOT NULL,
 	"title"     	varchar(100) NOT NULL,
 	"details"   	text NOT NULL,
 	"created_at"	DATE NOT NULL DEFAULT CURRENT_TIMESTAMP,
 	"systemid" 		integer NOT NULL,
 	"levelid"  		integer NOT NULL,
 	PRIMARY KEY ( "id" ),
 	FOREIGN KEY ( "systemid" ) REFERENCES "system" ( "id" ),
 	FOREIGN KEY ( "levelid" ) REFERENCES "level" ( "id" )
);

INSERT INTO account(name, email, salt, passhash) values('admin', 'admin@email.com', 'rqtx', 'AqQRtFGUWhTFB6l7+/7hqooltVlWRZ58Y2Kym/Kw0joEJLz/7FeGV5+TExAfrdNiEHao6U0wcSUdiGiqKSTyWA==');
INSERT INTO system(name) values('System Test');
INSERT INTO level(name) values('Critical');
INSERT INTO level(name) values('Fatal');
INSERT INTO log(title, details, created_at, systemid, levelid) values('test', 'test details', (SELECT NOW()), (SELECT id FROM system WHERE name = 'System Test'), (SELECT id FROM level WHERE name = 'Critical'));
INSERT INTO log(title, details, created_at, systemid, levelid) values('test', 'test details', (SELECT NOW()), (SELECT id FROM system WHERE name = 'System Test'), (SELECT id FROM level WHERE name = 'Critical'));
INSERT INTO log(title, details, created_at, systemid, levelid) values('test', 'test details', (SELECT NOW()), (SELECT id FROM system WHERE name = 'System Test'), (SELECT id FROM level WHERE name = 'Fatal'));