using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace NotepadTestUI
{
    internal class AppDriver
    {
        protected const string WinAppDriverURL = "http://127.0.0.1:4723";
        protected static WindowsDriver<WindowsElement>? session;
        public static WindowsDriver<WindowsElement> Session
        {
            get => session;
            set => session = value;
        }

        public static void InitApplication(TestContext context, string AppId)
        {
            if (session == null)
            {
                // Create a new session to launch Notepad application
                var appCapabilities = new AppiumOptions();
                appCapabilities.AddAdditionalCapability("app", AppId);
                session = new WindowsDriver<WindowsElement>(new Uri(WinAppDriverURL), appCapabilities);

                Assert.IsNotNull(session);
                Assert.IsNotNull(session.SessionId);

                // Set implicit timeout to 1.5 seconds to make element search to retry every 500ms for at most three times
                session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);
            }
        }

        public static void QuitApplication(string btnSave)
        {
            if (Session != null)
            {
                Session.Close();

                try
                {
                    // Dismiss save dialog if it is blocking the exit
                    Session.FindElementByName(btnSave).Click();
                }
                catch { }

                Session.Quit();
                Session = null;
            }
        }
    }
}
