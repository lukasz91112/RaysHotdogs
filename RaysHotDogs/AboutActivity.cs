using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;

namespace RaysHotDogs
{
    [Activity(Label = "About Ray's Hot Dogs")]
    public class AboutActivity : Activity
    {
        private TextView phoneNumberTextView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AboutView);

            FindViews();
            HandleEvents();
        }

        private void FindViews()
        {
            phoneNumberTextView = FindViewById<TextView>(Resource.Id.phoneNumberTextView);
        }

        private void HandleEvents()
        {
            phoneNumberTextView.Click += PhoneNumberTextView_Click;
        }

        private void PhoneNumberTextView_Click(object sender, EventArgs e)
        {
            var intent = new Intent(Intent.ActionCall);
            intent.SetData(Android.Net.Uri.Parse("tel:" + phoneNumberTextView.Text));
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.CallPhone) != Android.Content.PM.Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.CallPhone }, 123);
            }
            else
            {
                try
                {
                    StartActivity(intent);
                }
                catch (SecurityException err)
                {
                    var dialog = new AlertDialog.Builder(this);
                    dialog.SetTitle("Error");
                    dialog.SetMessage(err.Message);
                    dialog.Show();
                }
            }
        }

    }
}