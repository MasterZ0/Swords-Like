namespace Z3.DemoSkull.Inputs
{
    public abstract class BaseInput
    {
        protected readonly Controls controls;

        public BaseInput(bool enable)
        {
            controls = new Controls();

            if (enable)
            {
                controls.Enable();
            }
        }

        public void SetActive(bool active)
        {
            if (active)
            {
                controls.Enable();
            }
            else
            {
                controls.Disable();
            }
        }

        public void Dispose()
        {
            controls.Dispose();
        }
    }
}