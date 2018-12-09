using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isComboFontSizeClosed = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuNew_Click(object sender, RoutedEventArgs e)
        {
            txtBoxDoc.SelectAll();           
            txtBoxDoc.Selection.Text = "";
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open);
                    TextRange range = new TextRange(txtBoxDoc.Document.ContentStart, txtBoxDoc.Document.ContentEnd);
                    range.Load(fileStream, DataFormats.Rtf);
                }
                catch (IOException)
                {
                    MessageBox.Show("Your file is opened in another application.");
                }
            }               
        }

        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Rich Text Format (*.rtf)|*.rtf";
            if (saveFileDialog.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create);
                TextRange range = new TextRange(txtBoxDoc.Document.ContentStart, txtBoxDoc.Document.ContentEnd);
                range.Save(fileStream, DataFormats.Rtf);
            }
        }

        private void MenuFontTimes_Click(object sender, RoutedEventArgs e)
        {
            menuFontCourier.IsChecked = false;
            menuFontArial.IsChecked = false;
            txtBoxDoc.FontFamily = new FontFamily("Times New Roman");
        }

        private void MenuFontCourier_Click(object sender, RoutedEventArgs e)
        { 
            menuFontTimes.IsChecked = false;
            menuFontArial.IsChecked = false;
            txtBoxDoc.FontFamily = new FontFamily("Courier");
        }

        private void MenuFontArial_Click(object sender, RoutedEventArgs e)
        {
            menuFontTimes.IsChecked = false;
            menuFontCourier.IsChecked = false;
            txtBoxDoc.FontFamily = new FontFamily("Arial");
        }

        private void ComboFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(isComboFontSizeClosed)
            {
                ChangeToolbarFontSize();
                isComboFontSizeClosed = true;
            }
        }

        private void ComboFontSize_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox combobox = sender as ComboBox;
            isComboFontSizeClosed = !combobox.IsDropDownOpen;
            ChangeToolbarFontSize();
        }

        private void ChangeToolbarFontSize()
        {
            string fontSize = ComboFontSize.SelectedItem.ToString();
            fontSize = fontSize.Substring(fontSize.Length - 2);
            switch(fontSize)
            {
                case "10":
                    txtBoxDoc.FontSize = 10;
                    break;
                case "12":
                    txtBoxDoc.FontSize = 12;
                    break;
                case "14":
                    txtBoxDoc.FontSize = 14;
                    break;
                case "16":
                    txtBoxDoc.FontSize = 16;
                    break;
            }
        }

    }
}
