# RedOnion
Visual Studio Template for Clean Architecture

https://devblogs.microsoft.com/dotnet/how-to-create-your-own-templates-for-dotnet-new/

# What is Clean Architecture?
Organizing software to manage complexity and simplify the addition of new features has been a challenge that many have attempted to solve. There are several models that have been proposed such as Hexagonal Architecture (Ports & Adapters), Screaming Architecture, Onion Architecture and more. This post is focussed on Onion Architecture. Onion Architecture has several layers. The inner most layer is the Application Core. It contains the business logic of the application. The Application Core does not depend on anything else. The outer layers are responsible for persistence, presentation, exposing services etc.. Each of these layers depend on the Application Core as needed. A dotnet starter template has been created to demonstrate the implementation of this architecture.

![]( https://secureservercdn.net/45.40.144.60/l5j.f95.myftpupload.com/wp-content/uploads/2019/08/Screen-Shot-2019-08-01-at-12.25.59-PM.png )

# What the template does?
Inline with the Onion Architecture that was described earlier, the template creates a visual studio project structure. The projects themselves are classified under INFRASTRUCTURE and PERSISTENCE refer to CORE. As noted earlier projects in CORE does not depend on anything else. More detail is in the table below.

| PROJECT	| PLATFORM | 	FUNCTION | REFERENCES |
|---------|----------|-----------|------------|
| PRESENTATION/API	| dotnet core, asp.net core webapi | API frontend for the application |	CORE/APPLICATION, INFRASTRUCTURE /PERSISTENCE |
| CORE/DOMAIN |	dotnet standard, class library |	Holds Entities, Value Objects, Interfaces etc… | |
| CORE/APPLICATION |	dotnet standard, class library | Holds business logic, the meat of the application. Functionality is typically divided into commands, queries and the events |	CORE/DOMAIN |
| INFRASTRUCTURE/PERSISTENCE | dotnet standard, class library |	Code that interacts with databases. Entity Framework for now. |	CORE/DOMAIN |
| INFRASTRUCTURE/DEPLOYMENT |	dotnet standard, class library |	ARM templates to deploy to Azure.	| |
| TESTS/* |	dotnet core, class library |	XUNIT Tests projects that reference the above appropriately.	| |

# How to install it?
To install the template clone it from github here. To install it do the below.

Remove git bindings
*rm -rf .git*

Install the template
*dotnet new —install path/to/folder/containing/.template.config*

This will install the template. To verify try dotnet new -h. RedOnion should now be a new type of project that you can create.

# How to use it?
To use the template create a new project from the command line.

*dotnet new redonion -n SampleApp*

This will create a solution with the structured described earlier. Since the solution is created out of a template the solution names, project names and the namespaces are named with SampleApp. You should be able to now build and run the solution.

Beyond this, most of the code shall go into the Application project. Assuming that some amount of event storming has already been done, you are now ready to organize your code around features. Features contain the commands, queries and events along with their respective handlers that implement that feature. As a soft convention each feature maps to a controller in the API project. Each command / query maps to a API method in the controller.

As an example if my application is responsible for scheduling jobs to run at some time in the future. Then my Application Project has the following sample structure.

![]( https://secureservercdn.net/45.40.144.60/l5j.f95.myftpupload.com/wp-content/uploads/2019/07/sample-app-layout-400x363.png )
