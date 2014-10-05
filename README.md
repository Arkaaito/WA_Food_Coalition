WA_Food_Coalition
=================

This app was created at the GiveCamp 2013 event, and was continued in the 2014 event.

The intent here is fairly simple: make it easy for people (donors) to notify the Washington
Food Coalition of food donations that can be picked up by one of the members of the Coalition.
Coalition members can then schedule a pickup time, pick up the food, and mark the pickup
as completed.

As of the end of the GiveCamp 2014 event, it's not quite entirely completed--there are a few things that really should be finished before this app can be unleashed upon the world. But we're getting closer.

## Architecture

The server is an ASP.NET MVC project, consisting of a set of API endpoints (as described in the GitHub wiki page "Server API Reference"), which allows for easier cross-platform access, and a set of MVC forms for the CRUD-like access to the tables. Currently two clients are in place, the Web front-end that is intended to (eventually) be a admin and member UI, and the Android app that is intended to be the client (donor) UI. The database is SQL Server, arranged into three tables (Donors, Members, and Pickups).

## Technology

The ASP.NET MVC project is an MVC 5 project, using Entity Framework to access SQL Server.

The Android project is a straight Java Android SDK project, using Ant (not Gradle).

## GiveCamp 2014 Team
Stephanie Kardos (Product Owner)
Jack Pines
Audrey Smiley
Scott Slack
Ted Neward

## Work Remaining

Make sure to look at the [stories](https://github.com/Arkaaito/WA_Food_Coalition/wiki/Stories)
for the list of work identified as part of the 2014 project.

### Web/admin-UI project

* Needs to be auth-only access from the front page
* Needs to restrict member-based access to only see the pickups within their range
* Needs to restrict member-based access to only see pickups they have scheduled
* Needs to allow a member to cancel a scheduled pickup

### Android app

* Needs to allow a donor to edit/delete their profile


