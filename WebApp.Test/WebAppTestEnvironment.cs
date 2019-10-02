using System;
using NUnit.Framework;

namespace WebApp.Test
{
    public class WebAppTestEnvironment : IDisposable
    {
        public WebAppTestHost WebAppHost { get; }

        public WebAppTestEnvironment()
        {
            WebAppHost = new WebAppTestHost();
        }

        public void Prepare()
        {
            WebAppHost.Start();
        }

        public void Dispose()
        {
            WebAppHost?.Dispose();
        }
    }
}