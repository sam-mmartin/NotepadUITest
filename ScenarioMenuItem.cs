using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;

namespace NotepadTestUI
{
    [TestClass]
    public class ScenarioMenuItem : NotepadSession
    {
        private WindowsElement MenuEditar => AppDriver.Session.FindElementByName("Editar");
        private WindowsElement MenuEditarHoraData => AppDriver.Session.FindElementByXPath($"//MenuItem[starts-with(@Name, \"Hora/data\")]");
        private WindowsElement MenuEditarSelecionarTudo => AppDriver.Session.FindElementByXPath($"//MenuItem[starts-with(@Name, \"Selecionar tudo\")]");
        private WindowsElement MenuEditarCopiar => AppDriver.Session.FindElementByXPath($"//MenuItem[starts-with(@Name, \"Copiar\")]");
        private WindowsElement MenuEditarColar => AppDriver.Session.FindElementByXPath($"//MenuItem[starts-with(@Name, \"Colar\")]");

        [TestMethod]
        public void MenuItemEdit()
        {
            // Seleciona o menu Editar -> Hora/Data e retorna a hora/data para o notepad
            Assert.AreEqual(expected: string.Empty,
                            actual: editBox.Text);
            MenuEditar.Clicar();
            MenuEditarHoraData.Clicar();
            string timeDateString = editBox.Text;
            Assert.AreNotEqual(notExpected: string.Empty,
                               actual: timeDateString);

            // Seleciona todo texto, copia e cola duas vezes usando o menu Editar
            MenuEditar.Clicar();
            MenuEditarSelecionarTudo.Clicar();
            MenuEditar.Clicar();
            MenuEditarCopiar.Clicar();
            MenuEditar.Clicar();
            MenuEditarColar.Clicar();
            MenuEditar.Clicar();
            MenuEditarColar.Clicar();

            Assert.AreEqual(expected: timeDateString + timeDateString,
                            actual: editBox.Text);
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
