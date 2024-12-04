To run the program locally:<br />
In your termial naviagte to src/Chirp.Web/<br />
Run the following commands:

`dotnet user-secrets init`<br />
`dotnet user-secrets set  "authentication_github_clientId" "Ov23liN6Yjxe3rEIVpMB"`<br />
`dotnet user-secrets set  "authentication_github_clientSecret" "983419733e343552b15de88bbf4b5d170fa30420"`<br />
(These secrest will only work for running it locally)

When this is done you should be able to run the program from the same location using `dotnet run`

If you want to log in to the "Admin" account which is has permission to delete and edits other's cheep you can log in with the credentials:

Username: ADMIN <br />
Password: Admin123!
