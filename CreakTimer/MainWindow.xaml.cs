using CreakTimer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CreakTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SettingsConfig SettingsConfig;
        private string DataFile = string.Empty;
        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        public MainWindow()
        {
            InitializeComponent();
            CreateFile();
            LoadSettings();
        }
        
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            this.dtpTarget.SelectedDate = DateTime.Now;
            this.txtDays.Text = "0";
        }

        private void CreateFile()
        {
            string dataFolder = System.IO.Path.Combine(Environment.CurrentDirectory, "CreakTimerData");
            if(!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }

            DataFile = System.IO.Path.Combine(dataFolder, "Data.json");
            if(!File.Exists(DataFile))
            {
                File.Create(DataFile);
            }            
        }

        private void SaveSettings()
        {
            if(ValidateSettingsOnForm())
            {
                SettingsConfig = new SettingsConfig()
                {
                    TargetDate = this.dtpTarget.SelectedDate.Value,
                    Days = Convert.ToInt32(this.txtDays.Text),
                    EnableDarkMode = this.chkEnableDarkMode.IsChecked.Value
                };

                string jsonData = javaScriptSerializer.Serialize(SettingsConfig);
                File.WriteAllText(DataFile, jsonData);
            }
            else
            {
                MessageBox.Show("Errors on Form");
            }
        }

        private void LoadSettings()
        {
            string jsonData = File.ReadAllText(DataFile);
            if (!string.IsNullOrWhiteSpace(jsonData))
            {
                SettingsConfig = javaScriptSerializer.Deserialize<SettingsConfig>(jsonData);
                this.dtpTarget.SelectedDate = SettingsConfig.TargetDate;
                this.txtDays.Text = SettingsConfig.Days.ToString();
                this.chkEnableDarkMode.IsChecked = SettingsConfig.EnableDarkMode;

            }

        }
        
        private bool ValidateSettingsOnForm()
        {
            if (this.dtpTarget.SelectedDate == null || string.IsNullOrWhiteSpace(this.txtDays.Text) || !ContainsOnlyDigits(this.txtDays.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ContainsOnlyDigits(string numberString)
        {
            if(numberString.Where(x => x < 49 || x > 57).Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
