# Asp.net Core Multi Layer (Beta) in progress...

Asp.net Core 1.1.0 With EntityFrameWorkCore 1.1.0

Customize and ready to use as a framework web application multi languages


##for Create Database:
#####setup your custom connectionString in appsetting.json in SampleFive.Web
#####run CMD from location of SampleFive.DataLayer
######and Enter this commands(one by one):
```
dotnet ef --startup-project ../SampleFive.Web/ migrations add NameOfMigration
dotnet ef --startup-project ../SampleFive.Web/ database update
```
##Features
```
1.Dependency Injection (ASP.net Core & StructureMap)
2.Multi-Layer Base
3.IoC
4.Add ASP.net MVC
5.ViewComponents
6.Replace HtmlHelper by TagHelper
7.Project Structure = Features
8.Setup MVC routing
9.ExternalResources
10.Setup Controllers , Views , ViewModels use Resources for Multi Languages
11.RTL Bootstratp
12.Site.css & Site.rtl.fa.css Dynamic choose when change Langauge
13.Add Asp.net Core Identity And Customize (ApplicationUser,...)
14.Setting Fluent Api
15.Add Captcha to forms
```

###Problem(s) in Progress:
 13.Add Asp.net Core Identity And Customize (ApplicationUser,...)
 


