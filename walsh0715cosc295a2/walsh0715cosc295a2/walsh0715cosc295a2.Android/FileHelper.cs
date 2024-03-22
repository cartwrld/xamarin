using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xamarin.Forms;
using walsh0715cosc295a2;
// modify for your namespace ///
[assembly: Dependency(typeof(walsh0715cosc295a2.Droid.FileHelper))]    // tells xamarin that this is the Android FileHelper
namespace walsh0715cosc295a2.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}