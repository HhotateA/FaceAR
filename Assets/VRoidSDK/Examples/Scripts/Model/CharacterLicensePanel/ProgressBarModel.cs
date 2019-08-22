namespace VRoidSDK.Example
{
    public class ProgressBarModel
    {
        public ReactiveBoolProperty ProgressBarActive { get; private set; }
        public ReactiveFloatProperty ProgressBarValue { get; private set; }

        public ProgressBarModel()
        {
            ProgressBarActive = new ReactiveBoolProperty(false);
            ProgressBarValue = new ReactiveFloatProperty(0);
        }

        public void UpdateProgress(float progress)
        {
            if (progress <= 0f)
            {
                ProgressBarActive.Set(false);
                return;
            }

            ProgressBarActive.Set(true);
            ProgressBarValue.Set(progress);
        }
    }
}