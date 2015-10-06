using System;
using Android.Views;

namespace Sample
{
    public class RecyclerItemClickEventArgs : EventArgs
    {
        public readonly View View;
        public readonly int Position;

        public RecyclerItemClickEventArgs(View view, int position)
        {
            this.View = view;
            this.Position = position;
        }
    }
}
