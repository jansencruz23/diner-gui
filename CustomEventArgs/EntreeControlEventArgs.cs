namespace Diner.CustomEventArgs
{
    public class EntreeControlEventArgs : EventArgs
    {
        public EntreeControl EntreeControl { get; set; }

        public EntreeControlEventArgs(EntreeControl entreeControl)
        {
            EntreeControl = entreeControl;
        }
    }
}