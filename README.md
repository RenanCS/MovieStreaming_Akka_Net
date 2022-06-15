<h1 align="center">
  Movie Streaming With Akka.Net
</h1>

<h4 align="center">
	🚧  Movie Streaming ♻️ Concluído 🚀 🚧
</h4>

<p align="center">
 <a href="#-sobre-o-projeto">Sobre</a> •
 <a href="#-funcionalidades">Funcionalidades</a> •
 <a href="#-como-executar-o-projeto">Como executar</a> •
 <a href="#-tecnologias">Tecnologias</a> •
 <a href="#-problemas-encontrados">Problemas encontrados</a> •
 <a href="#user-content--licença">Licença</a>
</p>

## 💻 Sobre o projeto

👉O projeto consiste em compreender os fundamentos do sistema de atores e comunicação via mensagens da tecnologia <strong>Akka.Net</strong>. Além disso, o sistema cobre tolerância a falha com auto-cura através da hierarquia de supervisão.

👉Para definição e utilização de atores, podemos utilizar de duas formas: <i>UntypedActor ou ReceiveActor</i>, mas para ter a referência de um ator é necessário utilizar o <i>IActorRef</i> e podendo enviar mensagens podemos utilizar <i>Tell, Aks e Forward</i>.

👉Em cada ator, está sendo apresentado o ciclo de vida do mesmo, ao qual podemos acrescentar eventos dentro de <i>PreStart, PostStop, PreRestart e PostRestart</i>. Também é possível utilizar o <i>PoisonPill</i> para finalizar o processo dos atores ao final das mensagens.

👉Apresentei neste projeto, um exemplo de hierarquia, comunicação entre actor de outras hierarquias via <i>ActorSelection</i>, bem como, tipo de supervisão que um actor pai pode exercer sobre o filho como: <i>Resume, Restart, Stop e Escalate</i>.

Por último, criei um projeto para representar uma comunicação remota entre actores.


✅ .Net 5 <br/>
✅ Akka.Net <br/>
✅ Akka.Remote <br/>
---

## 🚀 Como executar o projeto

### Pré-requisitos

Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:
 [.Net core](https://dotnet.microsoft.com/en-us/download/dotnet/5.0).
Além disto é bom ter um editor para trabalhar com o código como [VSCode](https://code.visualstudio.com/), [Visuall Studio](https://visualstudio.microsoft.com/pt-br/downloads/)


---

## ❌Problemas encontrados

 
---

## 🛠 Tecnologias

- **[.NET](https://dotnet.microsoft.com/en-us/)**
- **[Akka.Net](https://getakka.net/)**






