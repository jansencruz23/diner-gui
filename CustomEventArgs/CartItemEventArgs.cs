using Diner.UserControls;

namespace Diner.CustomEventArgs
{
    public class CartItemEventArgs : EventArgs
    {
        public CartItem CartItem { get; set; }

        public CartItemEventArgs(CartItem cartItem)
        {
            CartItem = cartItem;
        }
    }
}