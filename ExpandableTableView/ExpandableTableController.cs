
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace ExpandableTableView
{
	public class ExpandableTableController : UITableViewController
	{
		public ExpandableTableController () : base (UITableViewStyle.Grouped)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Register the TableView's data source
//			TableView.Source = new ExpandableTableSource<string> ();
			var expandableSource = new ExpandableTableSource<string> ();
			expandableSource.Items = new List<string> { "Fee", "Fie", "Foe", "Fom" };
			TableView.Source = expandableSource;
		}
	}
}

