
drop table cyUser;
drop table cyUserRole;
drop table cyCategory;
drop table cyBizNotification;


create table cyUser
(
	username				VARCHAR(32)		NOT NULL,
	pwd						VARCHAR(32)		NOT NULL,
	name					VARCHAR(32),
	_group					VARCHAR(32),
	disused					TINYINT(1)		NOT NULL default 0,
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

