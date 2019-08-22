using System.Collections.Generic;
using System;
namespace VRoidSDK.Editor
{
    public class AndroidUriSchemeValidator : IValidator
    {
        private UriSchemeValidator Validator;

        public AndroidUriSchemeValidator(SDKConfiguration configuration)
        {
            Validator = new UriSchemeValidator(configuration.AndroidUrlScheme);
        }

        public bool Validate()
        {
            return Validator.Validate();
        }
    }
}