using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace alisthelper
{
    static class AutoStartup
    {
        // Don't use Application.ExecutablePath
        // see https://stackoverflow.com/questions/12945805/odd-c-sharp-path-issue
        private static readonly string ExecutablePath = Assembly.GetEntryAssembly().Location;

        private static string Key = "alist_" + Application.StartupPath.GetHashCode();

        public static RegistryKey OpenRegKey(string name, bool writable, RegistryHive hive = RegistryHive.CurrentUser)
        {
            // we are building x86 binary for both x86 and x64, which will
            // cause problem when opening registry key
            // detect operating system instead of CPU
            try
            {
                RegistryKey userKey = RegistryKey.OpenBaseKey(hive,
                        Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32)
                    .OpenSubKey(name, writable);
                return userKey;
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show("OpenRegKey: " + ae.ToString());
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static bool Set(bool enabled)
        {
            RegistryKey runKey = null;
            try
            {
                runKey = OpenRegKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                if (runKey == null)
                {
                    return false;
                }
                if (enabled)
                {
                    runKey.SetValue(Key, ExecutablePath + " -autorun");
                }
                else
                {
                    runKey.DeleteValue(Key);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                if (runKey != null)
                {
                    try
                    {
                        runKey.Close();
                        runKey.Dispose();
                    }
                    catch (Exception e)
                    { }
                }
            }
        }

        public static bool Check()
        {
            RegistryKey runKey = null;
            try
            {
                runKey = OpenRegKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                if (runKey == null)
                {
                    return false;
                }
                string[] runList = runKey.GetValueNames();
                foreach (string item in runList)
                {
                    if (item.Equals(Key, StringComparison.OrdinalIgnoreCase))
                        return true;
                    else if (item.Equals("alist", StringComparison.OrdinalIgnoreCase)) // Compatibility with older versions
                    {
                        string value = Convert.ToString(runKey.GetValue(item));
                        if (ExecutablePath.Equals(value, StringComparison.OrdinalIgnoreCase))
                        {
                            runKey.DeleteValue(item);
                            runKey.SetValue(Key, ExecutablePath);
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception e)
            {

                return false;
            }
            finally
            {
                if (runKey != null)
                {
                    try
                    {
                        runKey.Close();
                        runKey.Dispose();
                    }
                    catch (Exception e)
                    { }
                }
            }
        }
    }
}
