using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace UMovies.Relay
{
    [HubName("umoviesHub")]
    public class UMovies : Hub
    {
        public void Welcome()
        {
            Clients.Others.Welcome("Hello!");
        }

        public void PlayMovie(string folderName, string fileName)
        {
            Clients.Others.PlayMovie(folderName, fileName);
        }

        public void PlayPause()
        {
            Clients.Others.PlayPause();
        }

        public void PlayAllMovieFiles(int movieId)
        {
            Clients.Others.PlayAllMovieFiles(movieId);
        }        

        public void SkipBack()
        {
            Clients.Others.SkipBack();
        }

        public void SkipForward()
        {
            Clients.Others.SkipForward();
        }

        public void PlayFromBeginning()
        {
            Clients.Others.PlayFromBeginning();
        }

        public void ClosePlayer()
        {
            Clients.Others.ClosePlayer();
        }
    }
}