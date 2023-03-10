using FaceParser;
using System.Windows;
using System.Windows.Controls;

namespace FaceSplitScripter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LootsplitText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var senderObject = sender as TextBox;
            string newText = senderObject.Text;

            if (newText.Trim() != string.Empty)
            {
                try
                {
                    var converter = new LootsplitScriptOrchestrator();
                    var result = converter.ConvertLootsplitTextToRazorMacro(newText);

                    textBox_RazorScript.Text = result.GetScript();
                    textBox_ManualItems.Text = result.GetErrors();
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
    }
}
