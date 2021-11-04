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
            OutputDirectoryTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Trackmania\\Skins\\Models\\CarSport";
        }

        private void ConvertButtonClick(object sender, RoutedEventArgs e)
        {
            if(InputDirectoryTextBox.Text == "")
            {
                return;
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
            Title = "Converting...";
            converter.Convert(InputDirectoryTextBox.Text, OutputDirectoryName, SkinName);
            Title = "Conversion complete!";
            ConvertButton.IsEnabled = true;
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
