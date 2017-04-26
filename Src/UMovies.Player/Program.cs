using System;
using System.Windows.Forms;

namespace UMovies.Player
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;
            Application.Run(new Main());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            var error = new Error(e.Exception);
            error.ShowDialog();
            Application.Exit();
        }
    }
}