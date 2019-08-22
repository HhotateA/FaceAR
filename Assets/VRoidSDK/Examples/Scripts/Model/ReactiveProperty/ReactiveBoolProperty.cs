namespace VRoidSDK.Example
{
    public class ReactiveBoolProperty : ReactivePropertyBase<bool>
    {
        public ReactiveBoolProperty(bool initValue)
        {
            _value = initValue;
        }
    }
}