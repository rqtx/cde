\connect cde

INSERT INTO level(name) values('Information');
INSERT INTO level(name) values('Warning');
INSERT INTO level(name) values('Error');
INSERT INTO level(name) values('Fatal');
INSERT INTO level(name) values('Critical');
INSERT INTO level(name) values('Trace');

INSERT INTO role(name) values('admin');
INSERT INTO role(name) values('user');
INSERT INTO role(name) values('system');
INSERT INTO level(name) values('Debug');

INSERT INTO system(name) values('VavilovSeeds');
INSERT INTO log(title, details, created_at, systemid, levelid) values('test', 'test details', (SELECT NOW()), (SELECT id FROM system WHERE name = 'VavilovSeeds'), (SELECT id FROM level WHERE name = 'Critical'));
INSERT INTO log(title, details, created_at, systemid, levelid) values('test', 'test details', (SELECT NOW()), (SELECT id FROM system WHERE name = 'VavilovSeeds'), (SELECT id FROM level WHERE name = 'Critical'));
INSERT INTO log(title, details, created_at, systemid, levelid) values('test', 'test details', (SELECT NOW()), (SELECT id FROM system WHERE name = 'VavilovSeeds'), (SELECT id FROM level WHERE name = 'Fatal'));

INSERT INTO account(name, roleid, salt, passhash) values('admin', (SELECT id FROM role WHERE name = 'admin'), 'rqtx', 'AqQRtFGUWhTFB6l7+/7hqooltVlWRZ58Y2Kym/Kw0joEJLz/7FeGV5+TExAfrdNiEHao6U0wcSUdiGiqKSTyWA==');
INSERT INTO account(name, roleid, salt, passhash) values('vavilov', (SELECT id FROM role WHERE name = 'user'), 'DiwXrCYP3BfI4QUqANW443wRypmeA6ld0GcrihCUhS/T+XfPXKfnuwRKYemohGjbWgB9lPnUd6YbVpv6D7dRYA==', 'n5GMxCZ20meXvKhQzmiZ5Ubc++pSMGTNuQ6Arjj1UDNqBbi4TyJU0XSJN2kxewFZhRjXuwR1KdBb8Bvj3H6sJg==');
INSERT INTO account(name, roleid, salt, passhash) values('VavilovSeeds', (SELECT id FROM role WHERE name = 'system'), 'DiwXrCYP3BfI4QUqANW443wRypmeA6ld0GcrihCUhS/T+XfPXKfnuwRKYemohGjbWgB9lPnUd6YbVpv6D7dRYA==', 'n5GMxCZ20meXvKhQzmiZ5Ubc++pSMGTNuQ6Arjj1UDNqBbi4TyJU0XSJN2kxewFZhRjXuwR1KdBb8Bvj3H6sJg==');