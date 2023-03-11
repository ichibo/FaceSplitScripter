using System;
using System.Windows;
using System.Windows.Controls;

namespace FaceSplitScripter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string COPY_TO_CLIPBOARD_TEXT = "Copy to Clipboard";

        public MainWindow()
        {
            InitializeComponent();
            ResetButtonText();
        }

        private void LootsplitText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var senderObject = sender as TextBox;
            string newText = senderObject.Text;

            ResetButtonText();

            if (newText.Trim() != string.Empty)
            {
                try
                {
                    var converter = new LootsplitScriptOrchestrator();
                    var result = converter.ConvertLootsplitTextToRazorMacro(newText);

                    textBox_RazorScript.Text = result.GetScript();
                    textBox_ManualItems.Text = result.GetManualItems();
                }
                catch
                {
                    textBox_RazorScript.Text = "Unable to parse... Try pasting again.";
                }
            }

            else
            {
                textBox_RazorScript.Text = "...";
                textBox_ManualItems.Text = "...";
            }
        }

        private void ResetButtonText()
        {
            button_manualItems.Content = COPY_TO_CLIPBOARD_TEXT;
            button_razorScript.Content = COPY_TO_CLIPBOARD_TEXT;
        }

        private void button_manualItemsClick(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            clickedButton.Content = "Copied!";
            Clipboard.SetText(textBox_ManualItems.Text);
        }

        private void button_razorScriptClick(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            clickedButton.Content = "Copied!";
            Clipboard.SetText(textBox_RazorScript.Text);
        }
    }
}
