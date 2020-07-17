\connect cde

CREATE TABLE "role"
(
	"id"   smallserial NOT NULL,
 	"name" varchar(25) UNIQUE NOT NULL,
 	PRIMARY KEY ( "id" )
);

CREATE TABLE IF NOT EXISTS "account"
(
 	"id"       		serial NOT NULL,
 	"name"     		varchar(15) UNIQUE NOT NULL,
 	"roleid"		integer NOT NULL,
 	"salt"     		text NOT NULL,
 	"passhash" 		text NOT NULL,
 	"created_at"	DATE NOT NULL DEFAULT CURRENT_TIMESTAMP,
 	PRIMARY KEY ( "id" ),
 	FOREIGN KEY ( "roleid" ) REFERENCES "role" ( "id" )
);

CREATE TABLE "level"
(
	"id"   smallserial NOT NULL,
 	"name" varchar(25) UNIQUE NOT NULL,
 	PRIMARY KEY ( "id" )
);

CREATE TABLE "system"
(
 	"id"   serial NOT NULL,
 	"name" varchar(25) UNIQUE NOT NULL,
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

INSERT INTO system(name) values('System Test');
INSERT INTO level(name) values('Critical');
INSERT INTO level(name) values('Fatal');
INSERT INTO role(name) values('admin');
INSERT INTO role(name) values('user');
INSERT INTO role(name) values('system');
INSERT INTO log(title, details, created_at, systemid, levelid) values('test', 'test details', (SELECT NOW()), (SELECT id FROM system WHERE name = 'System Test'), (SELECT id FROM level WHERE name = 'Critical'));
INSERT INTO log(title, details, created_at, systemid, levelid) values('test', 'test details', (SELECT NOW()), (SELECT id FROM system WHERE name = 'System Test'), (SELECT id FROM level WHERE name = 'Critical'));
INSERT INTO log(title, details, created_at, systemid, levelid) values('test', 'test details', (SELECT NOW()), (SELECT id FROM system WHERE name = 'System Test'), (SELECT id FROM level WHERE name = 'Fatal'));
INSERT INTO account(name, roleid, salt, passhash) values('admin', (SELECT id FROM role WHERE name = 'admin'), 'rqtx', 'AqQRtFGUWhTFB6l7+/7hqooltVlWRZ58Y2Kym/Kw0joEJLz/7FeGV5+TExAfrdNiEHao6U0wcSUdiGiqKSTyWA==');