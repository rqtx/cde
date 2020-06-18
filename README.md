# Central de Erros

## Testes

Dentro da pasta do projeto de teste (src.Test) instale os pacotes com os comandos a seguir:

``` 
	dotnet add package xunit --version 2.4.1
	dotnet add package xunit.runner.visualstudio --version 2.4.2
	dotnet add package Microsoft.AspNetCore.TestHost --version 3.1.5
	dotnet add package FluentAssertions --version 5.10.3
```

Para entender como escrever os testes utilizando o FluentAssertions veja sua [documentação](https://fluentassertions.com/introduction).

A classe BaseTest faz a inicialização do Host de teste através do pacote Microsoft.AspNetCore.TestHost. Devido a isso as classes que executarão os testes devem herdar a classe BaseTest para que seja possível chamar a rota desejada através do atributo Client que é criado na classe BaseTest. Para entender melhor veja a classe de teste PingControllerTest.cs no projeto src.Test que faz o teste da rota api/ping (PingController).

Referências
https://medium.com/@higorluis/testes-de-integra%C3%A7%C3%A3o-com-net-core-2-1-xunit-e-fluentassertions-a64bf65084ab
