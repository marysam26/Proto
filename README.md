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
* Reviewers use the same code as students in order to enlist in a class (the classroom code). This will not prevent a student from having the ability to register as a reviewer. This was discovered after the most recent testing phase and a fix has not yet been implemented. A possible fix is to have teachers generate a reviewer code for reviewers, similar to the one the system administrator generates for teachers. The reviewer can then use the course code and the given reviewer code to enlist in a course.
* Currently, when a teacher tries to register as a reviewer from the teacher page or the reviewer tries the register as a teacher from their page, the user is redirected to a not found webpage. This is due to a race condition and an authentication condition using roles that has yet to be rectified. If the users logs off of WriteItUp and then signs back in, they will have permission to access both the reviewer and the teacher page.
* Currently, our delete buttons are not functioning correctly due to a JQuery issues. The issue seems related to either not locating the JQuery class or a problem in one of our JQuery scripts. This error may have to do with refactoring WriteItUp as it may have unintentionally changed part script code. Once the JQuery is corrected and the buttons are responsive, they should correctly delete the item they are associated with as the backend code is still correct and functioning.



