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


* Platform and System Dependencies: 
    WriteItUp! operates on both Windows and Mac operating systems. 
    Testing has been completed for Windows 7 and above and Mac Mavericks and above.

* Needed Tools: CKEditor which is included in the project

* RavenDB:
    Install instructions can be found here:[Download and Install](http://ravendb.net/downloads/builds) and API documentation can be found here:
    [Documentation](http://ravendb.net/docs/article-page/2.0/csharp/client-api/connecting-to-a-ravendb-datastore)

## Configuration and Run Info

* Deployment Locally
    For teseting, WriteItUp can be deployted locally though an emulator within Visual Studio. Ensure RavenDB is running on port 8080 locally and the conecction string exists in the Web.config file. If no connection string exists for RavenDB for localhost:8080 obtain one by using the native RavenDB UI. Make sure that the document store in RavenRegistry.cs under Dependency Resoultion has the ConnectionStringName set to "RavenDB", or whatever you had named it in the connection string. Clicking run will build and deploy the project locally.

* Deployment on Azure:
    This project can be deployed on Microsoft Azure using RavenHQ as the database back end. First, ensure the connection string for ravenHQ exists in the Web.config file. If no connection string exists copy one from your     RavenHQ account into that file, and modify the RavenRegistry to use RavenHQ. Then, from the solution explorer right click the package and select publish. Publish to Azure Websites using your account  information.Follow the prompts. When the prompts are finished the website will be available at <Given>.azurewebsites.net

* Deployment on WSU servers.
    For deployment on the appropriated WSU server follow these steps. On a windows machine on campus using remote desktop, remote into van-capstone.vancouver.wsu.edu. Credentails can be obtained by speaking to Chuck Harrsh. Ensure IIS8. Ensure RavenDB is installed and running on port 8080, with connection string and RavenRegistry settings set similarily to deploying locally. In the server manager, select manage, then add roles and features. Under server roles select Web Server(IIS). On the next screen, ensure .Net framework 4.5 is included. Next, Select all Application Development features and add them. Comfirm these and allow the features to install. Refresh the manager. Select tools and open IIS. Copy WriteItUP into the c://...inetpub//wwwroot folder. Permissions will be automatically reassigned. Right click Sites and select Add Websites. Pick a site name and point the phyisical path to the directory in the wwwroot folder. Under host name enter "van-capstone.vancouver.wsu.edu". Select browse to see if the site is working properly at this point. The ip of the address will be 69.166.34.2




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
* There is no way for the SystemAdmin to remove an assignment.  Completely removing the assignment from the database would mess up features that rely on that assignment such as viewing past assignments, so it is likely a feature will need to be created to track the list of active assignments that can be currently assigned by the teacher, and the list of all legacy assignments which needs to exist for database consistency


