using Microsoft.AspNet.SignalR.Client;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using UMovies.Data;

namespace UMovies.Player
{
    public partial class Main : Form
    {
        private readonly string _videoPlayerPath;
        private readonly string _videoRootFolder;
        private readonly string _relayUrl;
        private IHubProxy _soulstoneHub;
        private HubConnection _hubConnection;
        private Process _process;
        public delegate void UpdateControlsDelegate(string message);
        private readonly Timer _timer;

        public Main()
        {
            InitializeComponent();

            _videoPlayerPath = ConfigurationManager.AppSettings["VideoPlayerPath"];
            _videoRootFolder = ConfigurationManager.AppSettings["VideoRootFolder"];
            _relayUrl = ConfigurationManager.AppSettings["RelayUrl"];
            var sleepInterval = Convert.ToInt32(ConfigurationManager.AppSettings["Heartbeat"]);
            _timer = new Timer { Interval = sleepInterval };
            _timer.Tick += Timer_Tick;

            InitializeSignalR(_relayUrl);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TestReconnection();
        }

        private void TestReconnection()
        {
            if (_hubConnection.State == ConnectionState.Disconnected)
            {
                _timer.Stop();
                InvokeConsoleLog("SignalR connection lost. Reconnecting...");
                InitializeSignalR(_relayUrl);
            }
        }

        private async void InitializeSignalR(string url)
        {
            _hubConnection?.Stop();

            _hubConnection = new HubConnection(url);
            _soulstoneHub = _hubConnection.CreateHubProxy("umoviesHub");

            _soulstoneHub.On<string>("Welcome", (message) =>
            {
                try
                {
                    ConsoleLog(message);
                }
                catch (Exception ex)
                {
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            _soulstoneHub.On<string, string>("PlayMovie", (folderName, fileName) =>
            {
                try
                {
                    KillProcess();
                    InvokeConsoleLog(fileName);
                    var movieFilePath = Path.Combine(_videoRootFolder, Path.Combine(folderName, fileName));
                    var arguments = $@"""{movieFilePath}"" /fullscreen";
                    InvokeConsoleLog(arguments);
                    var processStart = new ProcessStartInfo(_videoPlayerPath, arguments);
                    _process = Process.Start(processStart);
                }
                catch (Exception ex)
                {
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            _soulstoneHub.On<int>("PlayAllMovieFiles", (movieId) =>
            {
                try
                {
                    KillProcess();
                    var entities = new UMoviesEntities();
                    var movie = entities.Movies.FirstOrDefault(m => m.Id == movieId);
                    if (movie != null)
                    {
                        var movieFilePath = string.Empty;
                        foreach (var movieFile in movie.MovieFiles)
                        {
                            movieFilePath +=$@"""{Path.Combine(_videoRootFolder, Path.Combine(movie.MovieFolder, movieFile.FileName))}"" ";
                        }

                        var arguments = $@"/add /play {movieFilePath} /fullscreen";
                        InvokeConsoleLog(arguments);
                        var processStart = new ProcessStartInfo(_videoPlayerPath, arguments);
                        _process = Process.Start(processStart);
                    }
                    else
                    {
                        ConsoleLog($"Movie with id:{movieId} not found");
                    }
                }
                catch (Exception ex)
                {
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });
            
            _soulstoneHub.On("PlayPause", () =>
            {
                try
                {
                    ConsoleLog("Play/Pause");
                    var input = new InputSimulator();
                    input.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                    input.Keyboard.KeyPress(VirtualKeyCode.VK_P);
                    input.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                }
                catch (Exception ex)
                {
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            _soulstoneHub.On("SkipBack", () =>
            {
                try
                {
                    ConsoleLog("Skip Back");
                    var input = new InputSimulator();
                    input.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                    input.Keyboard.KeyPress(VirtualKeyCode.VK_L);
                    input.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                }
                catch (Exception ex)
                {
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            _soulstoneHub.On("SkipForward", () =>
            {
                try
                {
                    ConsoleLog("Skip Forward");
                    var input = new InputSimulator();
                    input.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                    input.Keyboard.KeyPress(VirtualKeyCode.VK_R);
                    input.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                }
                catch (Exception ex)
                {
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            _soulstoneHub.On("PlayFromBeginning", () =>
            {
                try
                {
                    ConsoleLog("Play from the beginning");
                    var input = new InputSimulator();
                    input.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                    input.Keyboard.KeyPress(VirtualKeyCode.VK_B);
                    input.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                }
                catch (Exception ex)
                {
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            _soulstoneHub.On("ClosePlayer", () =>
            {
                try
                {
                    ConsoleLog("Closing Player");
                    KillProcess();
                }
                catch (Exception ex)
                {
                    ConsoleLog("An error has occurred: " + ex.Message);
                }
            });

            try
            {
                await _hubConnection.Start();
            }
            catch
            {
                InvokeConsoleLog("SignalR connection could not be established");
            }

            if (_hubConnection.State == ConnectionState.Connected)
            {
                InvokeConsoleLog("SignalR connection established");
            }

            _timer.Start();
        }

        private void KillProcess()
        {
            if (_process != null && !_process.HasExited)
            {
                var proc = Process.GetProcessById(_process.Id);
                proc.Kill();
            }
        }

        private void InvokeConsoleLog(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateControlsDelegate(ConsoleLog), message);
            }
            else
            {
                ConsoleLog(message);
            }
        }

        private void ConsoleLog(string message)
        {
            txtConsole.Text += DateTime.Now.ToString("yyyy.dd.MM-hh:mm:ss") + @" - " + message + Environment.NewLine;
            txtConsole.SelectionStart = txtConsole.Text.Length;
            txtConsole.ScrollToCaret();
        }

        private void reconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestReconnection();
        }

        private void stayOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
            stayOnTopToolStripMenuItem.Checked = TopMost;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}