namespace Diner.CustomEventArgs
{
    public class EntreeEventArgs : EventArgs
    {
        public EntreeControl EntreeControl { get; set; }

        public EntreeEventArgs(EntreeControl entreeControl)
        {
            EntreeControl = entreeControl;
        }
    }
}