create keyspace "test"
    with replication = {'class' : 'SimpleStrategy', 'replication_factor' : 1}
    and durable_writes = false;



use "test";


CREATE TABLE login_register (
    email text,
    password_hash text,
    role text,
    salt text,
    PRIMARY KEY (email)
) WITH caching={'keys':'ALL', 'rows_per_partition':'NONE'}
 AND compaction={'class':'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy', 'max_threshold':'32', 'min_threshold':'4'}
 AND compression={'chunk_length_in_kb':'64', 'class':'org.apache.cassandra.io.compress.LZ4Compressor'}
 AND dclocal_read_repair_chance=0.1
 AND speculative_retry='99PERCENTILE';

CREATE TABLE predmet (
    sifra_predmeta text,
    espb text,
    naziv_predmeta text,
    semestar text,
    smer text,
    PRIMARY KEY (sifra_predmeta)
) WITH caching={'keys':'ALL', 'rows_per_partition':'NONE'}
 AND compaction={'class':'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy', 'max_threshold':'32', 'min_threshold':'4'}
 AND compression={'chunk_length_in_kb':'64', 'class':'org.apache.cassandra.io.compress.LZ4Compressor'}
 AND dclocal_read_repair_chance=0.1
 AND speculative_retry='99PERCENTILE';

CREATE TABLE student (
    email text,
    godinaupisa text,
    ime text,
    indeks text,
    odobren boolean,
    prezime text,
    semestar text,
    smer text,
    PRIMARY KEY (email)
) WITH caching={'keys':'ALL', 'rows_per_partition':'NONE'}
 AND compaction={'class':'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy', 'max_threshold':'32', 'min_threshold':'4'}
 AND compression={'chunk_length_in_kb':'64', 'class':'org.apache.cassandra.io.compress.LZ4Compressor'}
 AND dclocal_read_repair_chance=0.1
 AND speculative_retry='99PERCENTILE';

CREATE TABLE polozeni_ispiti (
    id text,
    email_studenta text,
    ocena int,
    rok text,
    sifra_predmeta text,
    PRIMARY KEY (id)
) WITH caching={'keys':'ALL', 'rows_per_partition':'NONE'}
 AND compaction={'class':'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy', 'max_threshold':'32', 'min_threshold':'4'}
 AND compression={'chunk_length_in_kb':'64', 'class':'org.apache.cassandra.io.compress.LZ4Compressor'}
 AND dclocal_read_repair_chance=0.1
 AND speculative_retry='99PERCENTILE';

 CREATE TABLE sala (
    naziv text,
    sprat int,
    kapacitet int,
    PRIMARY KEY (naziv)
) WITH caching={'keys':'ALL', 'rows_per_partition':'NONE'}
 AND compaction={'class':'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy', 'max_threshold':'32', 'min_threshold':'4'}
 AND compression={'chunk_length_in_kb':'64', 'class':'org.apache.cassandra.io.compress.LZ4Compressor'}
 AND dclocal_read_repair_chance=0.1
 AND speculative_retry='99PERCENTILE';

CREATE TABLE profesor (
    email text,
    br_telefona text,
    ime text,
    kancelarija text,
    prezime text,
    PRIMARY KEY (email)
) WITH caching={'keys':'ALL', 'rows_per_partition':'NONE'}
 AND compaction={'class':'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy', 'max_threshold':'32', 'min_threshold':'4'}
 AND compression={'chunk_length_in_kb':'64', 'class':'org.apache.cassandra.io.compress.LZ4Compressor'}
 AND dclocal_read_repair_chance=0.1
 AND speculative_retry='99PERCENTILE';

CREATE TABLE predaje_predmet (
    id text,
    sifra_predmeta text,
    email_profesora text,
    PRIMARY KEY (id)
) WITH caching={'keys':'ALL', 'rows_per_partition':'NONE'}
 AND compaction={'class':'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy', 'max_threshold':'32', 'min_threshold':'4'}
 AND compression={'chunk_length_in_kb':'64', 'class':'org.apache.cassandra.io.compress.LZ4Compressor'}
 AND dclocal_read_repair_chance=0.1
 AND speculative_retry='99PERCENTILE';

CREATE TABLE zabranjena_prijava (
    id text,
    datum_isteka date,
    email_student text,
    razlog text,
    sifra_predmeta text,
    PRIMARY KEY (id, datum_isteka)
) WITH caching={'keys':'ALL', 'rows_per_partition':'NONE'}
 AND compaction={'class':'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy', 'max_threshold':'32', 'min_threshold':'4'}
 AND compression={'chunk_length_in_kb':'64', 'class':'org.apache.cassandra.io.compress.LZ4Compressor'}
 AND dclocal_read_repair_chance=0.1
 AND speculative_retry='99PERCENTILE';

CREATE TABLE obavestenje (
    id_obavestenja text,
    datum_objave date,
    email_profesor text,
    sifra_predmeta text,
    tekst text,
    PRIMARY KEY (id_obavestenja, datum_objave)
) WITH caching={'keys':'ALL', 'rows_per_partition':'NONE'}
 AND compaction={'class':'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy', 'max_threshold':'32', 'min_threshold':'4'}
 AND compression={'chunk_length_in_kb':'64', 'class':'org.apache.cassandra.io.compress.LZ4Compressor'}
 AND dclocal_read_repair_chance=0.1
 AND speculative_retry='99PERCENTILE';

CREATE TABLE rok (
    id text,
    pocetak_roka date,
    kraj_prijave date,
    naziv text,
    pocetak_prijave date,
    zavrsetak_roka date,
    PRIMARY KEY (id, pocetak_roka)
) WITH caching={'keys':'ALL', 'rows_per_partition':'NONE'}
 AND compaction={'class':'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy', 'max_threshold':'32', 'min_threshold':'4'}
 AND compression={'chunk_length_in_kb':'64', 'class':'org.apache.cassandra.io.compress.LZ4Compressor'}
 AND dclocal_read_repair_chance=0.1
 AND speculative_retry='99PERCENTILE';

CREATE TABLE satnica (
    id text,
    rok_id text,
    datum date,
    sifra_predmeta text,
    vreme text,
	naziv_sale text,
    PRIMARY KEY (id, rok_id)
) WITH caching={'keys':'ALL', 'rows_per_partition':'NONE'}
 AND compaction={'class':'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy', 'max_threshold':'32', 'min_threshold':'4'}
 AND compression={'chunk_length_in_kb':'64', 'class':'org.apache.cassandra.io.compress.LZ4Compressor'}
 AND dclocal_read_repair_chance=0.1
 AND speculative_retry='99PERCENTILE';

CREATE TABLE prijave_ispita (
    id text,
    rok_id text,
    dezurni_prof_email text,
    email_studenta text,
    naziv_sale text,
    sifra_predmeta text,
    PRIMARY KEY (id, rok_id)
) WITH caching={'keys':'ALL', 'rows_per_partition':'NONE'}
 AND compaction={'class':'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy', 'max_threshold':'32', 'min_threshold':'4'}
 AND compression={'chunk_length_in_kb':'64', 'class':'org.apache.cassandra.io.compress.LZ4Compressor'}
 AND dclocal_read_repair_chance=0.1
 AND speculative_retry='99PERCENTILE';

// login_register table data.
insert into "login_register"("email","password_hash","role","salt") values('culinjo@elfak.rs','sifra123','Student','Sadasd');


// predmet table data.
insert into "predmet"("sifra_predmeta","espb","naziv_predmeta","semestar","smer") values('A234B','6','BazeStare','3','RII');
insert into "predmet"("sifra_predmeta","espb","naziv_predmeta","semestar","smer") values('A123B','5','BazeNove','3','RII');


// student table data.
insert into "student"("email","godinaupisa","ime","indeks","odobren","prezime","semestar","smer") values('natana@elfak.rs','2018','Natalija','17321',true,'Pavlovic','7','RII');
insert into "student"("email","godinaupisa","ime","indeks","odobren","prezime","semestar","smer") values('culinjo@elfak.rs','2018','Slavko','17345',true,'Petrovic','3','RII');


// polozeni_ispiti table data.
insert into "polozeni_ispiti"("id","email_studenta","ocena","rok","sifra_predmeta") values('0','culinjo@elfak.rs',8,'JANUAR2022','A123B');


// sala table data.
insert into "sala"("naziv","kapacitet","sprat") values('NikolaTesla',99,0);
insert into "sala"("naziv","kapacitet","sprat") values('525',150,5);


// profesor table data.
insert into "profesor"("email","br_telefona","ime","kancelarija","prezime") values('prof@elfak.rs','066','Zvonko','333','Bogdan');


// predaje_predmet table data.
insert into "predaje_predmet"("id","email_profesora","sifra_predmeta") values('0','prof@elfak.rs','A123B');

// zabranjena_prijava table data.
insert into "zabranjena_prijava"("id","datum_isteka","email_student","razlog","sifra_predmeta") values('0','2022-01-25','culinjo@elfak.rs','prepisivao','A234B');


// obavestenje table data.
insert into "obavestenje"("id_obavestenja","datum_objave","email_profesor","sifra_predmeta","tekst") values('0','2022-01-09','prof@email.com','A123B','Prijava ispita soon');


// rok table data.
insert into "rok"("id","pocetak_roka","kraj_prijave","naziv","pocetak_prijave","zavrsetak_roka") values('0','2022-01-16','2022-01-09','JANUAR2022','2021-12-27','2022-02-14');


// satnica table data.
insert into "satnica"("id","rok_id","datum","sifra_predmeta","vreme","naziv_sale") values('0','0','2022-01-21','A123B','14:00','525');

// prijave_ispita table data.
insert into "prijave_ispita"("id","rok_id","dezurni_prof_email","email_studenta","naziv_sale","sifra_predmeta") values('0','0','prof@elfak.rs','culinjo@elfak.rs','NikolaTesla','A234B');

CREATE TABLE counting_id (
    counting text,
    tabela text,
    PRIMARY KEY (tabela)
) WITH caching={'keys':'ALL', 'rows_per_partition':'NONE'}
 AND compaction={'class':'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy', 'max_threshold':'32', 'min_threshold':'4'}
 AND compression={'chunk_length_in_kb':'64', 'class':'org.apache.cassandra.io.compress.LZ4Compressor'}
 AND dclocal_read_repair_chance=0.1
 AND speculative_retry='99PERCENTILE';
 
 insert into "counting_id"("counting","tabela") values('1','obavestenje');
 insert into "counting_id"("counting","tabela") values('1','polozeni_ispiti');
 insert into "counting_id"("counting","tabela") values('1','predaje_predmet');
 insert into "counting_id"("counting","tabela") values('1','prijave_ispita');
 insert into "counting_id"("counting","tabela") values('1','rok');
 insert into "counting_id"("counting","tabela") values('1','satnica');
 insert into "counting_id"("counting","tabela") values('1','zabranjena_prijava');


insert into "zabranjena_prijava"("id","datum_isteka","email_student","razlog","sifra_predmeta") values('1','2021-01-25','culinjo@elfak.rs','prepisivao','A123B');
