
drop table cyUser;
drop table cyUserRole;
drop table cyCategory;
drop table cyNotice;
drop table cyBizNotification;
drop table cyJobLog;


create table cyUser
(
	username				VARCHAR(32)		NOT NULL,
	pwd						VARCHAR(32)		NOT NULL,
	name					VARCHAR(32),
	_group					VARCHAR(32),
	disused					TINYINT(1)		NOT NULL default 0,
	avatar					VARCHAR(255),
	creation				DATETIME		NOT NULL,
	modification			DATETIME		NOT NULL,
	PRIMARY KEY (username)
);

create table cyUserRole
(
	username				VARCHAR(32)		NOT NULL,
	_role					VARCHAR(32)		NOT NULL,
	PRIMARY KEY (username,_role)
);

create table cyCategory
(
	id						BIGINT	unsigned	NOT NULL AUTO_INCREMENT,
	code					VARCHAR(32)		NOT NULL,
	name					VARCHAR(32)		NOT NULL,
	description				VARCHAR(255),
	icon					VARCHAR(32),
	parentId				BIGINT,
	sequence				INT				NOT NULL,
	disused					TINYINT(1)		NOT NULL default 0,
	scope					VARCHAR(32)		NOT NULL,
	PRIMARY KEY (id)
);
ALTER TABLE cyCategory ADD INDEX scope_code_index  (scope,code);

create table cyNotice
(
	id						BIGINT	unsigned	NOT NULL AUTO_INCREMENT,
	title					VARCHAR(255)		NOT NULL,
	content					VARCHAR(10240),
	creator					VARCHAR(32)			NOT NULL,
	creation				DATETIME			NOT NULL,
	modification			DATETIME			NOT NULL,
	PRIMARY KEY (id)
);
ALTER TABLE cyNotice ADD INDEX modification_index  (modification);

create table cyBizNotification
(
	id						BIGINT	unsigned	NOT NULL AUTO_INCREMENT,
	sender					VARCHAR(32)		NOT NULL,
	receiver				VARCHAR(32)		NOT NULL,
	content					VARCHAR(255),
	creation				DATETIME		NOT NULL,
	review					DATETIME,
	_resource				VARCHAR(32)		NOT NULL,
	resourceId				VARCHAR(32)		NOT NULL,
	PRIMARY KEY (id)
);

create table cyJobLog
(
	id						BIGINT	unsigned	NOT NULL AUTO_INCREMENT,
	scope					VARCHAR(32)			NOT NULL,
	count					INT					NOT NULL default 0,
	timestamp				DATETIME			NOT NULL,
	creation				DATETIME			NOT NULL,
	PRIMARY KEY (id)
);
ALTER TABLE cyJobLog ADD INDEX timestamp_index  (timestamp);

