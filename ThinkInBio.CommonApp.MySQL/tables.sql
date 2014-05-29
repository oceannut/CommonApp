
drop table cyUser;
drop table cyUserRole;
drop table cyRole;
drop table cyRolePremission;

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
	PRIMARY KEY (username,roleId)
);

create table cyRole
(
	id						VARCHAR(32)		NOT NULL,
	name					VARCHAR(32)		NOT NULL,
	PRIMARY KEY (id)
);

create table cyRolePremission
(
	roleId					VARCHAR(32)		NOT NULL,
	premission				VARCHAR(32)		NOT NULL,
	PRIMARY KEY (roleId,premission)
);