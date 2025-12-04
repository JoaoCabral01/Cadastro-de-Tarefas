# Cadastro de Tarefas – Aplicação WPF

> Uma aplicação simples, rápida e elegante para gerenciamento de tarefas, desenvolvida em **C# / WPF** com o padrão **MVVM** e design inspirado nas cores da **Williams Racing**.

---

## Sobre o Projeto

O **Cadastro de Tarefas** é um app desktop criado para facilitar o gerenciamento diário de atividades.  
Ele traz uma interface moderna e limpa, seguindo o padrão **MVVM** para um código mais organizado, e utiliza **SQLite** para armazenar os dados localmente.

As cores foram escolhidas com base no estilo visual da **Williams Racing**, garantindo um tema harmonioso, leve e agradável.

---

## Tema Inspirado na Williams Racing

A interface utiliza tons baseados na identidade visual da equipe, como:

- **Azul escuro — #0A1A2F**
- **Azul claro — #00A3E0**
- **Detalhes em branco para contraste**

Esse conjunto cria um visual refinado e esportivo, trazendo personalidade à aplicação.

---

## 📂 Estrutura do Projeto

📁 CadastroDeTarefas
┣ 📁 Helpers
│ ┗ 📄 RelayCommand.cs
┣ 📁 Models
│ ┗ 📄 Tarefa.cs
┣ 📁 Services
│ ┗ 📄 BancoDeDadosService.cs
┣ 📁 ViewModels
│ ┗ 📄 MainViewModel.cs
┣ 📁 Views
│ ┗ 📄 MainWindow.xaml
┣ 📄 App.xaml
┣ 📄 README.md



---

## Funcionalidades

### ✔ Adicionar tarefas
Campo de entrada com suporte total a caracteres acentuados (ex: **ação**, **análise**, **tarefa concluída**).

### ✔ Excluir tarefas
Cada item possui um botão próprio para exclusão.

### ✔ Marcar como concluída
Botão **“Já fiz”** envia a tarefa automaticamente para a lista de concluídas.

### ✔ Banco de dados local
O projeto utiliza **SQLite**, mantendo suas tarefas salvas mesmo após encerrar o app.

### ✔ Interface visual moderna
Cores, tipografia e layout inspirados na Williams Racing, com elementos alinhados e responsivos.

---

## Interface (Screenshots)

<img width="1918" height="1031" alt="Screenshot 2025-12-03 015656" src="https://github.com/user-attachments/assets/16363408-020c-434c-8529-52b47a815199" />

## MVVM + RelayCommand

O projeto segue fielmente o padrão **MVVM**, separando interface, lógica e dados.

### Exemplo do comando usado no ViewModel:

```csharp
public partial class RelayCommand : ICommand
{
    private readonly Action<object> _execute;

    public RelayCommand(Action<object> execute)
    {
        _execute = execute;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter) => _execute(parameter);
}
```
---

## Banco de Dados (SQLite)

### O serviço BancoDeDadosService é responsável por:

• Criar a tabela de tarefas, caso não exista;

• Inserir novas tarefas;

• Listar todas as tarefas armazenadas;

• Remover tarefas pelo Id.

• Simples, leve e eficiente.

### Tecnologias Utilizadas

• C#

• WPF / XAML

• MVVM

• SQLite

• .NET 6/7

---
  
## Como Executar o Projeto
### Clone o repositório

git clone https://github.com/seu-usuario/CadastroDeTarefas.git

### Abra no Visual Studio

Abra a solução .sln. 🠒 Execute 🠒 Pressione F5 para rodar.





