using System;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Graphics.Drawables;
using Android.Content.Res;
using Android.Graphics;
using Android.Views;
using Java.Interop;

namespace Sample
{
    public class DividerDecoration : RecyclerView.ItemDecoration
    {
        private static int[] Attrs = new int[]{ Android.Resource.Attribute.ListDivider };
        private Drawable divider;

        public DividerDecoration(Context context)
        {
            TypedArray a = context.ObtainStyledAttributes(Attrs);
            this.divider = a.GetDrawable(0);
            a.Recycle();
        }

        public override void OnDraw(Android.Graphics.Canvas c, RecyclerView parent, RecyclerView.State state)
        {
            base.OnDraw(c, parent, state);

            if (this.GetOrientation(parent) == LinearLayoutManager.Vertical) 
            {
                this.DrawVertical(c, parent);
            } else {
                this.DrawHorizontal(c, parent);
            }
        }

        public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
        {
            base.GetItemOffsets(outRect, view, parent, state);
            if (this.GetOrientation(parent) == LinearLayoutManager.Vertical) 
            {
                outRect.Set(0, 0, 0, this.divider.IntrinsicHeight);
            } else {
                outRect.Set(0, 0, this.divider.IntrinsicWidth, 0);
            }
        }

        private void DrawVertical(Canvas c, RecyclerView parent) 
        {
            int left = parent.PaddingLeft;
            int right = parent.Width - parent.PaddingRight;
            int recyclerViewTop = parent.PaddingTop;
            int recyclerViewBottom = parent.Height - parent.PaddingBottom;
            int childCount = parent.ChildCount;

            for (int i = 0; i < childCount; i++) 
            {
                View child = parent.GetChildAt(i);
                RecyclerView.LayoutParams layoutParams = child.LayoutParameters.JavaCast<RecyclerView.LayoutParams>();
                int top = Math.Max(recyclerViewTop, child.Bottom + layoutParams.BottomMargin);
                int bottom = Math.Min(recyclerViewBottom, top + this.divider.IntrinsicHeight);
                this.divider.SetBounds(left, top, right, bottom);
                this.divider.Draw(c);
            }
        }

        private void DrawHorizontal(Canvas c, RecyclerView parent) 
        {
            int top = parent.PaddingTop;
            int bottom = parent.Height - parent.PaddingBottom;
            int recyclerViewLeft = parent.PaddingLeft;
            int recyclerViewRight = parent.Width - parent.PaddingRight;
            int childCount = parent.ChildCount;

            for (int i = 0; i < childCount; i++) 
            {
                View child = parent.GetChildAt(i);
                RecyclerView.LayoutParams layoutParams = child.LayoutParameters.JavaCast<RecyclerView.LayoutParams>();

                int left = Math.Max(recyclerViewLeft, child.Right + layoutParams.RightMargin);
                int right = Math.Min(recyclerViewRight, left + this.divider.IntrinsicHeight);
                this.divider.SetBounds(left, top, right, bottom);
                this.divider.Draw(c);
            }
        }

        private int GetOrientation(RecyclerView parent) 
        {
            if (parent.GetLayoutManager() is LinearLayoutManager) {
                LinearLayoutManager layoutManager = (LinearLayoutManager) parent.GetLayoutManager();
                return layoutManager.Orientation;
            } else {
                throw new ArgumentException("DividerItemDecoration can only be used with a LinearLayoutManager.");
            }
        }
    }
}

