# UMovies

## Description

UMovies is a collection of applications to remotely play and control media files. The main scenario is to show a list of available media files, play them in a media center and control remotely and in realtime. It support commands like Play/Pause, Skip back and forward, play from the beginning and Close player.

It's composed by an application that scans media files and index them in a database. A Windows application that handle real time commands and pass them to mphc and a web application as front end.

There are a lot of excellent products out there to do this and much more functionality. This started as a weekend project for fun and to practice SignalR.

## Tools

I'm using mphc as the default media player. View > Options > Player Title Bar section, select Don't prefix anything.
With this option mphc won't prefix the window title with the name of the media file that is playing. It will always show the default text Media Player Classic Home Cinema, making it easier to automate with autohotkey.

Autohotkey to automate mphc functions like play/pause, skip back, skip forward or stop

And Input simulator to programatically emulate a key press.

[https://mpc-hc.org/]()
[https://github.com/michaelnoonan/inputsimulator]()
[https://autohotkey.com/]()

## Technologies 

- .Net 4.5.2
- SignalR 2.2.1
- Entity Framework 6.1.3
- SQL Server Express 2014

## Configure and Run

The solution is composed of two Web applications: Web and Relay and a Windows desktop application: Player.

### Relay

In the Web.config file update the customsHeaders `Access-Control-Allow-Origin` element with the url of the Web application.

For example

    <httpProtocol>
      <customHeaders>
        <add name="Access-Control-Allow-Origin" value="http://10.0.64.10:1010"/>
      </customHeaders>
    </httpProtocol>

### Web

In the Web.config file update the `appSettings` section SignalRUrl and PicturesUrl with the correspondent urls. And also update the connection string.

For example:

	 <appSettings>
		<add key="SignalrUrl" value="http://10.0.64.10:1011/signalr" />
		<add key="PicturesUrl" value="http://10.0.64.10:1012" />
	 ...

And connection string:

	<connectionStrings>
		<add name="UMovies" connectionString="Data Source=localhost;Initial Catalog=UMovies;Integrated Security=True" providerName="System.Data.SqlClient" />
	</connectionStrings>

### Player

In the Player config file you will have to configure the `VideoPlayerPath` key `VideoRootFolder` and `RelayUrl`:

For example:

    <add key="VideoPlayerPath" value="C:\root\bin\mphc\App\MPC-HC\mpc-hc.exe" />
    <add key="VideoRootFolder" value="Z:\family-videos" />
    <add key="RelayUrl" value="http://10.0.64.10:1011" />
    <add key="Heartbeat" value="8000" />

	<connectionStrings>
		<add name="UMovies" connectionString="Data Source=localhost;Initial Catalog=UMovies;Integrated Security=True" providerName="System.Data.SqlClient" />
	</connectionStrings>