using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Content;

namespace Sample
{
    public class RecyclerItemClickListener : Java.Lang.Object, RecyclerView.IOnItemTouchListener
    {
        private readonly GestureDetector gestureDetector;

        public RecyclerItemClickListener(Context context)
        {
            this.gestureDetector = new GestureDetector(context, new ItemClickGestureListener());
        }

        public event EventHandler<RecyclerItemClickEventArgs> Click;

        public bool OnInterceptTouchEvent(RecyclerView rv, MotionEvent e)
        {
            View childView = rv.FindChildViewUnder(e.GetX(), e.GetY());
            if (childView != null && this.gestureDetector.OnTouchEvent(e)) 
            {
                this.OnClick(rv, childView, rv.GetChildAdapterPosition(childView));
            }

            return false;
        }

        public void OnRequestDisallowInterceptTouchEvent(bool disallowIntercept)
        {
            
        }

        public void OnTouchEvent(RecyclerView rv, MotionEvent @event)
        {
            
        }
    
        protected virtual void OnClick(RecyclerView sender, View view, int position)
        {
            EventHandler<RecyclerItemClickEventArgs> h = this.Click;
            if (h != null)
                h(sender, new RecyclerItemClickEventArgs(view, position));
        }

        private class ItemClickGestureListener : GestureDetector.SimpleOnGestureListener
        {
            public ItemClickGestureListener()
            {
            }

            public override bool OnSingleTapUp(MotionEvent e)
            {
                return true;
            }
        }
    }
}