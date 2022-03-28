using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace NotepadTestUI
{
    [TestClass]
    public class ScenarioEditor : NotepadSession
    {
        [TestMethod]
        public void EditorEnterTest()
        {
            // Digita um texto misto e aplica o modificador de deslocamento a 7890_ para gerar os símbolos correspondentes
            AppDriver.Session.Waiting(2000);
            editBox.SendKeys("abcdeABCDE 12345" + Keys.Shift + "7890-" + Keys.Shift + @"!@#$%");
            Assert.AreEqual(@"abcdeABCDE 12345&*()_!@#$%", editBox.Text);
        }

        [TestMethod]
        public void EditorKeyboardShortcut()
        {
            // Digita uma sequência de texto, seleciona, copia e cola três vezes
            editBox.SendKeys("789");
            editBox.SendKeys(Keys.Control + "a" + Keys.Control);    // Seleciona todo o texto
            editBox.SendKeys(Keys.Control + "c" + Keys.Control);    // Copia utilizando o atalho Ctrl + C
            editBox.SendKeys(Keys.Control + "vvv" + Keys.Control);  // Cola 3x utilizando o atalho Ctrl + V
            Assert.AreEqual("789789789", editBox.Text);
        }

        [TestMethod]
        public void EditorNonPrintableShortcutKey()
        {
            // Pressiona F5 para obter a hora/data do bloco de notas
            Assert.AreEqual(string.Empty, editBox.Text);
            editBox.SendKeys(Keys.F5);
            Assert.AreNotEqual(string.Empty, editBox.Text);
        }

        [TestMethod]
        public void ClearEditor()
        {
            editBox.SendKeys("limpando texto atual");
            editBox.Clear();
            Assert.AreEqual(string.Empty, editBox.Text);
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Setup(context);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }
    }
}