# Projeto 01 - Linguagem Formal e Autômatos LAB

A partir de dados inputados pelo usuário (Regras de Produção, Alfabeto, Passos e Variável Inicial), o algoritmo gera a palavra formada (pertencente ao alfabeto), de acordo com os passos. 

## Alterando os dados

O projeto possui um arquivo .json no caminho <i>src/LFA_project1/config/userInput.json</i>, o qual contêm valores default para utilização. Caso queira alterar os dados a serem executados, mantenha o padrão e altere os dados.

## Executando o projeto

Estando dentro da pasta <i>src</i>, no diretório do projeto, execute os comandos abaixo. 
```
dotnet restore
dotnet run
```
Na saída de dados, será exibida a palavra <i>baba</i>.

## Executando os testes

Estando dentro da pasta <i>test</i>, no diretório do projeto, execute os comandos abaixo.

```
dotnet test
```
