# MarfCompraGado

## Instruções para executar o Projto

Baixar todos os arquivos do Github no seguinte endereço, clicando na opção 'Clone or download': 
https://github.com/AlmirFerreira/MarfCompraGado.git

Mover os seguintes arquivos de Banco de Dados, que foram baixados, para a raiz do drive C: ficando da seguinte maneira:
- c:\Marfrig.mdf
- c:\Marfrig_log.ldf

## Sequencia de execução ##
- Abrir e executar primeiro o projeto Web.API (MarfrigCompras.WebApi.sln);
- Verificar a URL que ele está sendo executado no Navegador;
- Abrir o projeto Windows Form (MarfrigCompras.sln);
- Alterar a constante que informa onde o Web.Api está rodando, no projeto Windows Form, substituindo o endereço "http://localhost:49585/" pelo endereço onde o Web.Api está sendo executado no seu navegador. Essa constante chama-se 'MarfrigApiHostAddress' e está no projeto MarfrigCompras (Windows Form) na Classe 'Constants.cs' na raiz da Solution.
- Executar o projeto Windows Form.

## Importante
Ambos os Projetos foram desenvolvidos no Visual Studio 2017 e o Projeto MarfrigCompras.WebApi foi desenvolvido com tecnologia Asp Net Core.
