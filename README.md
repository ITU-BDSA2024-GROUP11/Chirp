# Chirp!
![Alt text](src/Chirp.Web/wwwroot/images/icon1.png)

**Chirp!** is a social media application developed as an assignment for students in the 'Softwareudvikling' course at the IT University of Copenhagen, 3rd semester, 2024.
This implementation of the Chirp! project was created by Group 11.

## Features
* Post and interact with "cheeps" (short text updates).
* Edit or delete your own cheeps.
* Post images by "cheeping" their URL
* Follow other users
* Admin privileges to manage other users' cheeps.

## Running the program locally
To run the program locally you need to have .net8 downloaded
Download [.net8 here](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

Open a terminal and navigate to the project root <br />
Then naviagte to src/Chirp.Web/ by running `cd src/Chirp.Web`<br />
Run the following commands:

`dotnet user-secrets init`<br />
`dotnet user-secrets set  "authentication_github_clientId" "Ov23liN6Yjxe3rEIVpMB"`<br />
`dotnet user-secrets set  "authentication_github_clientSecret" "983419733e343552b15de88bbf4b5d170fa30420"`<br />
(These secrest will only work for running it locally)

When this is done you should be able to run the program from the same location using `dotnet run`

## Admin credentials

If you want to log in to the "Admin" account which has permission to delete and edits other's cheep you can log in with the credentials:

Username: ADMIN <br />
Password: Admin123!
