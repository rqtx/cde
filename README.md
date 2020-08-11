# Central de Erros

Um simples projeto de uma central de erros para entender melhor o Framework .NetCore e a linguagem C#.

## Setup

Para executar o projeto localmente siga os passos a seguir:

- Inicia o container do banco de dados. Entre na pasta ./dbscripts e execute o comando:

	`(cd ./dbscripts && docker-compose up db-postgres)`

- Inicia a API:

	`(cd src/Cde.Api/ && dotnet run)`

- Para executa os tests:

	`(cd src/Cde.Tests/ && dotnet test)`

## Documentação API

A documentação com os endpoints da api pode ser encontrado na pasta ./docs/Endpoint

[Endpoint user](./docs/Endpoints/UserEndpoint.md)

[Endpoint level](./docs/Endpoints/LevelEndpoint.md)

[Endpoint system](./docs/Endpoints/SystemEndpoint.md)

[Endpoint log](./docs/Endpoints/LogEndpoint.md)
