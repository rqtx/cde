
\connect cde

CREATE TABLE IF NOT EXISTS "account"
(
 	"id"       serial NOT NULL,
 	"name"     varchar(15) NOT NULL,
 	"email"    text UNIQUE NOT NULL,
 	PRIMARY KEY ( "id" )
);

CREATE TABLE IF NOT EXISTS "credential"
(
	"id"		serial NOT NULL,
	"accountid" integer NOT NULL,
	"salt"     	text NOT NULL,
 	"passhash" 	text NOT NULL,
 	PRIMARY KEY ( "id" ),
 	FOREIGN KEY ( "accountid" ) REFERENCES "account" ( "id" ) ON DELETE CASCADE
);

CREATE TABLE "log"
(
 "id"      serial NOT NULL,
 "msg"     varchar(100) NOT NULL,
 "projeto" varchar(50) NOT NULL,
 "branch"  varchar(10) NOT NULL,
 "cod"     int NOT NULL,
 PRIMARY KEY ( "id" )
);

INSERT INTO account(name, email, salt, passhash) values('test', 'test@email.com');
INSERT INTO credential(accountid, salt, passhash) values((SELECT id FROM account WHERE name='test'), , 'test');















