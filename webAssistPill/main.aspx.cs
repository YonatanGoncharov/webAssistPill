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
    // The main class for the ASP.NET Web Forms page named "main.aspx"
    public partial class main : System.Web.UI.Page
    {
        // This method is called when the page is loaded
        protected void Page_Load(object sender, EventArgs e)
        {
            // Register an asynchronous task to load quotes when the page loads
            RegisterAsyncTask(new PageAsyncTask(LoadQuotesAsync));
        }

        // Asynchronous method to load quotes from a remote API
        private async Task LoadQuotesAsync()
        {
            // URL of the remote API endpoint that provides quotes
            string url = "https://type.fit/api/quotes";

            // Create an HttpClient instance to send HTTP requests
            using (HttpClient client = new HttpClient())
            {
                // Send an asynchronous GET request to the API endpoint and await the response
                string response = await client.GetStringAsync(url);

                // Deserialize the JSON response into a list of Quote objects
                IList<Quote> quoteList = JsonSerializer.Deserialize<IList<Quote>>(response);

                // Generate a random index to select a random quote from the list
                Random random = new Random();
                int index = random.Next(quoteList.Count);
                Quote randomQuote = quoteList[index];

                // Remove ", type.fit" from the author field, if present
                int commaIndex = randomQuote.author.IndexOf(",");
                if (commaIndex != -1)
                {
                    randomQuote.author = randomQuote.author.Substring(0, commaIndex);
                }

                // Display the random quote and its author on the web page
                quoteTextLiteral.Text = randomQuote.text;
                quoteAuthorLiteral.Text = randomQuote.author;
            }
        }

        // Event handler for the login button click event
        protected void loginButton_Click(object sender, EventArgs e)
        {
            // Redirect the user to the login page
            Server.Transfer("login.aspx", true);
        }

        // Event handler for the register button click event
        protected void registerButton_Click(object sender, EventArgs e)
        {
            // Redirect the user to the register page
            Server.Transfer("register.aspx", true);
        }
    }
}
