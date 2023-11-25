using Diner.Models;

namespace Diner.CustomEventArgs
{
    public class EntreeEventArgs : EventArgs
    {
        public Entree Entree { get; set; }

        public EntreeEventArgs(Entree entree)
        {
            Entree = entree;
        }
    }
}