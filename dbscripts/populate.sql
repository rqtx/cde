\connect cde

INSERT INTO account(name, email, salt, passhash) values('admin', 'admin@email.com', 'rqtx', 'AqQRtFGUWhTFB6l7+/7hqooltVlWRZ58Y2Kym/Kw0joEJLz/7FeGV5+TExAfrdNiEHao6U0wcSUdiGiqKSTyWA==');
INSERT INTO level(name) values('Debug');
INSERT INTO level(name) values('Information');
INSERT INTO level(name) values('Warning');
INSERT INTO level(name) values('Error');
INSERT INTO level(name) values('Critical');
INSERT INTO level(name) values('Trace');