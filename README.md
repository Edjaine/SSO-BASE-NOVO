<h1>SSO-BASE</h1>
<h5>O que é?</h5>
<p>API com estrutura básica de segurança para Microserviços</p>
<h5>Para que serve?</h5>
<p>Facilitar a implentação de camada de segurança que poderá ser consumida por outras aplicação. Essa camada é responsável por fazer a gestão de usuários, permissões e políticas</p>
<h5>Tecnologias Utilizadas</h5>
<ul>
  <li>.NET CORE 3.1</li>
  <li>Microsoft Identity Server</li>
  <li>JWT</li>
  <li>MongoDb</li>
  <li>Swagger</li>
  <li>Docker</li>
</ul>
<h5>Como usar</h5>
<p>Para utilização durante o desenvolvimento local pode ser executado no Docker</p>
<ul>
  <li>1 - Criando a rede interna docker >> 
     <strong>docker network create --driver bridge rede-integrada  </strong></li>
  <li>2 - Criando o banco de dados em uma container >> 
    <strong>docker run --name mongo -p 27017:27017 --network rede-integrada --network-alias mongo-db   bitnami/mongodb:latest   </strong></li>
  <li>3 - Criando a Imagem e executando o container com API >> 
      <strong>docker-compose up --build </strong></li>
</ul>
