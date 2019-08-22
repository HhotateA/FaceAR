using System.Collections.Generic;
using System;
namespace VRoidSDK.Editor
{
    public class UriSchemeValidator : IValidator
    {
        private string UriString;
        private List<string> ExcludeUriScheme;

        public UriSchemeValidator(string uri)
        {
            UriString = uri;
            ExcludeUriScheme = new List<string>()
            {
                "my-vroidsdk-app"
            };
        }

        public bool Validate()
        {
            // 空文字の場合は設定不要なのでチェックしない
            if (string.IsNullOrEmpty(UriString))
            {
                return true;
            }

            try
            {
                var uri = new Uri(UriString);
                if (ExcludeUriScheme.Contains(uri.Scheme))
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}