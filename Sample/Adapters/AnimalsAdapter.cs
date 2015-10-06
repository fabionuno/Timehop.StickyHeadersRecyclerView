using System;
using Android.Support.V7.Widget;
using System.Collections.Generic;

namespace Sample
{
    public abstract class AnimalsAdapter : RecyclerView.Adapter
    {
        private List<string> items = new List<string>();

        public AnimalsAdapter()
        {
            this.HasStableIds = true;
        }

        public void Add(string obj)
        {
            items.Add(obj);
            this.NotifyDataSetChanged();
        }

        public void AddAll(ICollection<string> collection) 
        {
            if (collection != null) 
            {
                this.items.AddRange(collection);
                this.NotifyDataSetChanged();
            }
        }
            

        public void Clear() 
        {
            this.items.Clear();
            this.NotifyDataSetChanged();
        }

        public void Remove(String obj)
        {
            this.items.Remove(obj);
            this.NotifyDataSetChanged();
        }

        public String GetItem(int position) 
        {
            return this.items[position];
        }

        public override long GetItemId(int position)
        {
            return this.items[position].GetHashCode();
        }

        public override int ItemCount
        {
            get
            {
                return this.items.Count;
            }
        }
    }
}

