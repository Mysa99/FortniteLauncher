
using Moon_Launcher.InjectorShit;
using Moon_Launcher.Models;
using Moon_Launcher.Properties;
using Moon_Launcher.UserOptionsAndEpicStuff;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moon_Launcher
{
    public partial class Launcher : Form
    {



        public Launcher()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = false;
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();
            bool flag = dialogResult == DialogResult.OK;
            if (flag)
            {
                string selectedPath = folderBrowserDialog.SelectedPath;
                this.FN_Path.Text = selectedPath;
            }
            Settings.Default.Path = this.FN_Path.Text;
            Settings.Default.Save();
        }

        private void Launcher_Load(object sender, EventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserOptions userOptions = this.GetUserOptions();
            if (userOptions.CurrentInstall == null)
                return;
            this.UpdateSettings(userOptions);
            Process process1 = ProcessHelper.StartProcess(userOptions.CurrentInstall + "\\FortniteGame\\Binaries\\Win64\\FortniteLauncher.exe", true);
            Process process2 = ProcessHelper.StartProcess(userOptions.CurrentInstall + "\\FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping_BE.exe", true);
            Process process3 = ProcessHelper.StartProcess(userOptions.CurrentInstall + "\\FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe", false, "-AUTH_TYPE=exchangecode -AUTH_LOGIN=unused -AUTH_PASSWORD=" + this.SecretBox.Text);
            this.Hide();
            process3.WaitForInputIdle();
            ProcessHelper.InjectDll(process3.Id, Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Platanium.dll"));
            Thread.Sleep(1000);
            process3.WaitForExit();
            this.Show();
            process1.Kill();
            process2.Kill();
        }

        private EpicInstallList GetInstallList()
        {
            string path = "C:\\ProgramData\\Epic\\UnrealEngineLauncher\\LauncherInstalled.dat";
            return !File.Exists(path) ? (EpicInstallList)null : JsonConvert.DeserializeObject<EpicInstallList>(File.ReadAllText(path));
        }

        private UserOptions GetUserOptions()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Rift", "settings.json");
            if (!File.Exists(path))
            {
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
               UserOptions userOptions = new UserOptions()
                {
                    CurrentInstall = (string)null,
                    Secret = ""
                };
                File.WriteAllText(path, JsonConvert.SerializeObject((object)userOptions));
            }
            return JsonConvert.DeserializeObject<UserOptions>(File.ReadAllText(path));
        }

        private void UpdateSettings(UserOptions newSettings)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Rift", "settings.json");
            if (!File.Exists(path))
                return;
            File.WriteAllText(path, JsonConvert.SerializeObject((object)newSettings));
        }
    }
}