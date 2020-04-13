using FishySkypeParser.Converters;
using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace FishySkypeParser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BusinessLogic bl;

        public MainWindow()
        {
            InitializeComponent();

            bl = new BusinessLogic();
            this.Closing += MainWindow_Closing;

            BuildMessageConverter.dataM = bl.MainDataModel;
            MessageFromToGridColumnConverter.dataM = bl.MainDataModel;
            ColorChoosingConverter.dataM = bl.MainDataModel;
            this.DataContext = bl.MainDataModel;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (bl != null)
                    bl.SaveSettings();
            }
            catch { }
        }

        private void FileOpenClick(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Json files (*.json) | *.json";
                ofd.InitialDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                ofd.CheckFileExists = true;
                if (ofd.ShowDialog() == true)
                {
                    bl.MainDataModel.JsonFilePath = ofd.FileName;
                    bl.LoadJson();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
