using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Youtube_Video_Downloader
{
    public partial class Main : Form
    {
        string url;
        public async Task<string> Download(string args)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://youtube-mp3-download1.p.rapidapi.com/dl?id={args}"),
                Headers =
                    {
                      { "x-rapidapi-host", "youtube-mp3-download1.p.rapidapi.com" },
                      { "x-rapidapi-key", "" }, //Your Rapid Api key here
                    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                JObject jobject = (JObject)JsonConvert.DeserializeObject(body);
                JToken jtoken = jobject["link"];
                return string.Format("{0:n0}", jtoken);
            }
        }
        public Main()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.textBox1.Text))
            {
                this.richTextBox1.Clear();
                this.url = await Download(textBox1.Text);
                this.richTextBox1.Text = url;
                return;
            }
            MessageBox.Show("The id space is empty!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(url))
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(url))
                Clipboard.SetText(url);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
            this.textBox1.Clear();
            this.url = "";
        }
    }
}
