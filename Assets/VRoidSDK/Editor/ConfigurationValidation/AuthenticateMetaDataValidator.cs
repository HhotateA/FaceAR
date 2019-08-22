using System.Reflection;
using UnityEngine;

namespace VRoidSDK.Editor
{
    public class AuthenticateMetaDataValidator : IValidator
    {
        private SDKConfiguration Configuration;

        public AuthenticateMetaDataValidator(SDKConfiguration configuration)
        {
            Configuration = configuration;
        }

        public bool Validate()
        {
            var appIdField = Configuration.GetType().GetField("ApplicationId", BindingFlags.Instance | BindingFlags.NonPublic);
            var secretIdField = Configuration.GetType().GetField("Secret", BindingFlags.Instance | BindingFlags.NonPublic);

            var appId = (string)appIdField.GetValue(Configuration);
            var secret = (string) secretIdField.GetValue(Configuration);

            if (string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(secret))
            {
                return false;
            }

            return true;
        }
    }
}