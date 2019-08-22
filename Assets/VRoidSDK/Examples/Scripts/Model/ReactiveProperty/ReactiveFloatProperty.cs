namespace VRoidSDK.Example
{
    public class ReactiveFloatProperty : ReactivePropertyBase<float>
    {
        public ReactiveFloatProperty(float value)
        {
            _value = value;
        }
    }
}