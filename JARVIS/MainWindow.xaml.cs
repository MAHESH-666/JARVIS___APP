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
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
using System.Xml;

namespace JARVIS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpeechRecognitionEngine SpeechRecognitionEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer JARVIS = new SpeechSynthesizer();

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                //hook the events
                SpeechRecognitionEngine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(engine_AudioLevelUpdated);
                SpeechRecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognized);

                //load dictionary
                LoadGrammarAndCommands();
                //use the systems default microphone
                SpeechRecognitionEngine.SetInputToDefaultAudioDevice();
                //start listening
                SpeechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
                JARVIS.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(JARVIS_SpeakCompleted);

                if (JARVIS.State == SynthesizerState.Speaking)
                {
                    JARVIS.SpeakAsyncCancelAll();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Voice Recognition Failed");
            }
        }
        private void JARVIS_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            if (JARVIS.State == SynthesizerState.Speaking)
            {
                JARVIS.SpeakAsyncCancelAll();
            }
        }
        private void engine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string Speech = e.Result.Text;
            switch (Speech)
            {
                //some  statements
                case "hai mama":
                    JARVIS.SpeakAsync("Hello Mahesh Gaaaru, Ela Unnnaru, Baggunnarra?  Nenu Mee?, Chitti Robottt Nee, ");
                    break;

                case "tell me about me":
                  
                    JARVIS.SpeakAsync("Hello Mahesh Sir, you are a very good person in the world...! very good profession behavior thank you mahesh sir");
                    break;
                case "can i get a girlfriend":
                    JARVIS.SpeakAsync("No, you cant get a girlfriend, you born single and you will die single");
                    break;
                case "open google":
                    System.Diagnostics.Process.Start("https://www.google.com");
                    break;
                case "open facebook":
                    System.Diagnostics.Process.Start("https://www.facebook.com");
                    break;
                case "open youtube":
                    System.Diagnostics.Process.Start("https://www.youtube.com");
                    break;

                default:
                    break;
            }
        }

        private void engine_AudioLevelUpdated(object sender, AudioLevelUpdatedEventArgs e)
        {
           // progress.Value = e.AudioLevel;
        }


        private void LoadGrammarAndCommands()
        {
      
            try
            {
                Choices Text = new Choices();
                string[] Lines = File.ReadAllLines(Environment.CurrentDirectory +"\\Commands.txt");
                Text.Add(Lines);
                Grammar WordsList = new Grammar(new GrammarBuilder(Text));
                SpeechRecognitionEngine.LoadGrammar(WordsList);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void jBG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}

