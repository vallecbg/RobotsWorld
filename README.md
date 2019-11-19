![alt text](https://previews.123rf.com/images/anatolir/anatolir1808/anatolir180810297/105975416-mechanical-robot-banner-flat-style.jpg)
# Project - Robots World

## Type - Site for Robot Assembly and Management

Demo: https://robotsworld.azurewebsites.net

## Demo Users
- Username: usertest
- Password: user123

- Username: admintest
- Password: admin123

## Description

RobotsWorld is the site where you can assembly your robots and manage their parts and assemblies.
There are 2 roles - User and Administrator.
- Users can Add, Delete and Edit Robots,
- Users can Create Assemblies,
- Users can Create SubAssemblies and view Details about them,
- Users can Add and Delete Parts,
- Users can Ship their own robots to other users,
- Users can Subscribe to the Newsletter using their e-mail addresses and receive notifications about the site,
- Users can use the Live Chat Room.
- Administrators have their own Admin Panel
- Administrators can see all of the information about the entities,
- In the Dashboard, Administrators can see information about the total Users, Robots, Vendors, Transport Types, etc.,
- In the Dashboard, Administrators can also see total deliveries this week and the top 3 robots with most deliveries,
- Administrators can Delete Users and change their roles,
- Administrators can Add, Delete Vendors,
- Administrators can Delete Robots,
- Administrators can Add, Delete Transport Types.

## Entities

### User
  - Id (string)
  - Name (string)
  - Username (string)
  - Robots (collection of Robot)
  - SentRobots (collection of Delivery)
  - ReceivedRobots (collection of Delivery)
### Robot
  - Id (string)
  - Name (string)
  - Serial Number (string)
  - Axes (int)
  - Assembly (Assembly)
  - User (User)
  - Deliveries (collection of Delivery)
### Assembly
> An ASSEMBLY is a combination of two or more sub assemblies joined to perform a specific function. A SUB ASSEMBLY consists of two or more parts that form a portion of an assembly.It can be replaced as a whole, but some of its parts can be replaced individually.
  - Id (string)
  - TotalPrice (calculated property - sum of the subassemblies PartsPrice and Quantity)
  - TotalWeight (calculated property - sum of the subassemblies Weight and Quantity)
  - Robot (Robot)
  - SubAssemblies (collection of SubAssembly)
### SubAssembly
  - Id (string)
  - Assembly (Assembly)
  - Name (string)
  - Quantity (int)
  - PartsPrice (calculated property - sum of the parts Price and Quantity)
  - Weight (double)
  - ImageUrl (string)
  - Parts (collection of Part)
### Part
  - Id (string)
  - Name (string)
  - Quantity (int)
  - Price (decimal)
  - SubAssembly (SubAssembly)
  - Vendor (Vendor)
### Vendor
  - Id (string)
  - Name (string)
  - Parts (collection of Part)
### Delivery
  - Id (string)
  - Sender (User)
  - Receiver (User)
  - Robot (Robot)
  - SentOn (DateTime)
  - Starting Point (string)
  - Destination Point (string)
  - Price (decimal)
  - Transport Type (TransportType)
### Transport Type
  - Id (string)
  - Name (string)
  - Deliveries (collection of Delivery)
### ChatRoomMessage
  - Id (string)
  - Published On (DateTime)
  - Content (string)
  - Username (string)
