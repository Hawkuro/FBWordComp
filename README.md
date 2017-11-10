# FBWordComp
Compares word usage in facebook messages based on zipped FB data as received from FB settings page

# Instructions for use

1. Compile the project using Visual Studio 2015+
2. In Facebook, go to Settings and at the bottom of the first settings page, click "Download a copy of your Facebook data"
3. Wait for the data to be processed, you'll get an email when done
4. When you've received the zipped data, unzip it into some directory
5. In App.config, change the app setting "FBDataFolder" to the aforementioned directory where you extracted the data
6. Run the .exe in a command line with the full name of the user you want to analyze followed by a list of words you want to compare, e.g. `.\FBWordComp.exe "Haukur Óskar Þorgeirsson" "D&D" Spunaspil Döndjönns` will analyze my usage of the words "D&D", "Döndjönns" and "Spunaspil"
