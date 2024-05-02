
## API C# com Arquitetura Vertical Slice
## Estrutura do Projeto

- `Contracts/`: Modelos de dados para requisições e respostas.
- `Database/`: Configurações de banco de dados e mapeamentos do Entity Framework.
- `Domain/`: Entidades de domínio.
- `Endpoints/`: Definições dos endpoints da API.
- `Extensions/`: Métodos de extensão para configuração e Entity Framework.
- `Feature/Articles/`: Implementações das funcionalidades relacionadas a artigos.
- `Migrations/`: Migrações de banco de dados do Entity Framework.
- `Shared/`: Componentes compartilhados como erros e respostas padrão da API.
- `Program.cs`: Ponto de entrada da aplicação.

- ![image](https://github.com/gfmaurila/poc.vertical.slices.net8/assets/5544035/1ce7897a-9588-48ad-b2dc-784ed13ea030)
- ![image](https://github.com/gfmaurila/poc.vertical.slices.net8/assets/5544035/b32fefd6-3e3d-4e64-bb3a-6a86d8f6abaa)
- ![image](https://github.com/gfmaurila/poc.vertical.slices.net8/assets/5544035/7ad3089e-5f01-471e-b6b5-232e82a3c27f)
- ![image](https://github.com/gfmaurila/poc.vertical.slices.net8/assets/5544035/558d1b24-1156-459d-881b-f451fc0056f8)





## Configuração e Instalação

### Clonando o Repositório
Clone o repositório usando: https://github.com/gfmaurila/poc.vertical.slices.net8

### Configurando o Docker e Docker Compose
docker-compose up --build
http://localhost:5075/swagger/index.html

### SQL Server
- Add-Migration Inicial -Context EFSqlServerContext
- Update-Database -Context EFSqlServerContext

## Youtube
- ......

## Autor

- Guilherme Figueiras Maurila

## 📫 Como me encontrar
- [![YouTube](https://img.shields.io/badge/YouTube-FF0000?style=for-the-badge&logo=youtube&logoColor=white)](https://www.youtube.com/channel/UCjy19AugQHIhyE0Nv558jcQ)
- [![Linkedin Badge](https://img.shields.io/badge/-Guilherme_Figueiras_Maurila-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/guilherme-maurila)](https://www.linkedin.com/in/guilherme-maurila)
- [![Gmail Badge](https://img.shields.io/badge/-gfmaurila@gmail.com-c14438?style=flat-square&logo=Gmail&logoColor=white&link=mailto:gfmaurila@gmail.com)](mailto:gfmaurila@gmail.com)
- 📧 Email: gfmaurila@gmail.com


