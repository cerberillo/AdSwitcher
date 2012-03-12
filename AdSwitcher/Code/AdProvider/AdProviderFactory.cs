﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdSwitcher.Code.AdProvider
{
    public class AdProviderFactory
    {
        #region Singleton

        private static AdProviderFactory _instance;
        public static AdProviderFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AdProviderFactory();
                return _instance;
            }
        }
        private AdProviderFactory()
        {
            //TODO: Initialize attributes here
        }

        #endregion

        public IAdProvider CreateProvider(AdProviderSettings description)
        {
            Debug.WriteLine("AdProviderFactory -> CreateProvider");

            IAdProvider result = null;

            string adType = description.Name.ToLower();
            float priority = 0f;

            if (description.Priority > 0 || description.Priority <= 100)
                priority = description.Priority / 100;

            if (String.Equals(adType, Constants.PROVIDER_PUBCENTER))
            {
                if (String.IsNullOrEmpty(description.PubcenterAdUnitID) || String.IsNullOrEmpty(description.PubcenterApplicationID))
                    return result;

                Debug.WriteLine("AdProviderFactory -> CreateProvider -> PUBCENTER");

                PubcenterSystem pubcenterSystem = new PubcenterSystem();
                pubcenterSystem.AdUnitID = description.PubcenterAdUnitID;
                pubcenterSystem.ApplicationID = description.PubcenterApplicationID;
                pubcenterSystem.Priority = priority;

                result = pubcenterSystem;
            }
            else if (String.Equals(adType, Constants.PROVIDER_ADMOB))
            {
                if (String.IsNullOrEmpty(description.AdmobAdUnitID))
                    return result;

                Debug.WriteLine("AdProviderFactory -> CreateProvider -> ADMOB");

                AdmobSystem admobSystem = new AdmobSystem();
                admobSystem.AdUnitID = description.AdmobAdUnitID;
                admobSystem.Priority = priority;

                result = admobSystem;
            }
            else if (String.Equals(adType, Constants.PROVIDER_MILLENIALMEDIA))
            {
                Debug.WriteLine("AdProviderFactory -> CreateProvider -> MILLENIALMEDIA");
                //TODO: Create MILLENIALMEDIA provider
            }
            else if (String.Equals(adType, Constants.PROVIDER_SMAATO))
            {
                Debug.WriteLine("AdProviderFactory -> CreateProvider -> SMAATO");

                SmaatoSystem smaatoSystem = new SmaatoSystem();
                smaatoSystem.AdSpaceID = description.SmaatoSpaceID;
                smaatoSystem.PublisherID = description.SmaatoPublisherID;
                smaatoSystem.Priority = priority;

                result = smaatoSystem;

            }
            else if (String.Equals(adType, Constants.PROVIDER_MOBFOX))
            {
                Debug.WriteLine("AdProviderFactory -> CreateProvider -> MOBFOX");
                //TODO: Create MOBFOX provider
            }
            else if (String.Equals(adType, Constants.PROVIDER_ADDUPLEX))
            {
                if (String.IsNullOrEmpty(description.AdduplexAppID))
                    return result;

                Debug.WriteLine("AdProviderFactory -> CreateProvider -> ADDUPLEX");

                AdduplexSystem adduplexSystem = new AdduplexSystem();
                adduplexSystem.AppID = description.AdduplexAppID;
                adduplexSystem.Priority = priority;

                result = adduplexSystem;                
            }
            else if (String.Equals(adType, Constants.PROVIDER_INNERACTIVE))
            {
                Debug.WriteLine("AdProviderFactory -> CreateProvider -> INNERACTIVE");
                //TODO: Create INNERACTIVE provider
            }

            return result;
        }
    }
}
