using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using Timehop.StickyHeadersRecyclerview;
using Android.Content.PM;
using Android.Support.V7.App;

namespace Sample
{
    [Activity(MainLauncher = true, Theme="@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        private ToggleButton isReverseButton;
        private Button updateButton;
        private LinearLayoutManager layoutManager;

        private AnimalsHeadersAdapter adapter;
        private StickyRecyclerHeadersTouchListener stickyTouchListener;
        private RecyclerItemClickListener recyclerItemClick;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.activity_main);

            RecyclerView recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerview);
            this.updateButton = FindViewById<Button>(Resource.Id.button_update);
            this.isReverseButton = FindViewById<ToggleButton>(Resource.Id.button_is_reverse);			
           
            // Set adapter populated with example dummy data
            this.adapter = new AnimalsHeadersAdapter();
            adapter.Add("Animals below!");
            adapter.AddAll(this.GetDummyDataSet());
            recyclerView.SetAdapter(adapter);

            //adapter.RegisterAdapterDataObserver(

            // Set layout manager
            int orientation = this.GetLayoutManagerOrientation(this.Resources.Configuration.Orientation);
            this.layoutManager = new LinearLayoutManager(this, orientation, isReverseButton.Checked);
            recyclerView.SetLayoutManager(layoutManager);

            // Add the sticky headers decoration
            StickyRecyclerHeadersDecoration headersDecor = new StickyRecyclerHeadersDecoration(adapter);
            recyclerView.AddItemDecoration(headersDecor);

            // Add decoration for dividers between list items
            recyclerView.AddItemDecoration(new DividerDecoration(this));

            this.adapter.RegisterAdapterDataObserver(new HeaderAdapterObserver(headersDecor));

            // Add touch listeners
            this.stickyTouchListener = new StickyRecyclerHeadersTouchListener(recyclerView, headersDecor);
            recyclerView.AddOnItemTouchListener(this.stickyTouchListener);

            this.recyclerItemClick = new RecyclerItemClickListener(this);
            recyclerView.AddOnItemTouchListener(recyclerItemClick);
        }

        protected override void OnResume()
        {
            base.OnResume();
            this.isReverseButton.Click += IsReverseButton_Click;
            this.updateButton.Click += UpdateButton_Click;
            this.stickyTouchListener.HeaderClick += StickyTouchListener_HeaderClick;
            this.recyclerItemClick.Click += RecyclerItemClick_Click;
        }

        protected override void OnPause()
        {
            base.OnPause();
            this.isReverseButton.Click -= IsReverseButton_Click;
            this.updateButton.Click -= UpdateButton_Click;
            this.stickyTouchListener.HeaderClick -= StickyTouchListener_HeaderClick;
            this.recyclerItemClick.Click -= RecyclerItemClick_Click;
        }

        private void IsReverseButton_Click (object sender, EventArgs e)
        {
            bool isChecked = this.isReverseButton.Checked;
            this.isReverseButton.Checked = isChecked;
            this.layoutManager.ReverseLayout = isChecked;
            this.adapter.NotifyDataSetChanged();
        }

        private void UpdateButton_Click (object sender, EventArgs e)
        {
            Handler handler = new Handler(Looper.MainLooper);
            for (int i = 0; i < this.adapter.ItemCount; i++) 
            {
                handler.PostDelayed(() =>
                    {
                        this.adapter.NotifyItemChanged(i);
                    }, 50);
            }
        }

        private void StickyTouchListener_HeaderClick (object sender, StickyRecyclerHeadersTouchListener.HeaderClickEventArgs e)
        {
            Toast.MakeText(this, $"Header position: {e.Position}, id:{e.HeaderId}", ToastLength.Short).Show();
        }

        private void RecyclerItemClick_Click (object sender, RecyclerItemClickEventArgs e)
        {
            this.adapter.Remove(this.adapter.GetItem(e.Position));
        }

        private int GetLayoutManagerOrientation(Android.Content.Res.Orientation activityOrientation) 
        {
            if (activityOrientation == Android.Content.Res.Orientation.Portrait)
            {
                return LinearLayoutManager.Vertical;
            } 
            else 
            {
                return LinearLayoutManager.Horizontal;
            }
        }

        private String[] GetDummyDataSet() 
        {
            return this.Resources.GetStringArray(Resource.Array.animals);
        }
    
        private class HeaderAdapterObserver : RecyclerView.AdapterDataObserver
        {
            private readonly StickyRecyclerHeadersDecoration headersDecor;

            public HeaderAdapterObserver(StickyRecyclerHeadersDecoration headersDecor)
            {
                this.headersDecor = headersDecor;
            }

            public override void OnChanged()
            {
                this.headersDecor.InvalidateHeaders();
            }
        }
    }
}