using System;
using Timehop.StickyHeadersRecyclerview;
using Android.Views;
using Android.Support.V7.Widget;
using Android.Widget;
using Java.Security;
using Android.Graphics;

namespace Sample
{
    public class AnimalsHeadersAdapter : AnimalsAdapter, IStickyRecyclerHeadersAdapter
    {

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.view_item, parent, false);
            return new TextViewHolder(view);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            TextView textView = (TextView)holder.ItemView;
            textView.Text = this.GetItem(position);
        }

        public long GetHeaderId(int position)
        {
            return (position == 0) ? -1 : (int)this.GetItem(position)[0];
        }


        

        public RecyclerView.ViewHolder OnCreateHeaderViewHolder(ViewGroup parent)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.view_header, parent, false);
            return new TextViewHolder(view);
        }

        public void OnBindHeaderViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            TextView textView = (TextView) holder.ItemView;
            textView.Text = this.GetItem(position)[0].ToString();
            holder.ItemView.SetBackgroundColor(GetRandomColor());
        }

        private Color GetRandomColor() 
        {
            SecureRandom rgen = new SecureRandom();
            return Color.HSVToColor(150, new float[]{
                rgen.NextInt(359), 1, 1
            });
        }


        private class TextViewHolder : RecyclerView.ViewHolder
        {
            public TextViewHolder(View view) : base(view)
            {
            }
        }
    }
}