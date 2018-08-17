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
using com.gargoylesoftware.htmlunit;
using com.gargoylesoftware.htmlunit.html;
using System.Windows.Threading;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace GetBash
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Setup();

            btnPrev.Visibility = Visibility.Hidden;
            btnShare.Visibility = Visibility.Hidden;

            currentUser = Environment.UserName;

            FilePath = @"C:\Users\" + currentUser + @"\Documents\Bash.txt";
        }

        private WebClient webClient = new WebClient(BrowserVersion.INTERNET_EXPLORER_11);

        private List<Quote> list_quotes = new List<Quote>();

        private HtmlPage pageRandom;

        private int counter = 0;

        private string sendToUser = "";

        private string sendToPC = "";

        private string currentUser;

        private string FilePath;

        public bool Setup()
        {
            try
            {
                btnNext.Content = "LOAD";
                lblCounter.Content = "";
                lblData.Content = "";
                lblRating.Content = "";

                webClient.getOptions().setThrowExceptionOnFailingStatusCode(false);
                webClient.getOptions().setThrowExceptionOnScriptError(false);
                webClient.getOptions().setUseInsecureSSL(true);
                webClient.getOptions().setJavaScriptEnabled(true);
                webClient.getOptions().setRedirectEnabled(true);
                webClient.getOptions().setPrintContentOnFailingStatusCode(false);
                //webClient.setHTMLParserListener(HTMLParserListener.LOG_REPORTER);
                webClient.setRefreshHandler(new ImmediateRefreshHandler());
                webClient.setAjaxController(new NicelyResynchronizingAjaxController());
                webClient.setHTMLParserListener(null);
                webClient.setJavaScriptErrorListener(null);
                webClient.setJavaScriptTimeout(60000);
                webClient.waitForBackgroundJavaScript(60000);
                java.util.logging.Logger.getLogger("com.gargoylesoftware").setLevel(java.util.logging.Level.OFF);
                java.util.logging.Logger.getLogger("com.gargoylesoftware.htmlunit").setLevel(java.util.logging.Level.OFF);
                return true;
            }
            catch
            {
                return false;
            }
            //return true;
        } // setup creditenals and prepare settings

        public void GetPage()
        {
            pageRandom = (HtmlPage)webClient.getPage("http://bash.im/random");
        }

        private void GetQuotes()
        {
            pageRandom = null;

            loading_panel.Visibility = Visibility.Visible;
            progressRing.IsActive = true;
            DoEvents();

            list_quotes.Clear();

            Thread threadGetPage = new Thread(GetPage);
            threadGetPage.Start();
            
            while (pageRandom == null)
            {
                DoEvents();
            }

            loading_panel.Visibility = Visibility.Hidden;
            progressRing.IsActive = false;

            HtmlDivision div_actions;
            HtmlDivision div_text;

            java.util.List divList = new java.util.ArrayList();

            divList = pageRandom.getByXPath("//div[@class='quote']");

            foreach (HtmlDivision div in divList.toArray())
            {
                Quote quote = new Quote();

                div_actions = (HtmlDivision)div.getFirstByXPath("./div[@class='actions']");
                HtmlSpan rating = (HtmlSpan)div_actions.getFirstByXPath("./span[@class='rating-o']");
                HtmlSpan date = (HtmlSpan)div_actions.getFirstByXPath("./span[@class='date']");

                quote.rating = rating.asText();
                quote.date_added = date.asText();

                div_text = (HtmlDivision)div.getFirstByXPath("./div[@class='text']");

                quote.quote_text = div_text.asText();

                list_quotes.Add(quote);
            }


            DoEvents();

            if (list_quotes.Count < 1)
            {
                Log("Not enough mana!");
                lblCounter.Content = "";
            }
            else
            {
                btnPrev.Visibility = Visibility.Visible;
                btnShare.Visibility = Visibility.Visible;

                btnNext.Content = "NEXT";

                counter = 0;

                ShowQuote(counter);
            }
        }

        private static void DoEvents()
        {
            try
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));
            }
            catch
            {
                MessageBox.Show("Not enough mana!");
            }
        } // refresh app 

        private void Log(string text)
        {
            txtQuote.Text = text;

            DoEvents();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (counter > 0)
            {
                counter--;
                ShowQuote(counter);              
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            counter++;
            try
            {
                ShowQuote(counter);
            }
            catch
            {
                Log("");
                counter = 0;                
                GetQuotes();                
            }
        }

        private void ShowQuote(int index)
        {
            Log(list_quotes[index].quote_text);

            lblData.Content = list_quotes[index].date_added;

            int rating = 0;

            try
            {
                rating = Convert.ToInt32(list_quotes[index].rating.Replace("...", "0"));
            }
            catch
            {
                rating = 0;
                txtQuote.Text += "\r\n" + "Can't convert rating!";
            }


            if (rating < 1000)
            {
                lblRating.Foreground = Brushes.Red;
            }
            if (rating >= 1000 && rating < 4000)
            {
                lblRating.Foreground = Brushes.Orange;
            }
            if (rating >= 4000)
            {
                lblRating.Foreground = Brushes.Green;
            }
            lblRating.Content = list_quotes[index].rating;
            

            lblCounter.Content = counter+1 + " / " + list_quotes.Count;
        }

        private void btnShare_Click(object sender, RoutedEventArgs e)
        {            
            if (txtQuote.Text.Length > 5)
            {
                SaveQuote();

                ShareQuote();
            }
            else
            {
                MessageBox.Show("Ti 4o, ahuel?");
            }
        }

        private void cxtCopy_Click(object sender, RoutedEventArgs e)
        {
            txtQuote.Copy();
        }
        
        private void cxtGoogle_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://www.google.ru/search?q=" + txtQuote.SelectedText.Trim().Replace(" ", "%20");
            Process.Start("chrome.exe", url + " --incognito");
        }

        private void SaveQuote()
        {
            try
            {
                File.AppendAllText(FilePath, txtQuote.Text + "\r\n" + "\r\n" + "---------------------------------------------" + "\r\n" + "\r\n");
            }
            catch (Exception ex)
            {
                Log("Not enough mana!" + "\r\n\r\n" + ex.Message);
            }
        }

        private void ShareQuote()
        {
            string strCmdCom = @"/c C:\Windows\Sysnative\msg.exe /server:" + sendToPC + " " + sendToUser + " " + txtQuote.Text.Replace("\r\n", " ").Replace("\"", "").Replace("'", "").Replace("<", "").Replace(">", ":").Replace("|", "");
           
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.WorkingDirectory = @"C:\Windows\system32";
            startInfo.FileName = @"C:\Windows\system32\cmd.exe";
            startInfo.Arguments = strCmdCom;
            process.StartInfo = startInfo;
            process.Start();
        }

        private void cxtSave_Click(object sender, RoutedEventArgs e)
        {
            SaveQuote();
        }

        private void cxtOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(FilePath);
            }
            catch (Exception ex)
            {
                Log("Not enough mana!" + "\r\n\r\n" + ex.Message);
            }
        }
        
    }
}
