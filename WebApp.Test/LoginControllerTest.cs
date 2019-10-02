using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace WebApp.Test
{
    [TestFixture]
    public class LoginControllerTest
    {
        private WebAppTestEnvironment Env { get; set; }
        private HttpClient Client { get; set; }

        [OneTimeSetUp]
        public void Init()
        {
            Env = new WebAppTestEnvironment();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Env.Dispose();
            Client.Dispose();
        }

        [SetUp]
        public void Prepare()
        {
            Env.Prepare();
            Client = Env.WebAppHost.GetClient();
        }

        // This is a dummy senseless test. This project designed to simplify the process of solving code problems for you.
        // I recommend to write tests to verify your code. But you can go by your own way and it's not a bad choice.
        // Remember that it's just a recommendation and presence or absence of tests will not have a large affect on
        // evaluation of result. 90% of the assessment will consist of quantity and quality of solved TODOs.
        // Good luck.:)
        [Test]
        public async Task DummyTest()
        {
            var response = await Client.GetAsync("api/sign-in");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}