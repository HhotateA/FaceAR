namespace VRoidSDK.Example
{
    public class ReactiveStringProperty : ReactivePropertyBase<string>
    {
        public ReactiveStringProperty(string initValue)
        {
            _value = initValue;
        }
    }
}