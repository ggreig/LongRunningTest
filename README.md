# LongRunningTest

Coding test for a job application process - no names mentioned, to reduce searchability.

I haven't personally set up these project types before, nor Docker, Swagger or meesaging, and 
as a result I haven't made the progress I'd have liked.

I prioritised making sure that I could run something (the API) in Docker and document it;
because although Docker and Swagger were extra score options I thought:
* Building scalably to run processes on different hosts would be essential
* Documentation of an API is a fundamental
* With built-in support in the projects and Visual Studio I should be able to make a quick start
* Even if I messed it up I'd learn more from this approach.

Although working out some of the details took some time, I think it paid off reasonably.

Then I focussed on the Worker Service thinking I could set it up initially as a project dependency
using DI in the API project, then separate it out; I started with the template Worker Service and
got it running in Docker, then through DI from the API project (later removing the Docker support).

Having done that I tried to work out solutions from examples in Microsoft documentation 
(scoped services, queue services). I think that was my biggest mistake; I expect the sensible 
solution is to use the Web-Queue-Worker architecture style 
(https://learn.microsoft.com/en-us/azure/architecture/guide/architecture-styles/web-queue-worker)
and I should have tried to find an appropriate third party message queuing solution earlier to 
handle communications between scalable processes.

If I'd got to the database functionality I'd have used EF core, and if there had been time left
I could also have attempted an Angular UI.

The LongRunningWorkerService project contains both the "LongRunningWorker" (consumed by the
API project) and a "QueuedHostedService" which will be used if the project is run independently.
Neither are a satisfactory solution.

