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