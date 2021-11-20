# TodoList
> TodoList is my first self-made ASP.NET project, and first of all I use it for education purpose.

TodoList web-app made by ASP.NET Core. This web application have simple functionality for creation, editing and deleting todo-tasks.

# How to install

1. Clone this repository on local machine
2. Setup user secrets json file ([UserSecrets tutorial](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows)) so project file should looks like:
```
...

<PropertyGroup>
  <TargetFramework>net5.0</TargetFramework>
  <UserSecretsId>b461b0ab-069e-4ea8-8721-693e0794ccc8</UserSecretsId>
</PropertyGroup>

...
```
3. Clone external project "EmailService" from [here](https://github.com/whyme0/TodoList-EmailService) and make sure it is included to project properly. Default option in .csproj file:
```
...

<ItemGroup>
  <ProjectReference Include="..\EmailService\EmailService.csproj" />
</ItemGroup>

...
```
4. TodoList project is ready to use!

# Format of UserSecrets json file
```json
{
  "TodoList:EmailConfiguration" : {
    "Port": <your_port>,
    "SmtpServer": <your_smtp_server>,
    "Password": <your_password>,
    "From": <your_email_address>,
    "Username": <your_username>
  },
  "TodoList:ConnectionString": <your_connection_string>
}
```
> Often "From" and "Username" are the same
