# Database

Para inicia o Postgresql execute na raiz do projeto o comando a seguir:

``` docker-compose up db ```

A configuração do PostgreSql está no arquivo .database/database.env.

## Cliete psql

Para interagir Postgresql devemos utiliza o cliente psql. Podemos utilizar outro container Postgresql ou executar o psql no host.

### Utilizando cliente psql em um container Postgresql

Ao utilizar outro container devemos iniciar um container Postgresql com o shell:

``` docker-compose run database bash ```

No shell do container devemos utilizar o cliente psql:

``` psql --host=db --username=postgres --dbname=cde ```

### Utilizando psql no host

``` psql --host=0.0.0.0 --username=postgres --dbname=cde ```

## Extra

Para remover o volume criado pelo container:

``` docker-compose down --volumes ```

## Referências

https://medium.com/analytics-vidhya/getting-started-with-postgresql-using-docker-compose-34d6b808c47c
