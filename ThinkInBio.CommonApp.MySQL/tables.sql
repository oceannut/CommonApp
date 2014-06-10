
drop table cyUser;
drop table cyUserRole;
drop table cyRole;
drop table cyRolePremission;
drop table cyBizNotification;

create table cyUser
(
	username				VARCHAR(32)		NOT NULL,
	pwd						VARCHAR(32)		NOT NULL,
	name					VARCHAR(32),
	_group					VARCHAR(32),
	creation				DATETIME		NOT NULL,
	modification			DATETIME		NOT NULL,
	PRIMARY KEY (username)
);

create table cyUserRole
(
	username				VARCHAR(32)		NOT NULL,
	roleId					VARCHAR(32)		NOT NULL,
	creation				DATETIME		NOT NULL,
	PRIMARY KEY (username,roleId)
);

create table cyRole
(
	id						VARCHAR(32)		NOT NULL,
	name					VARCHAR(32)		NOT NULL,
	creation				DATETIME		NOT NULL,
	modification			DATETIME		NOT NULL,
	PRIMARY KEY (id)
);

create table cyRolePremission
(
	roleId					VARCHAR(32)		NOT NULL,
	premission				VARCHAR(32)		NOT NULL,
	creation				DATETIME		NOT NULL,
	PRIMARY KEY (roleId,premission)
);

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