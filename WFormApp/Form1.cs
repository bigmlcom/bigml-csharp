using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BigML;

namespace WFormApp
{
    public partial class Form1 : Form
    {
        string userName = System.Environment.GetEnvironmentVariable("BIGML_USERNAME");
        string apiKey = System.Environment.GetEnvironmentVariable("BIGML_API_KEY");

        public Form1()
        {
            InitializeComponent();
        }

        // add async attribute in the event handler
        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            label1.Text = "Loading...";

            Client c = new Client(userName, apiKey);
            Ordered<Source.Filterable, Source.Orderable, Source> result
                = (from s in c.ListSources()
                   orderby s.Created descending
                   select s);

            Listing<Source> sources = await result; //use await keyword
            int countSources = 0;
            foreach (Source src in sources)
            {
                countSources++;
            }

            label1.Text = countSources + " sources found";
            button1.Enabled = true;
        }


        private Task<Listing<Source>> SourcesList()
        {

            Client c = new Client(userName, apiKey);
            Ordered<Source.Filterable, Source.Orderable, Source> result
                = (from s in c.ListSources()
                   orderby s.Created descending
                   select s);

            return result.InternalTask;
        }

        private Task<Source> DoGetAsync()
        {
            Client c = new Client(userName, apiKey);
            return c.Get<Source>("source/57e4206ba2e47604db001b20");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            label2.Text = "Loading...";

            Task<Source> task1 = Task.Run(() => DoGetAsync());
            Task<Listing<Source>> task2 = Task.Run(() => SourcesList());

            Source s = task1.Result;
            Listing<Source> s2 = task2.Result;

            label2.Text = s.Name;
            label2.Text = s2.Object["meta"]["limit"] + " sources found";

            button2.Enabled = true;
        }

    }
}
