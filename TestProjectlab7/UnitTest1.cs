using Microsoft.VisualStudio.TestPlatform.TestHost;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using lab7_test;
using System;

using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using static System.Net.Mime.MediaTypeNames;
using System;

namespace TestProjectlab7
{
    public class Tests
    {
        private IWebDriver chromedriver = new ChromeDriver();
        private string Url;
        private string Email;
        private string Password;
        private string Text;
        private string Messagee;
        [SetUp]
        public void Setup()
        {
            Url = "https://dev.to/";
            Text = "Books That Helped Me Become a Tech Lead";
            Messagee = "OK";
            Email = "kirillzm01@mail.ru";
            Password = "w329cow329co";
        }

        [Test]
        [Order(1)]
        public void TestAuth()
        {

            //Заходим на сайт
            chromedriver.Navigate().GoToUrl(Url);
            Thread.Sleep(4000);
            //клик по авторизации
            IWebElement elem = chromedriver.FindElement(By.CssSelector("#authentication-top-nav-actions > span > a"));
            elem.Click();
            //ввод почты
            elem = chromedriver.FindElement(By.CssSelector("#user_email"));
            elem.Click();
            elem.SendKeys(Email);
            //ввод пароля
            elem = chromedriver.FindElement(By.CssSelector("#user_password"));
            elem.Click();
            elem.SendKeys(Password);

            // Клик на авторизацию
            elem = chromedriver.FindElement(By.CssSelector("#new_user > div.actions.pt-3 > input"));
            elem.Click();

            // Переходим в меню профиля
            elem = chromedriver.FindElement(By.CssSelector("#member-menu-button"));
            elem.Click();

            // Проверяем, равен ли текст в элементе заданному (никнейм)
            Thread.Sleep(4000);
            elem = chromedriver.FindElement(By.CssSelector("#first-nav-link > div > small"));
            Thread.Sleep(4000);
            Assert.That(elem.Text.ToString().Contains("@kirillzm"));
        }

        [Test]
        [Order(2)]
        public void TestSearch()
        {
            chromedriver.Navigate().GoToUrl(Url);
            Thread.Sleep(5000);
            //Жмем на поиск и вводим текст
            IWebElement elem = chromedriver.FindElement(By.CssSelector("#header-search > form > div > div > input"));
            elem.Click();
            elem.SendKeys(Text);
            //жмем найти
            elem = chromedriver.FindElement(By.CssSelector("#header-search > form > div > div > button"));
            elem.Click();

            // Проверяем наличие поста по названию
            Thread.Sleep(10000);
            elem = chromedriver.FindElement(By.Id("article-link-1703602"));
            Thread.Sleep(6000);
            Assert.That(elem.Text.Contains(Text));
            Thread.Sleep(6000);
        }

        [Test]
        [Order(3)]
        public void TestLike()
        {
            chromedriver.Navigate().GoToUrl(Url);
            Thread.Sleep(5000);

            // выбираем первый пост в списке
            IWebElement elem = chromedriver.FindElement(By.CssSelector("#article-link-1703602 > span"));
            elem.Click();
            Thread.Sleep(5000);

            elem = chromedriver.FindElement(By.CssSelector("#reaction_total_count"));
            int likesBefore = Convert.ToInt32(elem.Text);

            elem = chromedriver.FindElement(By.CssSelector("#reaction-drawer-trigger > span.crayons-reaction__icon.crayons-reaction__icon--borderless.crayons-reaction--like.crayons-reaction__icon--inactive > svg"));
            elem.Click();
            Thread.Sleep(4000);
            Assert.That(
                Convert.ToInt32(chromedriver.FindElement(
                    By.CssSelector("#reaction_total_count")
                    ).Text).Equals(likesBefore + 1)
                );
            Thread.Sleep(4000);
        }

        [Test]
        [Order(4)]
        public void TestComment()
        {
            chromedriver.Navigate().GoToUrl(Url);
            Thread.Sleep(6000);

            // выбираем нужный пост в списке
            IWebElement elem = chromedriver.FindElement(By.CssSelector("#article-link-1703602 > span"));
            elem.Click();
            Thread.Sleep(4000);

            elem = chromedriver.FindElement(By.CssSelector("#text-area"));
            elem.Click();
            elem.SendKeys(Messagee);

            // Отправить коммент
            elem = chromedriver.FindElement(By.CssSelector("#new_comment > div.comment-form__inner > div.comment-form__buttons.mb-4 > button:nth-child(1)"));
            elem.Click();

            elem = chromedriver.FindElement(By.Id("comment-trees-container"));
            Thread.Sleep(4000);
            Assert.That(elem.Text.Contains(Messagee));
            Thread.Sleep(4000);
        }
    }
}