# Database

Para inicia o Postgresql execute na raiz do projeto o comando a seguir:

``` docker-compose up db ```

A configuração do PostgreSql está no arquivo dbscripts/database.env.

O script de inicialização do banco está em dbcripts/seed.sql. 
Para remover o volume criado pelo container:

``` docker-compose down --volumes ```

## Cliete psql

Para interagir Postgresql devemos utiliza o cliente psql. Podemos utilizar outro container Postgresql ou executar o psql no host.

### Utilizando cliente psql em um container Postgresql

Ao utilizar outro container devemos iniciar um container Postgresql com o shell:

``` docker-compose run db bash ```

No shell do container devemos utilizar o cliente psql:

``` psql --host=db --username=postgres --dbname=cde ```

### Utilizando psql no host

``` psql --host=0.0.0.0 --username=postgres --dbname=cde ```

https://medium.com/analytics-vidhya/getting-started-with-postgresql-using-docker-compose-34d6b808c47c

## Adicionando um Service

Crie uma nova class no projeto Cde.Models para o novo Model.

Crie um DbSet para o Model no ApplicationContext do projeto Cde.Database.

O projeto Cde.Database implementa uma classe genêrica com um CRUD básico chamada DatabaseService.cs, caso seja necessário implementar acessos ao banco específicos para um modelo deve-se criar um service para o modelo dentro da pasta Services que herda a classa DatabaseService.

Para o service buscar corretamento os dados no banco devemos criar um Map dentro da pasta Map e depois adicionar esse map no ApplicationContext.OnModelCreating, siga o exemplo do LogMap.

https://medium.com/front-end-weekly/net-core-web-api-with-docker-compose-postgresql-and-ef-core-21f47351224f





