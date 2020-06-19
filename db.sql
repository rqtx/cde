

CREATE TABLE "user"
(
 "id"       serial NOT NULL,
 "name"     varchar(15) NOT NULL,
 "email"    text NOT NULL,
 "salt"     text NOT NULL,
 "passhash" text NOT NULL,
 PRIMARY KEY ( "id" )
);

CREATE TABLE "level"
(
 "id"   bit (4) NOT NULL,
 "name" varchar(10) NOT NULL,
 PRIMARY KEY ( "id" )
);

CREATE TABLE "branch"
(
 "id"   bit (3) NOT NULL,
 "name" varchar(10) NOT NULL,
 PRIMARY KEY ( "id" )
);

CREATE TABLE "log"
(
 "id"        serial NOT NULL,
 "level_id"  bit (4) NOT NULL,
 "branch_id" bit (3) NOT NULL,
 "msg"       varchar(100) NOT NULL,
 "events"    integer NULL,
 PRIMARY KEY ( "id" ),
 FOREIGN KEY ( "level_id" ) REFERENCES "level" ( "id" ),
 FOREIGN KEY ( "branch_id" ) REFERENCES "branch" ( "id" )
);







