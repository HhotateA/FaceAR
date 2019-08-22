using System.Collections.Generic;
using System;
namespace VRoidSDK.Editor
{
    public class IOSUriSchemeValidator : IValidator
    {
        private UriSchemeValidator Validator;

        public IOSUriSchemeValidator(SDKConfiguration configuration)
        {
            Validator = new UriSchemeValidator(configuration.IOSUrlScheme);
        }

        public bool Validate()
        {
            return Validator.Validate();
        }
    }
}