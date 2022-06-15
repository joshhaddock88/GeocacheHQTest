# GeocacheHQTest

> This project was created as part of a skills assessment test for GeocachingHQ. This project is a data source built using Entity Framework Core and MVC architecture in .NET6.0.

## Table of Contents

* [General Info](#general-information)
* [Technologies Used](#technologies-used)
* [Features](#features)
* [Screenshots](#screenshots)
* [Setup](#setup)
* [Usage](#usage)
* [Project Status](#project-status)
* [Room for Improvement](#room-for-improvement)
* [Acknowledgements](#acknowledgements)
* [Contact](#contact)
* [License](#license)

## General Information

* This is an API used to simulate a basic RESTful geocaching API. In it the user is able to add a geocache to the database with name and cordinate properties. They will also be able to add item objects to the database and assign those items to a specific cache.
* This application allows a user to store geocache objects and their associated items. It also puts limits on the number of items with a specific geocache. Each geocache has a collection of active items that belong to it, and will update it's collection as items are moved from one geocache to another. If an item become inactive, the geocache will automatically remove that item from it's collection.
* The purpose of this project was to demostrate competency with database architecture and functionality.

## Technologies Used

* Entity Framework Core
* ASP.NET6.0
* Visual Studio 2022

## Features

* Simple, bootstrapped view, allowing for easy access to data and testing functionality.
* Takes advantage of collections to automatically update item counts within each cache.
* Items automatically become inactive a set number of days after their last activity.

## Screenshots

![Example screenshot](./img/screenshot.png)
<!-- If you have screenshots you'd like to share, include them here. -->

## Setup

Project must be run in ASP.NET6.0 using Visual Studio 2022.
Install the following packages before starting:

* MicrosoftAspNetCore.Diagnostics.EntityFrameworkCore
* Microsoft.EntityFrameworkCore
* Microsoft.EnttiyFrameworkCore.SqlServer
* Microsoft.EntityFrameworkCore.Tools

## Project Status

Project is: _in progress_ / _complete_ / _no longer being worked on_. If you are no longer working on it, provide reasons why.

## Room for Improvement

Include areas you believe need improvement / could be improved. Also add TODOs for future development.

Room for improvement:

* Improvement to be done 1
* Improvement to be done 2

To do:

* Feature to be added 1
* Feature to be added 2

## Acknowledgements

Give credit here.

* This project was inspired by...
* This project was based on [this tutorial](https://www.example.com).
* Many thanks to...

## Contact

Created by [Joshua Haddock](https://www.linkedin.com/in/joshuahaddock/) - feel free to contact me!
