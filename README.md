# ScrapPackages
ScrapPackages is a Injector where you can use packages to extend your mod. You can even use them as a mod manager! You can load in dll files so you can implement such as HTTP so you can write such as a Database.

# How to use (User):
Startup the scrapackages executable (Not the injector) and if scrap mechanic is installed thats not on a default path. click select source and set it to where scrap mechanic is installed. Then go to where scrap mechanic is installed (With explorer) and go to release. Create a new file called 'steam_appid.txt', It must be exactly typed like that. In the file, type in `387990`.

Once you done that. you can now add the packages or create your own!

# How to use (Mod):
To create a package, go to the Package branch and download the files and make a new folder. it's folder name can be anything. but make it sure its short to be easy to write it. And paste the files inside the folder and thats your first package!

You can use the 'loadpackage' function to load a package. It requires only 1 argument. The argument is a string and must be the same as the folder name. If it spits out a error of the package not being found. then its not ether installed or injected.
