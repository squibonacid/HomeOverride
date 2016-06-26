using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace HomeOverride {
    public partial class HomeOverrideForm : Form {
        HomeOverrideProgram homeOverrideProgram;
        ContextMenu contextMenu;

        public HomeOverrideForm() {
            string[] args = Environment.GetCommandLineArgs();
            InitializeComponent();
            homeOverrideProgram = new HomeOverrideProgram(this);
            contextMenu = new ContextMenu();
            MenuItem m1 = contextMenu.MenuItems.Add("Open");
            MenuItem m2 = contextMenu.MenuItems.Add("Close");
            m1.Click += new EventHandler(notifyIcon_DoubleClick);
            m2.Click += new EventHandler(Close);
            notifyIcon.ContextMenu = contextMenu;

            tooltip_application.SetToolTip(label_AppName, "Select application that you want to launch when Oculus Home is starting or the hotkey is pressed");
            tooltip_application.SetToolTip(button_selectYourApplication, "Select the application that launches when Oculus Home is starting or the hotkey is pressed");
            tooltip_application.SetToolTip(button_about, "Info");
            tooltip_application.SetToolTip(checkbox_enabled, "enable/disable this tool");
            tooltip_hotkey.SetToolTip(comboBox_KeyMod, "What hotkey do you want to use to launch your application?");
            tooltip_hotkey.SetToolTip(comboBox_Key, "What hotkey do you want to use to launch your application?");
            tooltip_startminimized.SetToolTip(checkBox_StartMinimized, "Should this tool start minimized (to tray)?");
            tooltip_startwithwindows.SetToolTip(checkBox_StartWithWindows, "Should this tool start up with windows?");
        }

        public class GlobalHotKey {
            public const int WM_HOTKEY_MSG_ID = 0x0312;
            public enum Modifier {
                None = 0x0000,
                ALT = 0x0001,
                CTRL = 0x0002,
                SHIFT = 0x0004,
                WIN = 0x0008
            }

            [DllImport("user32.dll")]
            private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

            [DllImport("user32.dll")]
            private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

            private int modifier;
            private int key;
            private IntPtr hWnd;
            private int id;

            public GlobalHotKey(int modifier, Keys key, Form form) {
                this.modifier = modifier;
                this.key = (int)key;
                this.hWnd = form.Handle;
                id = this.GetHashCode();
            }

            public override int GetHashCode() {
                return modifier ^ key ^ hWnd.ToInt32();
            }

            public bool Register() {
                return RegisterHotKey(hWnd, id, modifier, key);
            }

            public bool Unregister() {
                return UnregisterHotKey(hWnd, id);
            }
        }

        public class HomeOverrideProgram {

            static GlobalHotKey hotkey;
            static HomeOverrideForm form;
            static System.Threading.Timer timer;

            bool wasOculusHomeRunningLastUpdate = false;
            int myProcessID = -1;
            Process myProcess = null;
            
            public HomeOverrideProgram(HomeOverrideForm _form) {
                form = _form;
                Configuration.Load();
                
                InitializeForm();
                InitializeTimer();
                InitializeHotkey();
                RegisterHotkey();
                RegisterInStartup(form.checkBox_StartWithWindows.Checked);
            }

            public void RegisterHotkey() {
                if (hotkey != null) {
                    hotkey.Unregister();
                }

                Configuration.selectedHotKeyKey = form.comboBox_Key.SelectedIndex;
                Configuration.selectedHotKeyMod = form.comboBox_KeyMod.SelectedIndex;
                Configuration.Save();

                GlobalHotKey.Modifier mod = (GlobalHotKey.Modifier)form.comboBox_KeyMod.SelectedItem;
                Keys key = (Keys)form.comboBox_Key.SelectedItem;
                hotkey = new GlobalHotKey((int)mod, key, form);
                bool success = hotkey.Register();

                form.label_HotKeyInfo.Text = success ? "" : "Could not register Hotkey " + mod + "+" + key;
                form.label_HotKeyInfo.ForeColor = success ? System.Drawing.Color.Black : System.Drawing.Color.DarkRed;
            }
            
            public void RegisterInStartup(bool register) {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                        ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (register) {
                    registryKey.SetValue("Oculus Home Override", Application.ExecutablePath);
                } else {
                    if (registryKey.GetValue("Oculus Home Override") != null) {
                        registryKey.DeleteValue("Oculus Home Override");
                    }
                }
            }

            public void SelectApplication() {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                openFileDialog.Filter = ".exe files (*.exe)|*.exe";

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    string result = openFileDialog.FileName;
                    Configuration.customApplicationPath = result;
                    Configuration.Save();
                    form.label_AppName.Text = Configuration.customApplicationPath;
                }

                if (File.Exists(Configuration.customApplicationPath)) {
                    form.pictureBox_applicationIcon.Image = System.Drawing.Icon.ExtractAssociatedIcon(Configuration.customApplicationPath).ToBitmap();
                }
            }
            
            public void HotKeyPressed() {
                StartOrRestartCustomApplication();
            }
            
            public bool HasAutostartEntry() {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                        ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                return registryKey.GetValue("Oculus Home Override") != null;
            }

            void OnTimerUpdate() {
                bool isOculusHomeRunning = ProcessExists("OculusVR");

                if (isOculusHomeRunning) {
                    if (myProcess == null || myProcessID == -1 || !wasOculusHomeRunningLastUpdate) {
                        StartOrRestartCustomApplication();
                    }
                }

                wasOculusHomeRunningLastUpdate = isOculusHomeRunning;
            }

            void InitializeForm() {
                form.checkBox_StartMinimized.Checked = Configuration.startMinimized;
                form.checkbox_enabled.Checked = Configuration.enabled;
                form.checkBox_StartWithWindows.Checked = HasAutostartEntry();
                form.label_AppName.Text = Path.GetFileName(Configuration.customApplicationPath);
                if (File.Exists(Configuration.customApplicationPath)) {
                    form.pictureBox_applicationIcon.Image = System.Drawing.Icon.ExtractAssociatedIcon(Configuration.customApplicationPath).ToBitmap();
                }
                if (Configuration.startMinimized) {
                    form.MinimizeToTray();
                }
            }

            void InitializeTimer() {
                timer = new System.Threading.Timer(
                    e => OnTimerUpdate(),
                    null,
                    TimeSpan.Zero,
                    TimeSpan.FromSeconds(1f));
            }

            void InitializeHotkey() {
                form.comboBox_KeyMod.Items.Clear();
                form.comboBox_KeyMod.Items.AddRange(Enum.GetValues(typeof(GlobalHotKey.Modifier)).Cast<object>().ToArray());
                form.comboBox_KeyMod.SelectedIndex = Configuration.selectedHotKeyMod;
                
                form.comboBox_Key.Items.Clear();
                form.comboBox_Key.Items.AddRange(Enum.GetValues(typeof(Keys)).Cast<object>().ToArray());
                form.comboBox_Key.SelectedIndex = Configuration.selectedHotKeyKey;
            }

            void StartOrRestartCustomApplication() {
                if (!Configuration.enabled || !File.Exists(Configuration.customApplicationPath)) {
                    return;
                }

                if (myProcess != null) {
                    while (!myProcess.HasExited) {
                        try {
                            myProcess.Kill();
                        } catch { }
                    }
                } while (ProcessExists(myProcessID)) {
                    try {
                        Process process = Process.GetProcessById(myProcessID);
                        process.Kill();
                    } catch { }
                }
                
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = Configuration.customApplicationPath;
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                Process newProcess = Process.Start(startInfo);
                myProcessID = newProcess.Id;
                myProcess = newProcess;
            }

            bool ProcessExists(int id) {
                return Process.GetProcesses().Any(x => x.Id == id);
            }

            bool ProcessExists(string name) {
                return Process.GetProcesses().Any(x => x.ProcessName == name);
            }

        }

        public static class Configuration {

            public static string customApplicationPath = "";
            public static bool enabled = true;
            public static bool startMinimized = false;
            public static int selectedHotKeyKey = 0;
            public static int selectedHotKeyMod = 0;

            public enum ConfigParam { CustomApplicationPath = 0, Enabled = 1, StartMinimized = 2, HotKeyKey = 3, HotKeyMod = 4 }

            public static void Load() {
                string configPath = (Path.Combine(Application.UserAppDataPath, "config.txt"));
                if (!File.Exists(configPath)) {
                    File.Create(configPath);
                }
                string[] lines = File.ReadAllLines(configPath);
                while (lines.Length < 5) {
                    List<string> temp = lines.ToList();
                    temp.Add("");
                    lines = temp.ToArray();
                }
                customApplicationPath = lines[(int)ConfigParam.CustomApplicationPath];
                enabled = lines[(int)ConfigParam.Enabled] == true.ToString();
                startMinimized = lines[(int)ConfigParam.StartMinimized] == true.ToString();

                selectedHotKeyKey = 0;
                int.TryParse(lines[(int)ConfigParam.HotKeyKey], out selectedHotKeyKey);

                selectedHotKeyMod = 0;
                int.TryParse(lines[(int)ConfigParam.HotKeyMod], out selectedHotKeyMod);
            }

            public static void Save() {
                string configPath = (Path.Combine(Application.UserAppDataPath, "config.txt"));
                if (!File.Exists(configPath)) {
                    File.Create(configPath);
                }

                File.WriteAllLines(configPath, new string[] {
                    customApplicationPath,
                    enabled.ToString(),
                    startMinimized.ToString(),
                    selectedHotKeyKey.ToString(),
                    selectedHotKeyMod.ToString()
                });
            }

        }

        public void MinimizeToTray() {
            WindowState = FormWindowState.Minimized;
        }

        #region events
        protected override void WndProc(ref Message m) {
            if (m.Msg == GlobalHotKey.WM_HOTKEY_MSG_ID) {
                homeOverrideProgram.HotKeyPressed();
            }
            base.WndProc(ref m);
        }
        
        private void StartupSeviceForm_Resize(object sender, EventArgs e) {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }

        private void Close(object sender, EventArgs e) {
            this.Close();
        }

        private void notifyIcon_DoubleClick(object sender, MouseEventArgs e) {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e) {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void button_SelectApplication(object sender, EventArgs e) {
            homeOverrideProgram.SelectApplication();
        }

        private void checkBox_StartWithWindows_CheckedChanged(object sender, EventArgs e) {
            if (homeOverrideProgram == null) { return; }
            homeOverrideProgram.RegisterInStartup(checkBox_StartWithWindows.Checked);
            checkBox_StartWithWindows.Checked = homeOverrideProgram.HasAutostartEntry();
        }

        private void checkBox_StartMinimized_CheckedChanged(object sender, EventArgs e) {
            if (homeOverrideProgram == null) { return; }
            Configuration.startMinimized = checkBox_StartMinimized.Checked;
            Configuration.Save();
        }

        private void checkbox_enabled_CheckedChanged(object sender, EventArgs e) {
            if (homeOverrideProgram == null) { return; }
            Configuration.enabled = checkbox_enabled.Checked;
            Configuration.Save();
        }
        
        private void button_about_Click(object sender, EventArgs e) {
            MessageBox.Show(
                "Oculus Home Override 1.0\nu/squibonacid\n\n" +

                "Functionality:\n" +
                "- Switch to your favourite VR application via hotkey\n" +
                "- Optional: Switch to your favourite VR application\nwhen Oculus Home starts up first time",
                "Info", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void comboBox_KeyMod_SelectedIndexChanged(object sender, EventArgs e) {
            if (homeOverrideProgram != null) {
                homeOverrideProgram.RegisterHotkey();
            }
        }

        private void comboBox_Key_SelectedIndexChanged(object sender, EventArgs e) {
            if (homeOverrideProgram != null) {
                homeOverrideProgram.RegisterHotkey();
            }
        }
        #endregion events
    }
}