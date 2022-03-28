using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;

namespace NotepadTestUI
{
    public class NotepadSession
    {
        private const string NotepadAppID = @"C:\Windows\System32\notepad.exe";
        private protected static WindowsElement? editBox;

        public static void Setup(TestContext context)
        {
            // Lauch a new instance of Notepad application
            AppDriver.InitApplication(context, NotepadAppID);

            // Verify that Notepad is started with untitled new file
            Assert.AreEqual("Sem título - Bloco de Notas", AppDriver.Session.Title);

            // Keep track of the edit box to be used throughout the session
            editBox = AppDriver.Session.FindElementByClassName("Edit");
            Assert.IsNotNull(editBox);
        }

        public static void TearDown() => AppDriver.QuitApplication("Não Salvar");

        [TestInitialize]
        public void TestInitialize()
        {
            // Select all text and delete to clear the edit box
            editBox.ClearEditor();
            Assert.AreEqual(string.Empty, editBox.Text);
        }

        protected static string SanitizeBackslashes(string input) => input.Replace("\\", Keys.Alt + Keys.NumberPad9 + Keys.NumberPad2 + Keys.Alt);
    }
}
