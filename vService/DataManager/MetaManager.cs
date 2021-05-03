using System.Deployment.Application;
using System.Reflection;

namespace DataService.DataManager {

    internal sealed class MetaManager {


        /// <summary>
        /// Gets the company name.
        /// </summary>
        public string ProductName => "ESchool";


        /// <summary>
        /// AssemblyProductVersion
        /// </summary>
        public string AssemblyProductVersion {
            get {
                var attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false);
                return attributes.Length==0 ?
                    "" :
                    ((AssemblyInformationalVersionAttribute)attributes[0]).InformationalVersion;
            }
        }


        /// <summary>
        /// La version Actuelle
        /// </summary>
        public string CurrentVersion => ApplicationDeployment.IsNetworkDeployed
            ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
            : Assembly.GetExecutingAssembly().GetName().Version.ToString();


        /// <summary>
        /// VersionNumber
        /// </summary>
        public string VersionNumber => Assembly.GetExecutingAssembly().GetName().Version.ToString();


        /// <summary>
        /// Gets the company name.
        /// </summary>
        public string CompanyName => "Matrix Technology";


        /// <summary>
        /// Gets the developper name.
        /// </summary>
        public string DevelopperName => "Halidou Cisse";


        /// <summary>
        /// Gets the copyright banner.
        /// </summary>
        public string[] CopyrightBanner => new[]
        {
            "Licensed under the Apache License, Version 2.0 (the \"License\");",
            "you may not use this file except in compliance with the License.",
            "You may obtain a copy of the License at",
            string.Empty,
            "    http://www.apache.org/licenses/LICENSE-2.0",
            string.Empty,
            "Unless required by applicable law or agreed to in writing, software",
            "distributed under the License is distributed on an \"AS IS\" BASIS,",
            "WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.",
            "See the License for the specific language governing permissions and",
            "limitations under the License."
        };

        public int CopyrightStartYear => 2014;
    }
}
