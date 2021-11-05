using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using TrackmaniaSkinImageConverterWPF;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TrackmaniaSkinImageConverterWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadingFrame.Visibility  = Visibility.Collapsed;
            OutputDirectoryTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Trackmania\\Skins\\Models\\CarSport";
        }

        private async void ConvertButtonClick(object sender, RoutedEventArgs e)
        {
            LoadingFrame.Visibility = Visibility.Visible;
            if(InputDirectoryTextBox.Text == "")
            {
                return;
            }
            if(!Directory.Exists(InputDirectoryTextBox.Text))
            {
                System.Windows.MessageBox.Show("Please enter a valid image directory.");
            }
            if(!Directory.Exists(OutputDirectoryTextBox.Text))
            {
                System.Windows.MessageBox.Show("Please enter a valid output directory.");
            }

            ConvertButton.IsEnabled = false;
            TrackmaniaSkinImageConverter.Converter converter = new TrackmaniaSkinImageConverter.Converter();

            var OutputDirectoryName = OutputDirectoryTextBox.Text;
            if(OutputDirectoryName == "")
            {
                OutputDirectoryName = InputDirectoryTextBox.Text;
            }

            var SkinName = SkinNameTextBox.Text;
            if(SkinName == "")
            {
                SkinName = "MySkin";
            } 
            //await Dispatcher.InvokeAsync<bool>(async () => await converter.Convert(InputDirectoryTextBox.Text, OutputDirectoryName, SkinName));
            var result = await converter.Convert(InputDirectoryTextBox.Text, OutputDirectoryName, SkinName);

            if(!result)
            {
                System.Windows.MessageBox.Show("A Zipfile with this name already exists!");
            }
            
            ConvertButton.IsEnabled = true;
            LoadingFrame.Visibility = Visibility.Collapsed;
            System.Windows.MessageBox.Show("Conversion complete.");
        }

        private void InputDirectoryButtonClick(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if(result == System.Windows.Forms.DialogResult.OK)
                {
                    InputDirectoryTextBox.Text = dialog.SelectedPath;
                }
            }
        }

        private void OutputDirectoryButtonClick(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if(result == System.Windows.Forms.DialogResult.OK)
                {
                    OutputDirectoryTextBox.Text = dialog.SelectedPath;
                }
            }
        }
    }
}
