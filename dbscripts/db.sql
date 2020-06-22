
CREATE TABLE IF NOT EXISTS "account"
(
 "id"       serial NOT NULL,
 "name"     varchar(15) NOT NULL,
 "email"    text NOT NULL,
 "salt"     text NOT NULL,
 "passhash" text NOT NULL,
 PRIMARY KEY ( "id" )
);

CREATE TABLE IF NOT EXISTS "level"
(
 "id"   bit (4) NOT NULL,
 "name" varchar(10) NOT NULL,
 PRIMARY KEY ( "id" )
);

CREATE TABLE IF NOT EXISTS "branch"
(
 "id"   bit (3) NOT NULL,
 "name" varchar(10) NOT NULL,
 PRIMARY KEY ( "id" )
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















