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
  
### Como usar
Para utilização durante o desenvolvimento local pode ser executado no Docker.
  1. Criando a rede interna docker.
  ```bash
     docker network create --driver bridge rede-integrada
  ```
  2. Criando o banco de dados em uma container.
  ```bash
    docker run --name mongo -p 27017:27017 --network rede-integrada --network-alias mongo-db   bitnami/mongodb:latest
  ```
  3. Criando a Imagem e executando o container com API.
  ```bash
      docker-compose up --build
  ```
  4. Dashboard do healthcheck está disponível em http://localhost:2000/healthchecks-ui#/healthchecks
