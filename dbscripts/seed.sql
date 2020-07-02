\connect cde

CREATE TABLE IF NOT EXISTS "account"
(
 	"id"       	serial NOT NULL,
 	"name"     	varchar(15) NOT NULL,
 	"email"    	text UNIQUE NOT NULL,
 	"salt"     	text NOT NULL,
 	"passhash" 	text NOT NULL,
 	PRIMARY KEY ( "id" )
);

CREATE TYPE "branch" AS ENUM
(
 	'Development', 'Homologation', 'Production'
);

CREATE TYPE "level" AS ENUM
(
	'Trace', 'Debug', 'Information', 'Warning', 'Error', 'Critical'
);

CREATE TABLE "system"
(
 	"id"   serial NOT NULL,
 	"name" varchar(50) NOT NULL,
 	PRIMARY KEY ( "id" )
);

CREATE TABLE "log"
(
 	"id"       	serial NOT NULL,
 	"title"     varchar(100) NOT NULL,
 	"details"   text NOT NULL,
 	"level"  	level,
 	"branch"   	branch,
 	"date"		DATE NOT NULL,
 	"systemid" integer NOT NULL,
 	PRIMARY KEY ( "id" ),
 	FOREIGN KEY ( "systemid" ) REFERENCES "system" ( "id" )
);

INSERT INTO account(name, email, salt, passhash) values('test', 'test@email.com', 'test', 'test');
INSERT INTO system(name) values('System Test');
INSERT INTO log(title, details, level, branch, date, systemid) values('test', 'test details', 'Critical', 'Production', (SELECT NOW()), (SELECT id FROM system WHERE name = 'System Test'));
