# Identity Manager

API em ASP.NET Core para autenticação e autorização de usuários utilizando JWT (JSON Web Tokens) e políticas personalizadas.

---

## 🚀 Tecnologias

- ASP.NET Core 8.0  
- Entity Framework Core  
- MySQL  
- JWT Authentication  
- Políticas de autorização customizadas  
- User Secrets (para armazenamento seguro de configurações sensíveis)

---

## 📦 Funcionalidades

- Registro de usuário  
- Autenticação com geração de token JWT  
- Endpoint protegido com política baseada em idade mínima  
- Configuração com placeholders seguros no `appsettings.json`

---

## 🛠️ Como rodar o projeto localmente

### Pré-requisitos

- [.NET SDK 8.0+](https://dotnet.microsoft.com/en-us/download)  
- [MySQL](https://www.mysql.com/) rodando localmente ou em container  
- [DBeaver](https://dbeaver.io/) ou outra ferramenta de visualização de banco (opcional)

### Passos

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/anncarln/identity-manager.git
   cd identity-manager/IdentityManager
   ```

2. **Configure os secrets:**
    ```bash
    dotnet user-secrets init
  
    dotnet user-secrets set "JwtSettings:SecretKey" "your-super-secret-key"
    dotnet user-secrets set "ConnectionStrings:UserConnection" "your-connection-string"
    ```
    
3. **Execute as migrações do banco:**
   ```bash
    dotnet ef database update
   ```
   
4. **Rode a aplicação:**
   ```bash
    dotnet run
   ```
   
5. **Acesse o Swagger:**
   ```bash
    http://localhost:5167/swagger
   ```
---

## 🔐 Exemplo de token JWT
Ao fazer login com sucesso, você receberá um token JWT. Use-o no header Authorization:
  ```bash
    Authorization: Bearer seu_token_aqui
  ```
---

## 📌 Observações
- Os dados sensíveis devem ser armazenados com o dotnet user-secrets ou variáveis de ambiente.
- Os placeholders no appsettings.json são apenas para referência e devem ser sobrescritos em tempo de execução.
