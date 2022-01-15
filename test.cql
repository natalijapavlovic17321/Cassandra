create keyspace "test"
    with replication = {'class' : 'SimpleStrategy', 'replication_factor' : 1}
    and durable_writes = false;



use "test";


CREATE TABLE counting_id (
    tabela text,
    counting text,
    PRIMARY KEY (tabela)
) WITH caching={'keys':'ALL', 'rows_per_partition':'NONE'}
 AND compaction={'class':'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy', 'max_threshold':'32', 'min_threshold':'4'}
 AND compression={'chunk_length_in_kb':'64', 'class':'org.apache.cassandra.io.compress.LZ4Compressor'}
 AND dclocal_read_repair_chance=0.1
 AND speculative_retry='99PERCENTILE';

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

CREATE TABLE mesto (
    id text,
    sala_naziv text,
    mesto_br int,
    PRIMARY KEY (id, sala_naziv)
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

CREATE TABLE predaje_predmet (
    id text,
    email_profesora text,
    sifra_predmeta text,
    PRIMARY KEY (id)
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

CREATE TABLE prijave_ispita (
    id text,
    rok_id text,
    email_studenta text,
    naziv_sale text,
    sifra_predmeta text,
    PRIMARY KEY (id, rok_id)
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

CREATE TABLE sala (
    naziv text,
    kapacitet int,
    sprat int,
    PRIMARY KEY (naziv)
) WITH caching={'keys':'ALL', 'rows_per_partition':'NONE'}
 AND compaction={'class':'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy', 'max_threshold':'32', 'min_threshold':'4'}
 AND compression={'chunk_length_in_kb':'64', 'class':'org.apache.cassandra.io.compress.LZ4Compressor'}
 AND dclocal_read_repair_chance=0.1
 AND speculative_retry='99PERCENTILE';

CREATE TABLE satnica (
    id text,
    rok_id text,
    datum date,
    naziv_sale text,
    sifra_predmeta text,
    vreme text,
    PRIMARY KEY (id, rok_id)
) WITH caching={'keys':'ALL', 'rows_per_partition':'NONE'}
 AND compaction={'class':'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy', 'max_threshold':'32', 'min_threshold':'4'}
 AND compression={'chunk_length_in_kb':'64', 'class':'org.apache.cassandra.io.compress.LZ4Compressor'}
 AND dclocal_read_repair_chance=0.1
 AND speculative_retry='99PERCENTILE';

CREATE TABLE student (
    email text,
    dugovanje text,
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

// counting_id table has no data.

// login_register table data.
insert into "login_register"("email","password_hash","role","salt") values('mate@elfak.rs','ALlG8DPHFgxtREUga787M6gEHgCDfBFl910DzN45Ym5q1X+M836QDf3vhaLkm5uZhDnDt1ekx5/pSVARLwIi59aRJF+nxQ==','Student','bUSC68GQ0JWEMDksnaGkCiOwcuqgrqRpk9PHKxW0X1iRIBnw16t7m4Vr4JBFPuALJttUqZgIWpKNmRjpprh4Gl5YUcBekA==');
insert into "login_register"("email","password_hash","role","salt") values('joca.skljoca@elfak.rs','y+OZXExNwZUrgIJJCRA9yjVVA77oZ6oCqO2DVFRwSWWt/RcaEh187ajF0S09lIGIfIGQ3VJIjUqZkw+GAv0OOLKWDJy7Xw==','Student','HqtBLWDdNzMQ+wLGddVeYCOwnHjkHGvO7jR1alFs9Qgy9DoqDMHuC5EPSleVM8gfxzQPrgtQuGWhxGsJ0/YubYjQthaPZQ==');
insert into "login_register"("email","password_hash","role","salt") values('milos.bogdanovic@elfak.rs','Mw5RVI4WqSM242BNgTrmtOHsvJ1Fzz+glfMrx85FesSC/vDrngGphPFIDw0MFRpBiKMp2K3w6N3KB+NslWW0SRVRGqgSNw==','Profesor','bsXyfm/ThC9OLc+A5KSretvPPg314NtcrzlETVDeblpuP/grSOga/bHEKWP+GCT/kbhvkfupNfgMdWWKmwF7oo21mW95CQ==');
insert into "login_register"("email","password_hash","role","salt") values('emina.milovanovic@elfak.rs','tKNGhtzIXIK+kkp61wsg57jg6drEQCuQIq8FBOBA2sA2yvOXzzTEkHL1U62KKGE1MKQdtl4gHog0+aHn8NidCy7xOZtloA==','Profesor','ZNE4PVUw3oxVKytuZB23OaDhqAjj1R9Oz7NjwCE/gGQ37uMhntbpzsuS340SDKpYomNBqsyPKDEY3fZQivxebQs52mWbAg==');
insert into "login_register"("email","password_hash","role","salt") values('natana@elfak.rs','VBJjRyFK+9+zi169hRUSwWgQagC/gk+IbI4gVah8SOB/QKP2T1eDsIsU7w3ohAm+W5HYgScpifwCDVbfmABrc5ZvW6tLpQ==','Student','Wz0rj2ifvUkQMleXwpiVXePuOmmdF3qrLI29s1HRlemJ39Q2ejuDh/NJOJor7z0kw43s7k41ye6B2O+6w4N7a7YKYEUMEQ==');
insert into "login_register"("email","password_hash","role","salt") values('aleksandar.stanimirovic@elfak.rs','418y1H5qV296Q/ZmGUVUxzH55EglE3KjfkRzef41zqPQ2n85oUqj2x4YxcyPcEYXxD3kxIxVBinvuKtx/08H6J0e50JXPw==','Profesor','1iDohVHmb83MgL2vFrwE0zoly4BwyYcFSIQ7zuaBbrJdrgKa5N5Xk1hmlG9tFYm+JotxyQHDWzNU7g0zaKpemEooV0rL4Q==');
insert into "login_register"("email","password_hash","role","salt") values('user@example.com','8An6o28ef7K1mmMIbQIkjjtvePtGuPcEUUP6ZseWqi0O/rdGDgNGZmIBaF5mOJvQzDI9lesT5u7lnvncHtHNmcvIoQJk/Q==','Profesor','XjMCAjbOX3HQ6RF6rrd24lDjZHMXDi9Xkv5if6HXueUBoEeBljZucKDwt+0KV7RoZLrgDpj1w6ZvskWugr1lGBc+ggMeEg==');
insert into "login_register"("email","password_hash","role","salt") values('saske@elfak.rs','8cbTUR9gLBf8B9zozWBdWv8NwVFENwygTuSkSRMs8mgVidH8DnRqoOMByouynGw5USSG9DOSXk8KAxNexwwFkxdArXvCKw==','Student','ND9CnnKP9Lgwnfm1R/3MUHUWQMh02yqsejJVwy6kvXkjvHEUIjPyIQ2c9MzyR6K/xxtGOY1I64iR6zJ52ajo5aQQZ/Rljg==');
insert into "login_register"("email","password_hash","role","salt") values('admin','RKgX3kStW44byiM6GszfuSKBoqM6FNJdOUKc0l+cQAd91wdMQ+ShurLLqkom9JirY+NYlgMlk8hMIz1XVA5cBPlGB8t38g==','Profesor','MPdySuU6d9cnlgrZH7eYDJBbSHo5cyoCJJqg7lSS1WRzh9OrUj2JJvA3z3fkvRHBEcWDMSskYJP9VQ02YswkX66dvNTyWQ==');
insert into "login_register"("email","password_hash","role","salt") values('string','TsJha67hf6jbf5PPTEb2/y4JOKKFgJ5RXOS3sLJ3oGDl4jBey3TiYlvGXW7i1hyifzC08Nj5sNxG3o+Gxh/6CEmKkdh7Rg==','Student','EdUMHYbZUrevFpNwBpk2JDDxM/ZJyEOtv6sbh4YC50d9/VMK1S4ZpPnC/38WHSyYuepBPx7k+XU8b4LnDZ+iZmNhjk/10A==');
insert into "login_register"("email","password_hash","role","salt") values('j@elfak.rs','vhZVtNSeYkEYOH/DjpD55WKTpdwa3EbbsV3+/XdjsoOyDVLhn/+4DrrG/P/MT4AKGSALJHGy+K98bs2po+PKW3LnmOhK6A==','Student','v3swP2fercWWXImu0evh+8FEpw5dEov3zybBBUZU/TM+sNG2xlUE7LvBJ4TpNxZAcauplU1vQBSK/7laAnlYO83S9OLHng==');
insert into "login_register"("email","password_hash","role","salt") values('mata@email.com','tGELZ+taLvpJabJchafm7n2M6/HnSIMqr79xkzEFwmE2kNAI+TVhHV2BN5qFWi2rEoOux+OFcFoKBQs0ltB13x4uaPcyAA==','Student','W4oP2VMtYu7KfMYwB2WPHElF5x5wAr8YlyfIVHIQh3WGTueNfi10CBUpwMH6JwMXYLek/PMyp2V/plALOcmv+OuZd829lA==');
insert into "login_register"("email","password_hash","role","salt") values('culinjo@elfak.rs','j1ITo3vosjptVUfbMLhxGl3GZgNc16JjJCDAKmrpZdMIe5CayBra9bBnqXVHXWerAYHllQlzTzMQelcIgKVu+Mfrpz/w0Q==','Student','XeFbJVuq15o/2NnuebQVB8j+DMzZiFvyer60ziwOgU4+7Hl7472QXhrGYJmyxa2cdD0o4tMdWaZApdi7PnY6RcCEXj50Kw==');


// mesto table data.
insert into "mesto"("id","sala_naziv","mesto_br") values('0','NikolaTesla',0);
insert into "mesto"("id","sala_naziv","mesto_br") values('1','NikolaTesla',1);


// obavestenje table data.
insert into "obavestenje"("id_obavestenja","datum_objave","email_profesor","sifra_predmeta","tekst") values('0','2022-01-09','prof@email.com','A123B','Prijava ispita soon');


// polozeni_ispiti table data.
insert into "polozeni_ispiti"("id","email_studenta","ocena","rok","sifra_predmeta") values('0','culinjo@elfak.rs',8,'JANUAR2022','A123B');


// predaje_predmet table data.
insert into "predaje_predmet"("id","email_profesora","sifra_predmeta") values('0','prof@elfak.rs','A123B');


// predmet table data.
insert into "predmet"("sifra_predmeta","espb","naziv_predmeta","semestar","smer") values('A234B','6','BazeStare','3','RII');
insert into "predmet"("sifra_predmeta","espb","naziv_predmeta","semestar","smer") values('A111B','3','AAAAAAAAA','3','RII');
insert into "predmet"("sifra_predmeta","espb","naziv_predmeta","semestar","smer") values('A123B','5','BazeNove','3','RII');


// prijave_ispita table data.
insert into "prijave_ispita"("id","rok_id","email_studenta","naziv_sale","sifra_predmeta") values('7','0','saske@elfak.rs','tbd','A444B');
insert into "prijave_ispita"("id","rok_id","email_studenta","naziv_sale","sifra_predmeta") values('4','0','natana@elfak.rs','tbd','A444B');
insert into "prijave_ispita"("id","rok_id","email_studenta","naziv_sale","sifra_predmeta") values('5','0','natana@elfak.rs','tbd','A111B');
insert into "prijave_ispita"("id","rok_id","email_studenta","naziv_sale","sifra_predmeta") values('2','0','mate@elfak.rs','tbd','A333B');
insert into "prijave_ispita"("id","rok_id","email_studenta","naziv_sale","sifra_predmeta") values('1','0','mate@elfak.rs','tbd','A111B');


// profesor table data.
insert into "profesor"("email","br_telefona","ime","kancelarija","prezime") values('milos.bogdanovic@elfak.rs','018654654','Milos','K2','Bogdanovic');
insert into "profesor"("email","br_telefona","ime","kancelarija","prezime") values('emina.milovanovic@elfak.rs','018987789','Emina','L2','Milovanovic');
insert into "profesor"("email","br_telefona","ime","kancelarija","prezime") values('prof@elfak.rs','066','Zvonko','333','Bogdan');
insert into "profesor"("email","br_telefona","ime","kancelarija","prezime") values('aleksandar.stanimirovic@elfak.rs','018183183','Aleksandar','K2','Stanimirovic');
insert into "profesor"("email","br_telefona","ime","kancelarija","prezime") values('user@example.com','string','string','string','string');
insert into "profesor"("email","br_telefona","ime","kancelarija","prezime") values('admin','018183183','Aleksandar','K2','Stanimirovic');


// rok table data.
insert into "rok"("id","pocetak_roka","kraj_prijave","naziv","pocetak_prijave","zavrsetak_roka") values('0','2022-01-16','2022-01-09','JANUAR2022','2021-12-27','2022-02-14');


// sala table data.
insert into "sala"("naziv","kapacitet","sprat") values('NikolaTesla',NULL,0);
insert into "sala"("naziv","kapacitet","sprat") values('525',NULL,5);
insert into "sala"("naziv","kapacitet","sprat") values('SlovodnaSala',NULL,2);


// satnica table data.
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('0','0','2022-01-21','a','A123B','14:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('2','0','2022-02-04','a','A234B','15:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('1','0','2022-01-27','a','A111B','13:00');


// student table data.
insert into "student"("email","dugovanje","godinaupisa","ime","indeks","odobren","prezime","semestar","smer") values('mate@elfak.rs','0','2018','Mateja','17326',false,'Pancic','7','RII');
insert into "student"("email","dugovanje","godinaupisa","ime","indeks","odobren","prezime","semestar","smer") values('joca.skljoca@elfak.rs','0','2018','Jovan','17333',false,'Radivojevic','4','AUT');
insert into "student"("email","dugovanje","godinaupisa","ime","indeks","odobren","prezime","semestar","smer") values('natana@elfak.rs','0','2018','Natalija','17321',false,'PavlovicevaMast','7','RII');
insert into "student"("email","dugovanje","godinaupisa","ime","indeks","odobren","prezime","semestar","smer") values('saske@elfak.rs','0','2018','Saske','17080',false,'Djokic','7','RII');
insert into "student"("email","dugovanje","godinaupisa","ime","indeks","odobren","prezime","semestar","smer") values('string',NULL,'string','string','string',false,'string','string','string');
insert into "student"("email","dugovanje","godinaupisa","ime","indeks","odobren","prezime","semestar","smer") values('j@elfak.rs',NULL,'2018','j','j',false,'j','j','j');
insert into "student"("email","dugovanje","godinaupisa","ime","indeks","odobren","prezime","semestar","smer") values('mata@email.com','0','2018','Mateja','17234',false,'Pancic','3','RII');
insert into "student"("email","dugovanje","godinaupisa","ime","indeks","odobren","prezime","semestar","smer") values('culinjo@elfak.rs','0','2018','Slavko','17345',false,'Petrovic','7','RII');


// zabranjena_prijava table has no data.

