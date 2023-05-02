using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Diagnostics;
using MetroFramework.Forms;

namespace ScrapPackages
{
    public partial class Main : MetroForm
    {
        public Logger MainLogger = Program.MainLogger;
        public Logger FileSystemLogger = Program.FileSystemLogger;
        public string ScrapMechanicPath = @"C:\Program Files (x86)\Steam\steamapps\common\Scrap Mechanic";

        public BindingList<ModClass> Mods = new BindingList<ModClass>();
        public Main()
        {
            InitializeComponent();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            PackagesList.DataSource = Mods;
            PackagesList.Columns[0].Width = 50;
            PackagesList.Columns[1].Width = 200;
            PackagesList.Columns[1].ReadOnly = true;
            PackagesList.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            PackagesList.Columns[2].ReadOnly = true;
            PackagesList.Columns[3].Visible = false;
            if (Directory.Exists(ScrapMechanicPath))
            {
                MainLogger.Info("Loading list.");
                ScrapMechanicPath_Text.Text = ScrapMechanicPath;
                RefreshButton_Click(sender, e);
            }
            else if (File.Exists(Path.Combine(Environment.CurrentDirectory, "smlocation.txt")))
            {
                FileSystemLogger.Info("Loading list by file.");
                ScrapMechanicPath = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "smlocation.txt"));
                ScrapMechanicPath_Text.Text = ScrapMechanicPath;
                RefreshButton_Click(sender, e);
            }
            else
                FileSystemLogger.Error("Couldn't find the scrap mechanic directory.");
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            PackagesList.Rows.Clear();
            MainLogger.Info("Cleared list.");

            string ModsPath = Path.Combine(ScrapMechanicPath, "Release", "packages");
            string ModPath = Path.Combine(Environment.CurrentDirectory, "packages");

            if (!Directory.Exists(ModPath))
            {
                FileSystemLogger.Error($"The mod directory dosen't exist! Please create a mod directory in '{Environment.CurrentDirectory}' called 'packages'.");
                DialogResult result = MessageBox.Show("The packages folder dosen't Exist!", "sModLauncher - Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                if (result == DialogResult.Retry)
                {
                    FileSystemLogger.Info("Retrying...");
                    RefreshButton_Click(sender, e);
                }
                else if (result == DialogResult.Ignore)
                    FileSystemLogger.Warn("Directory check has been ingored!");
                else
                    return;
            }

            string[] modpathdirs = Directory.GetDirectories(ModPath);
            foreach (string path in modpathdirs)
            {
                FileSystemLogger.Info($"Loading '{path}...'");
                ModClassJSON json = JsonConvert.DeserializeObject<ModClassJSON>(File.ReadAllText(Path.Combine(path, "package.json")));
                ModClass ModClass = new ModClass();
                ModClass.Inject = Directory.Exists(Path.Combine(ModsPath, new DirectoryInfo(path).Name));
                ModClass.Name = json.Name;
                ModClass.Description = json.Description;
                ModClass.Path = path;
                Mods.Add(ModClass);
            }
            TotalPackages_Text.Text = $"Total Packages: {modpathdirs.Length}";
        }

        private void LaunchSMButton_Click(object sender, EventArgs e)
        {
            string ModsPath = Path.Combine(ScrapMechanicPath, "Release", "packages");

            Directory.CreateDirectory(ModsPath);
            FileSystemLogger.Info("Creating packages directory on $RELEASE_DATA if it dosen't exist.");

            foreach (string dir in Directory.GetDirectories(ModsPath))
                Directory.Delete(dir, true);
            FileSystemLogger.Info("Removed all mods in the $RELEASE_DATA/packages directory.");

            foreach (DataGridViewRow row in PackagesList.Rows)
            {
                if (bool.Parse(row.Cells[0].Value.ToString()) == true)
                {
                    string path = row.Cells[3].Value.ToString();
                    DirectoryInfo pathInfo = new DirectoryInfo(path);
                    DirectoryInfoExtended.CopyAll(pathInfo, new DirectoryInfo(Path.Combine(ModsPath, pathInfo.Name)));
                }
            }
            FileSystemLogger.Info("Finished process of copying mods. Launching the injector...");

            ProcessStartInfo info = new ProcessStartInfo()
            {
                FileName = Path.Combine(Environment.CurrentDirectory, "SPInjector.exe"),
                Arguments = "",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = false
            };
            Process proc = new Process() { StartInfo = info };
            proc.Start();
        }

        private void ScrapMechanicPath_Btn_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.Title = "ScrapPackages - Select Scrap Mechanic Source Path";
            dialog.EnsurePathExists = true;
            dialog.IsFolderPicker = true;
            dialog.Multiselect = false;

            CommonFileDialogResult result = dialog.ShowDialog();

            if (result == CommonFileDialogResult.Ok)
            {
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "smlocation.txt"), dialog.FileName);
                ScrapMechanicPath_Text.Text = dialog.FileName;
                RefreshButton_Click(sender, e);
            }
        }
        public class ModClassJSON
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }
}