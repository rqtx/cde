\connect cde

CREATE TABLE IF NOT EXISTS "account"
(
 "id"       serial NOT NULL,
 "name"     varchar(15) NOT NULL,
 "email"    text NOT NULL,
 "salt"     text NOT NULL,
 "passhash" text NOT NULL,
 PRIMARY KEY ( "id" )
);

CREATE TABLE IF NOT EXISTS "log"
(
 "id"      serial NOT NULL,
 "msg"     varchar(100) NOT NULL,
 PRIMARY KEY ( "id" )
);

INSERT INTO log(msg) values('test');
