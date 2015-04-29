![Your Logo Text](http://encs.vancouver.wsu.edu/~k.gonzalez/Write.jpg)

# WriteItUp!

WriteItUp! is an interactive educational application designed to improve student quality of reading, writing and spelling skills.

[Dr. Michael Dunn](http://education.wsu.edu/directory/faculty/dunnm)

## Authors

[Casey Gilray](mailto:cgilray@gmail.com)

[Katie Gonzalez](mailto:kathrynn.gonzalez@gmail.com)

[Marysa McKay](mailto:marysam26@gmail.com)

[Michael Meyer](mailto:mm4223@yahoo.com)

[Tina Roper](mailto:troper17@comcast.net)


## Current Status

Version 1.0 is complete and the testing phase has been completed.

## Build / Install

The current implementation deploys on Microsoft Azure using RavenHQ as the database back end. First, ensure the connection string for ravenHQ exists in the web.xml file. If no connection string exists copy one from your RavenHQ account into that file, and modify the Raven Authorization Dependency to use RavenHQ. Then, from the solution explorer right click the package and select publish. Publish to Azure Websites using your account information. Follow the prompts. When the prompts are finished the website will be available at <Given Name>.azurewebsites.net.

* Platform and System Dependencies: 
    WriteItUp! operates on both Windows and Mac operating systems. 
    Testing has been completed for Windows 7 and above and Mac Mavericks and above.

* Needed Tools: CKEditor which is included in the project

* RavenDB:
    Install instructions can be found here:[Download and Install](http://ravendb.net/downloads/builds) and API documentation can be found here:
    [Documentation](http://ravendb.net/docs/article-page/2.0/csharp/client-api/connecting-to-a-ravendb-datastore)

## Configuration and Run Info



### Main components

* Model View Controller
* ASP.NET
* HTML5

### Dependencies, Tools, Libraries needed

* Use of Microsoft Azure for deployment
* RavenHQ
* RavenDB
* CKEditor

## Known Bugs / Caveats



