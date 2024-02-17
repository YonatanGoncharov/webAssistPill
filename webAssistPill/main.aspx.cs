using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Web.UI;

namespace webAssistPill
{
    public partial class main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(LoadQuotesAsync));
        }

        private async Task LoadQuotesAsync()
        {
            string url = "https://type.fit/api/quotes";

            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(url);

                IList<Quote> quoteList = JsonSerializer.Deserialize<IList<Quote>>(response);

                Random random = new Random();
                int index = random.Next(quoteList.Count);
                Quote randomQuote = quoteList[index];

                // Remove ", type.fit" from the author field
                int commaIndex = randomQuote.author.IndexOf(",");
                if (commaIndex != -1)
                {
                    randomQuote.author = randomQuote.author.Substring(0, commaIndex);
                }

                quoteTextLiteral.Text = randomQuote.text;
                quoteAuthorLiteral.Text = randomQuote.author;
            }
        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            Server.Transfer("login.aspx", true);
        }

        protected void registerButton_Click(object sender, EventArgs e)
        {
            Server.Transfer("register.aspx", true);
        }
    }
}