# ChatAppApi

This is backend codebase for a chat Api. Sadly it is slightly theoretical as I've not managed to get the whole solution running. The design uses the following patterns:
* Domain Driven Design
* (some) Test Driven Development - I didn’t have time to write full tests but gave some unit tests as a flavour of writing them.
* SOLID principles with the classes to help enable testing and the DDD elements above

The general idea with this app is that the user account interaction would be done via an API controller with application layer services below that and a repository to the SQLite database. The chat rooms 'as a list' were to be available via an API call as well as the ability to create a new room. But the sending and receiving of messages, joining and leaving a room, were to be done via a SignalR Hub which would make use of the message and chatroom services below it to interact ultimately with the database. 

## Technology Used
In terms of technology I used:
* Sqlite
* EntityFramework Core to populate the above database as a local file to allow the thing to run
* .net core Api project structure with latest c#
* SignalR to enable real-time chat posting and updates to other clients

## Things I felt went well
I was happy with my project structure in the DDD sense with a good separation of concerns, also I think I had the entity structure correct. 

## Things I'm not happy with
* My knowledge of SignalR is 0 so tried to get that working, however to do so I needed a front end (which I do have as a separate project) but I never got the two elements to work together properly.
* My EntityFramework knowledge is rather ancient and I fear my implementation here is a bit clumsy. 
* I'm guilty of trying to fit a lot in, the user element could have been ditched and made more 'hardcoded' for demo purposes.
* I'm annoyed not to have it running properly.

## Things to improve
* Currently there is no logging but if this were a 'real' system I would be using something centralised like Sentry or Logrocket etc.
* I would integrate this backend and the frontend with Azure Entra to manage users and permissions. I did consider that at the start but decided it would be too fiddly, hence the cut down local user classes and services
* More tests! 
* I need to sit down and properly learn SignalR and Angular interaction as here its not working.
