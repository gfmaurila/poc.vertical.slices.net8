
## API C# Vertical Slice Architecture

<div>
    <h2>Estrutura do Projeto</h2>
    <ul>
        <li>`Infrastructure/Database/`: Configurações de banco de dados e mapeamentos do Entity Framework.</li>               
        <li>`Domain/`: Entidades de domínio.</li>
        <li>`Endpoints/`: Definições dos endpoints da API.</li>
        <li>`Extensions/`: Métodos de extensão para configuração e Entity Framework.</li>
        <li>`Feature/`: Implementações das funcionalidades relacionadas a artigos.</li>
        <li>`Migrations/`: Migrações de banco de dados do Entity Framework.</li>
        <li>`Program.cs`: Ponto de entrada da aplicação.</li>
    </ul>    
</div>

<div>
    <h2>Vertical Slice Architecture</h2>
    <ul>
        <li>Event Sourcing</li>               
        <li>Repository Pattern</li>
        <li>Resut Pattern</li>
        <li>Domain Events</li>
    </ul>    
</div>

<div>
    <h2>Features</h2>
    <ul>
        <li>EASP.NET Core 8.0: Framework para desenvolvimento da Microsoft.</li>
        <li>Entity Framework</li>               
        <li>MediatR</li>
        <li>Mapster</li>
        <li>JWT auth</li>
        <li>Carter</li>
        <li>Ardalis Result</li>
        <li>Fluent Validation</li>
        <li>Swagger</li>
        <li>Serilog</li>
        <li>SQL Server</li>
        <li>Serilog</li>
        <li>Docker & Docker Compose</li>
        </ul>    
</div>

<div>
    <h2>Configuração e Instalação</h2>
    <table>
        <tr>
            <td>Clone o repositório usando:</td>
            <td>https://github.com/gfmaurila/poc.vertical.slices-full-stack-react.net8.git</td>
        </tr>
        <tr>
            <td>Configurando o Docker e Docker Compose</td>
            <td>docker-compose up --build</td>
        </tr>
    </table>    
</div>

<div>
    <h2>Configurando projeto</h2>
    <table>
        <tr>
            <td>Backend:</td>
            <td>http://localhost:5075/swagger/index.html</td>
        </tr>
        <tr>
            <td>Pasta:</td>
            <td>cd C:\Work\poc.vertical.slices-full-stack-react.net8</td>
        </tr>
        <tr>
            <td>Rodando a aplicação</td>
            <td>docker-compose up --build</td>
        </tr>
        <tr>
            <td>SQL Server</td>
            <td>
                <ul>
                    <li>Add-Migration Inicial -Context EFSqlServerContext</li>
                    <li>Update-Database -Context EFSqlServerContext</li>
                </ul>
            </td>
        </tr>
    </table>    
</div>



## API - Swagger

## API - Swagger - Auth

### 1.1 - POST - Login de Usuário via API
    ```
    curl -X 'POST' \
    'https://localhost:44375/api/v1/login' \
    -H 'accept: application/json' \
    -H 'Content-Type: application/json' \
    -d '{
    "email": "user@example.com",
    "password": "string"
    }'
    ```

### 1.2 - POST - Solicitar Redefinição de Senha via API
    ```
    curl -X 'POST' \
    'https://localhost:44375/api/v1/resetpassword' \
    -H 'accept: application/json' \
    -H 'Content-Type: application/json' \
    -d '{
    "email": "user@example.com"
    }'
    ```

### 1.3 - POST  - Redefinir Senha via API
    ```
    curl -X 'POST' \
    'https://localhost:44375/api/v1/newpassword' \
    -H 'accept: application/json' \
    -H 'Content-Type: application/json' \
    -d '{
    "password": "string",
    "confirmPassword": "string",
    "token": "string"
    }'
    ```

## API - Swagger - User

### 1.1 - POST - Criar Novo Usuário via API
    ```
    curl -X 'POST' \
    'https://localhost:44375/api/v1/user' \
    -H 'accept: application/json' \
    -H 'Content-Type: application/json' \
    -d '{
    "firstName": "string",
    "lastName": "string",
    "gender": 0,
    "notification": 0,
    "dateOfBirth": "2024-07-27",
    "email": "user@example.com",
    "phone": "string",
    "password": "string",
    "confirmPassword": "string",
    "roleUserAuth": [
        "string"
    ]
    }'
    ```

### 1.2 - GET - Obter Lista de Usuários via API
    ```
    curl -X 'GET' \
    'https://localhost:44375/api/v1/user' \
    -H 'accept: application/json'
    ```

### 1.3 - PUT - Atualizar Detalhes do Usuário via API
    ```
    curl -X 'PUT' \
    'https://localhost:44375/api/v1/user' \
    -H 'accept: application/json' \
    -H 'Content-Type: application/json' \
    -d '{
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "firstName": "string",
    "lastName": "string",
    "gender": 0,
    "notification": 0,
    "phone": "string",
    "dateOfBirth": "2024-07-27"
    }'
    ```

### 1.4 - DELETE - Excluir Usuário via API
    ```
    curl -X 'DELETE' \
    'https://localhost:44375/api/v1/user/fa49952f-e0c9-4ea4-aab0-2aaebe0275cc' \
    -H 'accept: application/json'
    ```

### 1.5 - PUT - Atualizar Email de Usuário via API
    ```
    curl -X 'PUT' \
    'https://localhost:44375/api/v1/user/updateemail' \
    -H 'accept: application/json' \
    -H 'Content-Type: application/json' \
    -d '{
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "email": "user@example.com"
    }'
    ```

### 1.6 - PUT - Atualizar Senha de Usuário via API
    ```
    curl -X 'PUT' \
    'https://localhost:44375/api/v1/user/updatepassword' \
    -H 'accept: application/json' \
    -H 'Content-Type: application/json' \
    -d '{
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "password": "string",
    "confirmPassword": "string"
    }'
    ```

### 1.7 - PUT - Permissões de Usuário via API
    ```
    curl -X 'PUT' \
    'https://localhost:44375/api/v1/user/updaterole' \
    -H 'accept: application/json' \
    -H 'Content-Type: application/json' \
    -d '{
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "roleUserAuth": [
        "string"
    ]
    }'
    ```




## Youtube
- ......

## Autor

- Guilherme Figueiras Maurila

## 📫 Como me encontrar
[![YouTube](https://img.shields.io/badge/YouTube-FF0000?style=for-the-badge&logo=youtube&logoColor=white)](https://www.youtube.com/channel/UCjy19AugQHIhyE0Nv558jcQ)
[![Linkedin Badge](https://img.shields.io/badge/-Guilherme_Figueiras_Maurila-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/guilherme-maurila)](https://www.linkedin.com/in/guilherme-maurila)
[![Gmail Badge](https://img.shields.io/badge/-gfmaurila@gmail.com-c14438?style=flat-square&logo=Gmail&logoColor=white&link=mailto:gfmaurila@gmail.com)](mailto:gfmaurila@gmail.com)

📧 Email: gfmaurila@gmail.com


