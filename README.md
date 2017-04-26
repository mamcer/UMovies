# UMovies

## Description

UMovies is a collection of applications to remotely play and control media files. The main scenario was to show a list of available media files, play them in a media center and control in realtime basic commands like Play/Pause or Skip next or back.

It's composed by an application that scans media files and index them in a database. An application that handle real time commands and pass them to mphc and a web application as front end.

There are a lot of excellent products out there to do this a much more functionality. This just is a weekend project for fun and to practice SignalR.

## Tools

I'm using mphc as the default media player. View > Options > Player Title Bar section, select Don't prefix anything.
With this option mphc won't prefix the window title with the name of the media file that is playing. It will always show the default text Media Player Classic Home Cinema, making it easier to automate with autohotkey.

Autohotkey to automate mphc functions like play/pause, skip back, skip forward or stop

And Input simulator to programatically emulate a key press.

[https://mpc-hc.org/]()
[https://github.com/michaelnoonan/inputsimulator]()
[https://autohotkey.com/]()

## Technologies 

.Net 4.5.2
SignalR 2.2.1
Entity Framework 6.1.3
SQL Server Express 2014
