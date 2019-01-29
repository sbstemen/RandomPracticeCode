﻿// *************************************************************
// Coder Camps
// 8444 N. 90th Street St. 110
// Scottsdale, AZ
// -- SBS ~ 20180830
// Copyright (c) 2016-18
// Project: Runs through the repetitive upload test course
// *************************************************************

namespace LottaUpload
{
  using System;
  using System.IO;
  using OpenQA.Selenium;
  using OpenQA.Selenium.Chrome;
  using OpenQA.Selenium.Interactions;
  using SeleniumExtras;
  using SeleniumExtras.PageObjects;
  using SeleniumExtras.WaitHelpers;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Microsoft.VisualStudio.TestTools;
  
  [TestClass]
  public class UpLoadLoop
  {
    private static string assetPath = Directory.GetCurrentDirectory() + "\\assets\\";
    private static string uploadFile = assetPath + Properties.Settings.Default.NameOfFile;
    private UserData studentData = new UserData();
    private UserData administratorData = new UserData();
    private HelperUtilities HelperUtilities = new HelperUtilities();
    private UploadProcess Process = new UploadProcess();

    [TestInitialize]
    public void TestSetUp()
    {
      studentData.Password = Properties.Settings.Default.Password;
      studentData.LogInAlias = Properties.Settings.Default.BaseUserNames;
      studentData.ClientUrl = Properties.Settings.Default.ClientUrl;
    }

    [TestMethod]
    public void UploadLoopingTest()
    {
      int iterationCount = Properties.Settings.Default.UserStartCount;
      do
      {
        using (IWebDriver webDriver = new ChromeDriver(assetPath))
        {
          Process.BrowserReady(webDriver, HelperUtilities, studentData);

          Process.LogInn(webDriver, HelperUtilities, studentData, iterationCount);

          Process.OpenCourse(webDriver, HelperUtilities, iterationCount);

          for(int lessonCount = 0; lessonCount < 10; lessonCount++)
          { 
            Process.UploadFile(webDriver, HelperUtilities, uploadFile);

            Process.NextLessonPage(webDriver, HelperUtilities, iterationCount, lessonCount);
          }
          Process.LogOff(webDriver, HelperUtilities);
        }

        HelperUtilities.RandomPause(1);
        iterationCount++;
      } while (iterationCount < Properties.Settings.Default.UserStopCount);

    }
  }
}
