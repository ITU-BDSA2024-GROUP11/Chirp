
# Chirp!

[**Design and architecture**](#design-and-architecture)

[Domain model](#domain-model)

[Architecture — In the small](#architecture-—-in-the-small)

[Architecture of deployed application](#architecture-of-deployed-application)

[User activities](#user-activities)

[Sequence of functionality/calls through Chirp!](#sequence-of-functionalitycalls-through-chirp)

[**Process**](#process)

[Build, test, release, and deployment](#build-test-release-and-deployment)

[Teamwork](#teamwork)

[How to make Chirp! work locally](#how-to-make-chirp-work-locally)

[How to run test suite locally](#how-to-run-test-suite-locally)

[**Ethics**](#ethics)

[License](#license)

[LLMs, ChatGPT, CoPilot, and others](#llms-chatgpt-copilot-and-others)

# Design and architecture 

## Domain model

*Provide an illustration of your domain model. Make sure that it is correct and complete. In case you are using ASP.NET Identity, make sure to illustrate that accordingly.*  
![Img0](Link_to_img0_here)
Our program uses ASP.NET Identity to handle user logins.

## Architecture — In the small

*Illustrate the organization of your code base. That is, illustrate which layers exist in your (onion) architecture. Make sure to illustrate which part of your code is residing in which layer.*  
![Img1](Link_to_img1_here)

## Architecture of deployed application

*Illustrate the architecture of your deployed application. Remember, you developed a client-server application. Illustrate the server component and to where it is deployed, illustrate a client component, and show how these communicate with each other.*

## User activities

As a non-authorized user, you are able to see the “Public Timeline” page, which is also the website's landing page. Under the “Chirp\!” logo you have the opportunity to navigate to a “Register” page and a “Log in” page. 

![Img2](Link_to_img2_here)

If you scroll to the bottom of the page you will see 4 buttons allowing you to switch between pages, or go directly to the first or last page. The “First” and “Previous” buttons are blocked when you are on the first page, and the same goes for the “Next” and “Last” buttons when you are on the last page.

![Img3](Link_to_img3_here)

When clicking on the register link, you enter a registration page, here you can create a new account. Either with an email address of your own choice, or you can choose to use GitHub when creating an account. You also have a link guiding you to the “Log in” page, if you already have an account. The website will give the user feedback, if the username is taken or the password does not meet the requirements.

![Img4](Link_to_img4_here)

After account creation, you are now logged in. A logged in user will see another version of the “Public Timeline” page. There is a box where you can cheep, all cheeps will end up on this page. You also have the opportunity to follow other users. If you cheep you will not see the “Follow” button next to you, but instead an “Edit” button. After clicking the “Edit” button, a new text input field will appear, along with the buttons “Save” and “Delete”. So it is possible for the user to either update the cheep or delete it. After editing a cheep, it will be displayed next to the timestamp, so other users know this cheep has been edited. 

![Img5](Link_to_img5_here)

A logged in user can also navigate to “My Timeline”. All of the users cheeps, and the cheeps of the users followed will be present here. As displayed, the user can also choose to unfollow.

![Img6](Link_to_img6_here)

If the user cheeps from this page, it will still appear on the “Public Timeline” page. The user can also cheep pictures or GIF’s by copying the corresponding image address and pasting the link as a cheep.

![Img7](Link_to_img7_here)

A logged in user will see a link called about me. This page lets you see all your information (username, email, following and cheeps made by the user). On this page, there also is a possibility to delete your account. 

![Img8](Link_to_img8_here)

It is at all times possible to logout by clicking the logout(username) link. If a user wants to log in again later, it is possible to do so by clicking the login link. Here the user can login with their username and password. If you have not created an account, but used github, you will need to use the “GitHub” button on this page as well. 

![Img9](Link_to_img9_here)

## Sequence of functionality/calls through Chirp\!

*With a UML sequence diagram, illustrate the flow of messages and data through your Chirp\! application. Start with an HTTP request that is send by an unauthorized user to the root endpoint of your application and end with the completely rendered web-page that is returned to the user.*

*Make sure that your illustration is complete. That is, likely for many of you there will be different kinds of "calls" and responses. Some HTTP calls and responses, some calls and responses in C\# and likely some more. (Note the previous sentence is vague on purpose. I want that you create a complete illustration.)*

# Process

## Build, test, release, and deployment

*Illustrate with a UML activity diagram how your Chirp\! applications are build, tested, released, and deployed. That is, illustrate the flow of activities in your respective GitHub Actions workflows.*

*Describe the illustration briefly, i.e., how your application is built, tested, released, and deployed.*

## Teamwork

*Show a screenshot of your project board right before hand-in. Briefly describe which tasks are still unresolved, i.e., which features are missing from your applications or which functionality is incomplete.*

*Briefly describe and illustrate the flow of activities that happen from the new creation of an issue (task description), over development, etc. until a feature is finally merged into the main branch of your repository.*

## How to make Chirp! work locally

To run the program locally you need to .net8 installed

To clone the repository to your computer you need to have git installed.  
Then run the command:  
`git clone [https://github.com/ITU-BDSA2024-GROUP11/Chirp.git](https://github.com/ITU-BDSA2024-GROUP11/Chirp.git)` 

After having cloned the repository locally to your machine you need to do the following steps, which are also in the readme.md file:  
From the root directory of the project in your termial run the following commands  
`cd src/Chirp.Web/`  
`dotnet user-secrets init`  
`dotnet user-secrets set  "authentication\_github\_clientId" "Ov23liN6Yjxe3rEIVpMB"`  
`dotnet user-secrets set  "authentication\_github\_clientSecret" "983419733e343552b15de88bbf4b5d170fa30420"`

When this is done you should be able to run the program from the same location using `dotnet run`

This should open your default browser and you will be able to use the program as desired.

## How to run test suite locally

This section assumes that you have successfully run the program, if you have not yet done that please refer to the section “How to make Chirp! work locally”.

To run tests locally you need to have powershell installed if you are not using powershell as your preferred shell  
On Linux: `sudo apt update && sudo apt install \-y powershell`  
On MacOS: `brew install \--cask powershell`  
On Windows: Skip this step if you are using powershell

From the root directory of the project run the following command to install playwright

`pwsh ./test/PlaywrightTests/bin/Debug/net8.0/playwright.ps1 install`

Now to run the tests simpy run the command  
`dotnet test`

# Ethics

## License

We are using an MIT license.

## LLMs, ChatGPT, CoPilot, and others

ChatGPT as well as CoPilot was used by some of the group members in assistance of developing the project. ChatGPT was used in different scenarios but never as the first measure.   
When it came to resolving problems with different package installations and similar issues it was very effective. However when trying to use it for directly code related issues the helpfulness varied a lot, some code related issues it would give a great solution to, whilst others it would provide a solution which we might not have fully understood which ended up giving us problems either when directly trying to implement it or, if changes later were necessary.

Taking everything into account the use of ChatGPT and CoPilot has probably sped up the process a bit.

*In case you were using an LLM to support your development, briefly describe when and how it was applied. Reflect in writing to which degree the responses of the LLM were helpful. Discuss briefly if application of LLMs sped up your development or if the contrary was the case.*

[https://github.com/itu-bdsa/lecture\_notes/blob/main/sessions/session\_12/README\_REPORT.md](https://github.com/itu-bdsa/lecture_notes/blob/main/sessions/session_12/README_REPORT.md)   


[image1]: 
[image2]: 
