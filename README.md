# SSO-BASE
### O que é?
API com estrutura básica de segurança para Microserviços
### Para que serve?
Facilitar a implentação de camada de segurança que poderá ser consumida por outras aplicação. Essa camada é responsável por fazer a gestão de usuários, permissões e políticas.
### Tecnologias Utilizadas
  - .NET CORE 3.1
  - Microsoft Identity Server
  - JWT
  - MongoDb
  - Swagger
  - Docker
  - Healthcheck
  - Testes de Integração
  - BDD
  
### Como usar
Para utilização durante o desenvolvimento local pode ser executado no Docker.
  1. Criando a rede interna docker.
  ```bash
     docker network create --driver bridge rede-integrada
  ```
  2. Criando duas instâncias do banco de dados em containes, sendo que o primeiro será usado ela aplicação e o segundo será usado para testar os recursos. 
  ```bash
docker run --name mongo -p 27017:27017 -d --network rede-integrada --network-alias mongo-local bitnami/mongodb:latest && 
docker run --name mongo -p 28017:27017 -d --network rede-integrada --network-alias mongo-local-teste bitnami/mongodb:latest

  ```
  3. Criando a Imagem e executando o container com API.
  ```bash
      docker-compose up --build
  ```
  4. Dashboard do healthcheck está disponível em http://localhost:2000/healthchecks-ui#/healthchecks
