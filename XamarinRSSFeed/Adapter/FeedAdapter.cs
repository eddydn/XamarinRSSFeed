using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using XamarinRSSFeed.Interface;
using XamarinRSSFeed.Model;

namespace XamarinRSSFeed.Adapter
{
    class FeedViewHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        public TextView txtTitle, txtContent, txtPubDate;
        public ItemClickListener itemClickListener { get; set; }


        public FeedViewHolder(View itemView):base(itemView)
        {
            txtTitle = (TextView)itemView.FindViewById(Resource.Id.txtTitle);
            txtPubDate = (TextView)itemView.FindViewById(Resource.Id.txtPubDate);
            txtContent = (TextView)itemView.FindViewById(Resource.Id.txtContent);

            //Set Event
            itemView.SetOnClickListener(this);
            itemView.SetOnLongClickListener(this);
        }

      
        public void OnClick(View v)
        {
            itemClickListener.OnClick(v, AdapterPosition, false);
        }

        public bool OnLongClick(View v)
        {
            itemClickListener.OnClick(v, AdapterPosition, true);
            return true;
        }
    }
    public class FeedAdapter : RecyclerView.Adapter, ItemClickListener
    {
        private RssObject rssObject;
        private Context mContext;
        private LayoutInflater inflater;

        public FeedAdapter(RssObject rssObject,Context mContext)
        {
            this.rssObject = rssObject;
            this.mContext = mContext;
            inflater = LayoutInflater.From(mContext);
        }


        public override int ItemCount => rssObject.items.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            FeedViewHolder hh = holder as FeedViewHolder;

            hh.txtTitle.Text = rssObject.items[position].title;
            hh.txtPubDate.Text = rssObject.items[position].pubDate;
            hh.txtContent.Text = rssObject.items[position].content;

            hh.itemClickListener = this;
        }

        public void OnClick(View view, int position, bool isLongClick)
        {
            if(!isLongClick)
            {
                Intent intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(rssObject.items[position].link));
                mContext.StartActivity(intent);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = inflater.Inflate(Resource.Layout.Row, parent, false);
            return new FeedViewHolder(itemView);
        }
    }
}