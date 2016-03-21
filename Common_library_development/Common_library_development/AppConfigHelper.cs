using System.Configuration;

namespace Common_library_development
{
    /// <summary>
    /// 操作AppConfig
    /// </summary>
    public static class AppConfigHelper
    {
        /// <summary>
        /// 返回＊.exe.config文件中appSettings配置节的value项
        /// </summary>
        /// <param name="strKey">配置节点的名字</param>
        /// <returns>config文件中appSettings配置节的value项</returns>
        public static string GetAppConfig(string strKey)
        {
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key == strKey)
                {
                    return ConfigurationManager.AppSettings[strKey];
                }
            }
            return null;
        }
        /// <summary>
        /// 在＊.exe.config文件中appSettings配置节增加一对键、值对
        /// 如果有就修改一对键值对，如果没有就增加一对键值对。
        /// </summary>
        /// <param name="newKey">appSettings的key</param>
        /// <param name="newValue">appSettings的Value</param>
        public static void UpdateAppConfig(string newKey, string newValue)
        {
            bool isModified = false;
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key == newKey)
                {
                    isModified = true;
                }
            }

            // Open App.Config of executable
            Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            // You need to remove the old settings object before you can replace it
            if (isModified)
            {
                config.AppSettings.Settings.Remove(newKey);
            }
            // Add an Application Setting.
            config.AppSettings.Settings.Add(newKey, newValue);
            // Save the changes in App.config file.
            config.Save(ConfigurationSaveMode.Modified);
            // Force a reload of a changed section.
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
