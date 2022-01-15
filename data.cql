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

CREATE TABLE counting_id (
    tabela text,
    counting text,
    PRIMARY KEY (tabela)
) WITH caching={'keys':'ALL', 'rows_per_partition':'NONE'}
 AND compaction={'class':'org.apache.cassandra.db.compaction.SizeTieredCompactionStrategy', 'max_threshold':'32', 'min_threshold':'4'}
 AND compression={'chunk_length_in_kb':'64', 'class':'org.apache.cassandra.io.compress.LZ4Compressor'}
 AND dclocal_read_repair_chance=0.1
 AND speculative_retry='99PERCENTILE';

// login_register table data.
insert into "login_register"("email","password_hash","role","salt") values('mate@elfak.rs','XyRN4kj14OWJGFq6080N9frfJnC5uNJSo9OeWYr+xzGv8LMzrhST45E8D2NAtVXWCzuiThxyQCb2uBqNMptNzYHaziNn8w==','Student','1DMGB2JKf8vFmntQDJx3dNckpnDRzK+A+BqcxJd/Y9D5S4ZJ7jE9qmzPq1dz3U+4POq6OvM3fb0e8Un2lSEE9nQiyOA1Yg==');
insert into "login_register"("email","password_hash","role","salt") values('milos.bogdanovic@elfak.rs','9rh4OZ6qGrhGzuo3J4XF1LDioSxingvsXHp4ieHgySklAvnKQswHbPatqNtXsNCtjr1endQjvmPGWvfT4Ydsg5Tf+b8ADg==','Profesor','DwE3jMFxAUaIJ955wpNtWM7K/uP9e36lsKsCw2y1sBQb4esCk1mdMC4INmmzzsiH0Rl55ATrwVZhnsmp/0csimN52EHSZQ==');
insert into "login_register"("email","password_hash","role","salt") values('emina.milovanovic@elfak.rs','HEz2HFGPZq82avkRN6xbOfWH5PWvXnI1zNtybS+lshd71fmJFQ3epvTOvoUztIv1RuCX20TUkGbDVG1uBxHPp9QYehM0Eg==','Profesor','wW3lS62HLXk18AM8hQsf3JpiYS7+16Jp9MpYMqJ67ICnxiwbpKoj+y1Y9NR+cNuGQgY2GggHgbqXcCMEs4qStHI7FYcM3g==');
insert into "login_register"("email","password_hash","role","salt") values('nata@elfak.rs','bIX7uKtoVG00RW6ZuJjrGswTikuWemYY4QhNJYNI7odDHzXImDorYerKTP7UYCEZFoXiP77havGe1fn9uJocAXLJgvGh7A==','Student','UIjOu8a6rx01xmPxxfu5OWLUUJv1F17UGzWVCq5n5Ifs5VIZ8/IOqztzOlRM+KUhXWygWruMgmQ2t5jyrQuOR6oNpGZZRg==');
insert into "login_register"("email","password_hash","role","salt") values('aleksandar.stanimirovic@elfak.rs','HsNkhpQ819AEOk2iYx8rdeoS2rc9G9H97nmBdXT/3bd+qmE6qeSRJKjhl8emqIhcm4BMcGiVMoPuB7w7tx5AHvf7K1EcoA==','Profesor','RcE2IQntsS4asQKTSRV92pFcLTf/7I3qNIPGmSSojxON5HmkiFw8/IuS06TA8U5kc+z91jPXHYpTfd3bcTgHhutJYJxr7w==');
insert into "login_register"("email","password_hash","role","salt") values('saske@elfak.rs','JGPd/98bmnMlSTjrNRwJ9bGvltel1IB2l+dDsh8zZoqXZkt8DumuHI3BTUoRp/6Sm+xm0K8XAdbjiAUUDwxw7cMu8BMnvQ==','Student','O8LFeF1BteIFUs+MihPDOe7t8rQne/a+1wlVgKibGOFw7NhF3JTqIaficJifoA9BSGZ8/6qj7FSaeJj/J5zUk8eQK20JBQ==');
insert into "login_register"("email","password_hash","role","salt") values('nebojsa.raicevic@elfak.rs','4sMum/C3ATxL1YcNJOTn4WnHg+uI/TX2ArAjGdr5BS/Wd+61BgztXqvLXHUPYKmy+Uemn2rHs3HBcEp00V1Gj+kdp7rZ5A==','Profesor','EEEaRbnvYTfx6eLY4b8vZwGLCc8Okez9LCafaYHQd4hhx1eMtBDYphIq1jEhmj7mH4BhNpao1Gn0wUajxtWhbOOFPfyuyQ==');
insert into "login_register"("email","password_hash","role","salt") values('zovao@elfak.rs','vKL2erWt3OSRBR2jcfSwZFK16BX00dzV9qPtx4uNcOapRevsWqzqIT++qIh6wYQVrs62U7RfCXsKF5PNSr2lk+gW4lwNTg==','Student','cMKHEJuYax7LYR9pH1J8jOVpGjWDUGVpNbmg7vp82PL3u61/wyjTSM2zZ/mduE8/oNXvovWgyNBrGUZyS33dDWaTDJRQDQ==');
insert into "login_register"("email","password_hash","role","salt") values('admin@elfak.rs','jNF9vBfPSI0LwO9ShaftHjShJMsEv55gYEA9D3hF+xvgisA9oo28m1CEmXyCx9Yyuznnx72F55WrqACB1BLJ2ZhDgsnWvw==','Admin','k6Nq8uolgKF4FMzT/VXU/UEJcbLCqgb6jEuNLkgCJ3+GVYQurTPK4lS/7sLd/rI8Tzzw2dRYex6WQIob4WoXo2dvWEBfrg==');
insert into "login_register"("email","password_hash","role","salt") values('jovic@elfak.rs','cvdXJtCATxdXv4upT9iGY3sqio3uPYMZYrxBn/tXGcdSCojd/13bz7sCtZqc4tlf0FtXHs+wO4swjQDOLc8Y0V2jgvxxgQ==','Student','c/6r4I7Jcf6Jn/EjLOVz5nAm7kDOvUIE8sfr2MXxb3P+XjAJ8iR8nDlipjvwPpXqat5Lz0+n/6PJ8UBH2lhxgXrxvH1qTA==');
insert into "login_register"("email","password_hash","role","salt") values('culinjo@elfak.rs','axYBuHpNeijO6/SfzbO7pixqGwDHbewWI4HXMVHHohPtYwwxzASo7i8t4gjjFJNDUjP3Y/PjZDsmwCLBVQ+ScbyKMJZLNw==','Student','ZlG+ZbHpLOnqIqotnbuBPxWyORFYeTSeU7Xu3VDXZn4g04xXASWL3vB4P28I/xj/CWQvol8MNQnOYho7ArB2n7tpoN7/5Q==');


// obavestenje table data.
insert into "obavestenje"("id_obavestenja","datum_objave","email_profesor","sifra_predmeta","tekst") values('0','2022-01-15','milos.bogdanovic@elfak.rs','A111B','Konsultacije 13:00 u kancelariji L2');


// polozeni_ispiti table data.
insert into "polozeni_ispiti"("id","email_studenta","ocena","rok","sifra_predmeta") values('4','saske@elfak.rs',10,'0','A222B');
insert into "polozeni_ispiti"("id","email_studenta","ocena","rok","sifra_predmeta") values('3','nata@elfak.rs',8,'0','A111B');
insert into "polozeni_ispiti"("id","email_studenta","ocena","rok","sifra_predmeta") values('5','saske@elfak.rs',10,'0','A111B');
insert into "polozeni_ispiti"("id","email_studenta","ocena","rok","sifra_predmeta") values('0','culinjo@elfak.rs',10,'0','A444B');
insert into "polozeni_ispiti"("id","email_studenta","ocena","rok","sifra_predmeta") values('2','nata@elfak.rs',10,'0','A333B');
insert into "polozeni_ispiti"("id","email_studenta","ocena","rok","sifra_predmeta") values('1','culinjo@elfak.rs',6,'0','A555B');


// predaje_predmet table data.
insert into "predaje_predmet"("id","email_profesora","sifra_predmeta") values('6','emina.milovanovic@elfak.rs','A333B');
insert into "predaje_predmet"("id","email_profesora","sifra_predmeta") values('7','nebojsa.raicevic@elfak.rs','A888B');
insert into "predaje_predmet"("id","email_profesora","sifra_predmeta") values('4','emina.milovanovic@elfak.rs','A777B');
insert into "predaje_predmet"("id","email_profesora","sifra_predmeta") values('3','milos.bogdanovic@elfak.rs','A111B');
insert into "predaje_predmet"("id","email_profesora","sifra_predmeta") values('5','emina.milovanovic@elfak.rs','A555B');
insert into "predaje_predmet"("id","email_profesora","sifra_predmeta") values('0','aleksandar.stanimirovic@elfak.rs','A666B');
insert into "predaje_predmet"("id","email_profesora","sifra_predmeta") values('8','nebojsa.raicevic@elfak.rs','A999B');
insert into "predaje_predmet"("id","email_profesora","sifra_predmeta") values('2','milos.bogdanovic@elfak.rs','A444B');
insert into "predaje_predmet"("id","email_profesora","sifra_predmeta") values('1','aleksandar.stanimirovic@elfak.rs','A222B');


// predmet table data.
insert into "predmet"("sifra_predmeta","espb","naziv_predmeta","semestar","smer") values('A666B','4','Baze Podataka','4','RII');
insert into "predmet"("sifra_predmeta","espb","naziv_predmeta","semestar","smer") values('A222B','6','Mikroracunarski Sistemi','5','RII');
insert into "predmet"("sifra_predmeta","espb","naziv_predmeta","semestar","smer") values('A444B','5','Teorija Grafova','4','RII');
insert into "predmet"("sifra_predmeta","espb","naziv_predmeta","semestar","smer") values('A999B','6','Analogna Elektronika','3','ELK');
insert into "predmet"("sifra_predmeta","espb","naziv_predmeta","semestar","smer") values('A888B','6','Osnovi Elektronike','5','ELK');
insert into "predmet"("sifra_predmeta","espb","naziv_predmeta","semestar","smer") values('A111B','6','Operativni Sistemi','5','RII');
insert into "predmet"("sifra_predmeta","espb","naziv_predmeta","semestar","smer") values('A777B','6','Programski Prevodioci','8','RII');
insert into "predmet"("sifra_predmeta","espb","naziv_predmeta","semestar","smer") values('A555B','4','Struktura Predmeta','5','RII');
insert into "predmet"("sifra_predmeta","espb","naziv_predmeta","semestar","smer") values('A333B','5','Diskretna Matematika','3','RII');


// prijave_ispita table data.
insert into "prijave_ispita"("id","rok_id","email_studenta","naziv_sale","sifra_predmeta") values('21','1','nata@elfak.rs',NULL,'A444B');
insert into "prijave_ispita"("id","rok_id","email_studenta","naziv_sale","sifra_predmeta") values('18','1','culinjo@elfak.rs',NULL,'A111B');
insert into "prijave_ispita"("id","rok_id","email_studenta","naziv_sale","sifra_predmeta") values('20','1','nata@elfak.rs',NULL,'A222B');


// profesor table data.
insert into "profesor"("email","br_telefona","ime","kancelarija","prezime") values('milos.bogdanovic@elfak.rs','0666666','Milos','L2','Bogdanovic');
insert into "profesor"("email","br_telefona","ime","kancelarija","prezime") values('emina.milovanovic@elfak.rs','06060606','Emina','L1','Milovanovic');
insert into "profesor"("email","br_telefona","ime","kancelarija","prezime") values('aleksandar.stanimirovic@elfak.rs','0777777','Aleksandar','L2','Stanimirovic');
insert into "profesor"("email","br_telefona","ime","kancelarija","prezime") values('nebojsa.raicevic@elfak.rs','061061061','Nebojsa','323','Raicevic');


// rok table data.
insert into "rok"("id","pocetak_roka","kraj_prijave","naziv","pocetak_prijave","zavrsetak_roka") values('0','2021-07-21','2021-07-20','JUN2021','2021-07-10','2021-07-30');
insert into "rok"("id","pocetak_roka","kraj_prijave","naziv","pocetak_prijave","zavrsetak_roka") values('2','2022-01-14','2022-01-10','DECEMBAR021','2021-12-12','2022-02-27');
insert into "rok"("id","pocetak_roka","kraj_prijave","naziv","pocetak_prijave","zavrsetak_roka") values('1','2022-02-16','2022-02-15','JANUAR2022','2022-01-14','2022-02-27');


// sala table data.
insert into "sala"("naziv","kapacitet","sprat") values('A2',50,2);
insert into "sala"("naziv","kapacitet","sprat") values('A3',50,1);
insert into "sala"("naziv","kapacitet","sprat") values('535',20,5);
insert into "sala"("naziv","kapacitet","sprat") values('A4',60,1);
insert into "sala"("naziv","kapacitet","sprat") values('525',20,5);
insert into "sala"("naziv","kapacitet","sprat") values('A1',100,1);


// satnica table data.
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('6','0','2021-07-27','535','A777B','14:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('16','1','2022-02-16','A2','A888B','13:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('26','2','2022-01-27','tbd','A999B','09:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('19','2','2022-01-24','tbd','A111B','13:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('13','1','2022-02-23','A1','A555B','09:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('21','2','2022-01-25','tbd','A444B','13:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('7','0','2021-07-26','A1','A888B','14:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('17','1','2022-02-17','A3','A999B','09:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('9','1','2022-02-17','A1','A111B','09:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('15','1','2022-02-17','535','A777B','14:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('10','1','2022-02-18','A2','A222B','13:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('4','0','2021-07-25','A4','A555B','13:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('3','0','2021-07-22','A1','A444B','14:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('5','0','2021-07-26','525','A666B','14:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('25','2','2022-01-27','tbd','A888B','13:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('18','2','2022-01-23','tbd','A222B','13:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('0','0','2021-07-22','A1','A111B','14:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('14','1','2022-02-25','525','A666B','14:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('8','0','2021-07-27','A2','A999B','13:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('20','2','2022-01-23','tbd','A333B','09:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('2','0','2021-07-22','A3','A333B','14:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('22','2','2022-01-25','tbd','A555B','09:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('12','1','2022-02-22','A4','A444B','14:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('23','2','2022-01-26','tbd','A666B','09:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('24','2','2022-01-23','tbd','A777B','13:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('11','1','2022-02-17','A3','A333B','09:00');
insert into "satnica"("id","rok_id","datum","naziv_sale","sifra_predmeta","vreme") values('1','0','2021-07-23','A2','A222B','13:00');


// student table data.
insert into "student"("email","dugovanje","godinaupisa","ime","indeks","odobren","prezime","semestar","smer") values('mate@elfak.rs','0','2018','Mateja','17326',true,'Pancic','3','RII');
insert into "student"("email","dugovanje","godinaupisa","ime","indeks","odobren","prezime","semestar","smer") values('nata@elfak.rs','0','2018','Natalija','17321',true,'Pavlovic','7','RII');
insert into "student"("email","dugovanje","godinaupisa","ime","indeks","odobren","prezime","semestar","smer") values('saske@elfak.rs','0','2018','Aleksandra','17080',false,'Djokic','6','RII');
insert into "student"("email","dugovanje","godinaupisa","ime","indeks","odobren","prezime","semestar","smer") values('zovao@elfak.rs','0','2018','Zoran','17333',false,'Savic','7','ELK');
insert into "student"("email","dugovanje","godinaupisa","ime","indeks","odobren","prezime","semestar","smer") values('jovic@elfak.rs','0','2018','Marko','17128',true,'Jovic','7','ELK');
insert into "student"("email","dugovanje","godinaupisa","ime","indeks","odobren","prezime","semestar","smer") values('culinjo@elfak.rs','0','2018','Slavko','17345',true,'Petrovic','7','RII');


// zabranjena_prijava table data.
insert into "zabranjena_prijava"("id","datum_isteka","email_student","razlog","sifra_predmeta") values('0','2022-01-30','culinjo@elfak.rs','prepisivao','A666B');
insert into "zabranjena_prijava"("id","datum_isteka","email_student","razlog","sifra_predmeta") values('2','2022-01-11','mata@elfak.rs','psovanje profesora','A444B');
insert into "zabranjena_prijava"("id","datum_isteka","email_student","razlog","sifra_predmeta") values('1','2021-10-10','nata@elfak.rs','varanje na ispitu','A333B');
insert into "zabranjena_prijava"("id","datum_isteka","email_student","razlog","sifra_predmeta") values('3','2021-10-10','culinjo@elfak.rs','varanje na ispitu','A111B');


// counting_id table data.
insert into "counting_id"("tabela","counting") values('rok','17');
insert into "counting_id"("tabela","counting") values('polozeni_ispiti','17');
insert into "counting_id"("tabela","counting") values('predaje_predmet','17');
insert into "counting_id"("tabela","counting") values('satnica','27');
insert into "counting_id"("tabela","counting") values('zabranjena_prijava','17');
insert into "counting_id"("tabela","counting") values('obavestenje','17');
insert into "counting_id"("tabela","counting") values('prijave_ispita','22');


