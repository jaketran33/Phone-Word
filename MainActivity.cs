using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;
using Core;
using System;
using System.Collections.Generic;

namespace Phoneword
{
    [Activity(Label = "Phone Word", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

		static readonly List<string> phoneNumbers = new List<string>();

		protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
			Log.Debug(GetType().FullName, "OnCreate called");

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our UI controls from the loaded layout
            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            Button translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
			TextView translatedPhoneWord = FindViewById<TextView>(Resource.Id.TranslatedPhoneWord);
			Button translationHistoryButton = FindViewById<Button>(Resource.Id.TranslationHistoryButton);

			string translatedNumber = string.Empty;

			// When translateButton is clicked
			translateButton.Click += (sender, e) =>
            {
				Log.Debug(GetType().FullName, "translateButton clicked");
                // Translate user’s alphanumeric phone number to numeric
                translatedNumber = PhonewordTranslator.ToNumber(phoneNumberText.Text);
                if (string.IsNullOrWhiteSpace(translatedNumber))
                {
                    translatedPhoneWord.Text = string.Empty;
                }   
                else
                {
                    translatedPhoneWord.Text = translatedNumber;
					phoneNumbers.Add(translatedNumber);
					translationHistoryButton.Enabled = true;
				}
            };

			// When translationHistoryButton is clicked
			translationHistoryButton.Click += (sender, e) =>
			{
				Log.Debug(GetType().FullName, "translationHistoryButton clicked");
				var intent = new Intent(this, typeof(TranslationHistoryActivity));
				intent.PutStringArrayListExtra("phone_numbers", phoneNumbers);
				StartActivity(intent);
			};

		}

		protected override void OnStart()
		{
			base.OnStart();

			Log.Debug(GetType().FullName, "OnStart called");

		}
	}
}