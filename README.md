# SecretsShare

The repository contains a project for a test task on creating an open API.
Test task:
#Secrets sharing
  The main goal of the project is to allow people to safely share files and/or text.
  You will need to implement the API for it.
  Main workflow / functionality:
    1. User can register (using email + password) and then log in using those email and password
    2. Authorized users have ability to:
      a. Upload a file. Users should be able to specify if they want the file to be automatically deleted
        once it’s downloaded. After uploading, the user will receive the file URL (file can only be
        downloaded from this URL). It is important to have some sort of file urls protection (at the very
        least, the urls should not have a simple numeric identifier like /api/files/1; a better level of
        protection is welcomed)
      b. Upload a text (string). Users should be able to specify if they want the information to be
        automatically deleted once it’s accessed. After sending the text, the user should receive the
        text URL (the text can only be accessed from this URL). It is important to have some sort of urls
        protection (at the very least, the urls should not have a simple numeric identifier like /api/files/1;
        a better level of protection is welcomed)
      c. Get the list of files/texts they have uploaded previously. User cannot see the deleted files/texts
      d. Delete a file/text they have uploaded earlier
    3. Anonymous users can:
      a. Access files/texts using the URLs generated from steps 2.a, 2.b
  Technical requirements:
    1. .NET 5, asp.net core
    2. Code comments should be in English
  Optional:
    1. Tests
    2. Docker-file for the application
    3. Git for revision history. You can use github/gitlab/bitbucket for your convenience and give us the link to
      it. OR you can just send us the archive with the git folder. Commit messages should be in English
    4. Storing files on S3 instead of file system
