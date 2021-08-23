using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Baby;

public class BabyTest
{
    [Test]
    public void LoadingBabyTest()
    {
        BabyInfo babyInfo = new BabyInfo();
        Dictionary<string, int> level = new Dictionary<string, int>();
        Dictionary<string, Dictionary<string, int>> appearance = 
            new Dictionary<string, Dictionary<string, int>>();
        Dictionary<string, int> appearanceFeature = 
            new Dictionary<string, int>();
        Dictionary<string, Decimal> temperament = 
            new Dictionary<string, Decimal>();

        level.Add("toilet", 1);

        appearanceFeature.Add("aa", 2);
        appearance.Add("unchangeable", appearanceFeature);
        appearance.Add("changeable", appearanceFeature);

        temperament.Add("aaaa", Convert.ToDecimal(5.55));

        babyInfo.ID = "aa";
        babyInfo.Name = "bb";
        babyInfo.Months = 5;
        babyInfo.Level = level;
        babyInfo.Weight = Convert.ToDecimal(4.52);
        babyInfo.Appearance = appearance;
        babyInfo.Temperament = temperament;

        Assert.Throws<InvalidOperationException>(() => babyInfo.ID = "aaa");
        Assert.Throws<InvalidOperationException>(() => babyInfo.Name = "aaa");
        babyInfo.Months++;
        Assert.AreEqual(babyInfo.Months, 6);
        Assert.Throws<InvalidOperationException>(() => babyInfo.Level = level);
        Assert.Throws<FormatException>(() => babyInfo.UpdateLevel("aaa", 3));
        babyInfo.UpdateLevel("toilet", 2);
        Assert.AreEqual(babyInfo.Level["toilet"], 2);
        Assert.Throws<ArgumentOutOfRangeException>(() => babyInfo.Weight = -2);
        babyInfo.Weight++;
        Assert.AreEqual(babyInfo.Weight, Convert.ToDecimal(5.52));
        Assert.Throws<InvalidOperationException>
        (
            () => babyInfo.Appearance = appearance
        );
        Assert.Throws<InvalidOperationException>
        (
            () => babyInfo.UpdateAppearance("unchangeable", "aa", 3)
        );
        babyInfo.UpdateAppearance("changeable", "aa", 5);
        Assert.AreEqual(babyInfo.Appearance["changeable"]["aa"], 5);
        Assert.Throws<InvalidOperationException>
        (
            () => babyInfo.Temperament = temperament
        );
    }

    [Test]
    public void checkBehaviorWebRequestSendPost()
    {
        // BasicHttpClient http_client = 
        //     (new GameObject("BasicHttpClient")).AddComponent();
        // IGoedleUploadHandler goedleUploadHandler = Substitute.For();
        // IGoedleWebRequest goedleWebRequest = Substitute.For();
        // string stringContent = null;
        
        // goedleUploadHandler.add(Arg.Do(x => stringContent = x));
        // goedleWebRequest.isHttpError.Returns(false);
        // goedleWebRequest.isNetworkError.Returns(false);
        // http_client
        //     .Post
        //     (
        //         "http://127.0.0.1/User/SignIn",
        //         "{\"userID\": \"1229kym\", \"userPWD\": \"abcd1234\"}", 
        //         goedleWebRequest, 
        //         goedleUploadHandler
        //     );
        // var result = JSON.Parse(stringContent);
        // Assert.AreEqual(result["userID"].Value, "1229kym");
        // Assert.AreEqual(result["userPWD"].Value, "abcd1234");
        // Assert.AreEqual
        // (
        //     goedleWebRequest.responseCode, 204
        // );
    }
}