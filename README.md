![Your Logo Text](http://ezekiel.vancouver.wsu.edu/~cs421/readme/logo.png)

Meeting with Dr. Dunn next week to discuss logo design options as well as change in project name

# Proto

Proto is an interactive educational application designed to improve student quality of reading, writing and spelling skills.

[Dr. Michael Dunn](http://education.wsu.edu/directory/faculty/dunnm)

## Authors

[Casey Gilray](mailto:cgilray@gmail.com)

[Katie Gonzalez](mailto:kathrynn.gonzalez@gmail.com)

[Marysa McKay](mailto:marysam26@gmail.com)

[Michael Meyer](mailto:mm4223@yahoo.com)

[Tina Roper](mailto:troper17@comcast.net)

## Current Status

The project has a front end that is mostly finished. There are still some features and models/controllers that need implemented. Some tweaking to align with design mockups needs to be done as well.

The project is ready for database implementation in order to utilize the functionality of the front end. Once the database is connected, phase 1 of testing begins where we are looking for major flaws in design & programming.

## Build / Install

There are no build or install instructions at this time.

* Platform and System Dependencies: ...

* Needed Tools: CKEditor, ...

## Configuration and Run Info

There are no configuration or run instructions at this time.

### Structure of repo

Centralized

### Main components

* Model View Controller
* ASP .NET

### Dependencies, Tools, Libraries needed

* Use of Microsoft Azure for deployment
* RavenHQ
* RavenDB

### Git access

* Everyone is able to check out and push to the development branch. Marysa manages any merge conflicts.

## Known Bugs / Caveats

* Bug in login, where calling User.Identity.GetUserId() is returning an id for a different user other than the one you logged in with. A log out link would solve this probably for testing if that can be implemented. Maybe even a logout command that is called before the login starts to parse data...

* We had a bug where if we were logged in it would not let you get back to the home page to log in for another user.  Currently we have the commented out so that now you always have to log in. 

## TODO

* Implement database dependent functionalities.
* Implement timer for planning pages.
* Confirmation email if possible to let Dr. Dunn know when he needs to approve a request.
* Admin page is implemented but needs to be linked to UI.
