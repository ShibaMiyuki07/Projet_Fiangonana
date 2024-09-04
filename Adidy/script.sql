grant select,update,insert on table public.utilisateurs to fiangonana;
grant select,update,insert on table public.type to fiangonana;
grant select,update,insert on table public.paiement_adidy to fiangonana;
grant select,update,insert on table public.paiement_isantaona to fiangonana;
grant select,update,insert on table public.droits to fiangonana;
grant select,update,insert on table public.droit_utilisateurs to fiangonana;
grant select,update,insert on table public.mpandray to fiangonana;

grant usage on sequence idtype to fiangonana;
grant usage on sequence idpaiement_adidy to fiangonana;
grant usage on sequence idpaiement_isantaona to fiangonana;
grant usage on sequence idutilisateur to fiangonana;
grant usage on sequence iddroit_utilisateur to fiangonana;


create sequence idtype;
create sequence idpaiement_adidy;
create sequence idpaiement_isantaona;
create sequence idutilisateur;
create sequence iddroit_utilisateur;

alter sequence idpaiement_adidy restart 0;


create table mpandray(numero int primary key,anarana text,fanampiny text,fonenana text,fokontany text,tel char(11));

create table type(idtype char(4) primary key default concat('T0',nextval('idtype')),nomAdidy text);

create table paiement_adidy(idPaiement_adidy char(10) primary key default concat('PA',nextval('idpaiement_adidy')),numero_mpandray int, mois_debut int,annee_debut int,mois_fin int,annee_fin int,montant decimal(10,2),duree int, dateheurepaiemet timestamp default now(),foreign key(numero_mpandray) references mpandray(numero));

create table paiement_isantaona(idpaiement_isantaona char(10) primary key default concat('PI',nextval('idpaiement_isantaona')),numero_mpandray int,annee_debut int,annee_fin int,montant decimal(10,2),dateheurepaiemet timestamp default now(),foreign key(numero_mpandray) references mpandray(numero));

create table utilisateurs(idutilisateur char(5) primary key default concat('U00',nextval('idutilisateur')),nomutilisateur text,motdepasse text);

create table droits(iddroit serial primary key,typedroit text);

create table droit_utilisateurs(iddroit_utilisateur char(6) primary key default concat('DU00',nextval('iddroit_utilisateur')),iddroit int, idutilisateur char(5),isvalid boolean,foreign key(iddroit) references droits(iddroit),foreign key(idutilisateur) references utilisateurs(idutilisateur));

insert into type(nomAdidy) values('vkt'),('ikt'),('adidy');

insert into table droits(typedroit) values
("/Home/Ajout"),("/Home/Details","/Home/Liste","/Home/Home");