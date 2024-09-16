# ChatAppApi

This is backend codebase for a chat api. Sadly it is slightly theoretical as I've not managed to get the whole solution running. The design uses the following patterns:
* Domain Driven Design
* (some) Test Drive Development - i didnt have time to write full tests but gave some unit tests as a flavour of writing them.
* SOLID principles with the classes to help enable testing and the DDD elements above

In terms of technology I used:
* Sqlite
* EntityFramework Core to populate the above database as a local file to allow the thing to run
* .net core api project structure with latest c#
* SignalR to enable realtime chat posting and updates to other clients

Things I felt went well
I was happy with my project structure in the DDD sense with a good separation of concerns, also I think I had the entity structure correct. 

Things I'm not happy with
My knowledge of SignalR is 0 so tried to get that working, however to do so I needed a front end (which I do have as a separate project) but I never got the two elements to work together properly.
My EntityFramework knowledge is rather ancient and I fear my implementation here is a bit clumsy but I have it a go. 
I'm guilty of trying to fit a lot in, the user element could have been ditched and made more 'hardcoded' for demo purposes.

Things to improve
* Currently there is no logging but if this were a 'real' system I would be using something centralised like Sentry or Logrocket etc.
* I would itegrate this backend and the frontend with Azure Entra to manage users and permissions. I did consider that at the start but decided it would be too fiddly, hence the cut down local user classes and services
* More tests!
* I need to sit down and properly learn SignalR and Angular interaction as here its not working. 
