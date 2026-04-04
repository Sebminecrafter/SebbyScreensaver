using System;
using System.Windows.Forms;
using SebbyScreensaver;

namespace SebbyScreensaver
{
    static class Program
    {
        public static string settingsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "SebbyScreensaver");

        public static String sizeFile = Path.Combine(settingsPath, "size.txt");
        public static String speedFile = Path.Combine(settingsPath, "speed.txt");

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length > 0)
            {
                string arg = args[0].ToLower().Trim();

                if (arg.StartsWith("/c"))
                {
                    Application.Run(new SettingsForm());
                }
                else if (arg.StartsWith("/p"))
                {
                    IntPtr previewHandle = new IntPtr(long.Parse(args[1]));
                    Application.Run(new ScreensaverForm(previewHandle));
                }
                else
                {
                    ShowScreens();
                }
            }
            else
            {
                ShowScreens();
            }
        }

        static void ShowScreens()
        {
            foreach (var screen in Screen.AllScreens)
            {
                ScreensaverForm form = new ScreensaverForm(screen.Bounds);
                form.Show();
            }
            Application.Run();
        }
    }
}