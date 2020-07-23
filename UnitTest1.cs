using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Winium;

namespace ComplianceCheck
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                string winiumdriverlink = @"C:\Users\pramo_gsxqos1\Downloads\Winium.Desktop.Driver";
                string inspectpath = @"C:\Program Files (x86)\Windows Kits\10\bin\10.0.19041.0\x64";

                var service = WiniumDriverService.CreateDesktopService(winiumdriverlink);

                var controlPanel = "control.exe";
                var notepad = "notepad.exe";
                var internetexplorer = "iexplore.exe";

                var notepadLocation = @"C:\Windows\System32\notepad.exe";
                var controlLocation = LocateEXE(controlPanel);
                var iexploreLocation = @"C:\Program Files (x86)\Internet Explorer\iexplore.exe";


                var controlPanelOptions = new DesktopOptions { ApplicationPath = controlLocation };
                var notepadOptions = new DesktopOptions { ApplicationPath = notepadLocation };
                var iexploreOptions = new DesktopOptions { ApplicationPath = iexploreLocation };



                WiniumDriver windriver = new WiniumDriver(service, iexploreOptions, TimeSpan.FromSeconds(60));
                //WiniumDriver windriver2 = new WiniumDriver(service, notepadOptions, TimeSpan.FromSeconds(60));
                string parent = windriver.CurrentWindowHandle;
                Thread.Sleep(1000);
                windriver.FindElementByName("Tools").Click();
                Thread.Sleep(1000);
                String child = windriver.CurrentWindowHandle;
                windriver.FindElementByName("Internet options").Click();
                Thread.Sleep(1000);
                windriver.FindElementByName("Advanced").Click();
                Thread.Sleep(1000);
                var iseabled= windriver.FindElementByName("Reset...").GetAttribute("IsEnabled");
                Thread.Sleep(1000);
                //windriver.FindElementByName("Reset...").Click();
                //Thread.Sleep(1000);
                windriver.FindElementByName("Close").Click();
               
                WiniumDriver windriver2 = new WiniumDriver(service, notepadOptions, TimeSpan.FromSeconds(60));
                Thread.Sleep(1000);
                windriver2.FindElementByClassName("Edit").SendKeys("isEnabled:-   " + iseabled);

                #region timout
                //WiniumDriver windriver = new WiniumDriver(service, controlPanelOptions, TimeSpan.FromSeconds(60));
                //Thread.Sleep(1000);

                //string parent = windriver.CurrentWindowHandle;

                //windriver.FindElementByClassName("SearchEditBox").SendKeys("change screen saver");
                //Thread.Sleep(1000);


                //windriver.FindElementById("tasklink").Click();
                //Thread.Sleep(1000);

                //string child = windriver.CurrentWindowHandle;

                //IWebElement window = windriver.FindElementByClassName("#32770");
                //Thread.Sleep(1000);
                ////windriver.FindElementByClassName("#32770");

                ////String child  = windriver.CurrentWindowHandle;

                //windriver.SwitchTo().Window(parent);
                //Thread.Sleep(1000);
                //windriver.FindElementByName("Close").Click();
                //Thread.Sleep(1000);
                //windriver.SwitchTo().Window(child);
                //Thread.Sleep(1000);
                //// IWebElement scwindow = windriver.FindElementByClassName("#32770");

                //var scwindow = windriver.FindElementByName("Screen Saver Settings");
                //var scwindow2 = scwindow.FindElement(By.Name("Screen Saver"));

                //var list = scwindow2.FindElements(By.XPath(".//*"));

                //Thread.Sleep(2000);
                //var x = "";
                //for (int i = 0; i < list.Count; i++)
                //{
                //    var xx = list[i].GetAttribute("AutomationId");
                //    var xxx = list[i].GetAttribute("Name");

                //    if (xx == "1307")
                //    {
                //        x = x + list[i].GetAttribute("IsEnabled");

                //    }

                //}
                ////var x = scwindow.FindElement(By.ClassName("Edit")).TagName;
                //Thread.Sleep(2000);

                //WiniumDriver windriver2 = new WiniumDriver(service, notepadOptions, TimeSpan.FromSeconds(60));
                //windriver2.FindElementByClassName("Edit").SendKeys("TimeoutEnabled:-   " + x);
                #endregion

            }
            catch (Exception ex)
            {
                var xs = ex.StackTrace;
            }
        }

        private string LocateEXE(String filename)
        {
            String path = Environment.GetEnvironmentVariable("path");
            String[] folders = path.Split(';');
            foreach (String folder in folders)
            {
                if (File.Exists(folder + filename))
                {
                    return folder + filename;
                }
                else if (File.Exists(folder + "\\" + filename))
                {
                    return folder + "\\" + filename;
                }
            }

            return String.Empty;
        }
    }
}
