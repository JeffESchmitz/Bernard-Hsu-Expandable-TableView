
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ExpandableTableView
{
	public class ExpandableTableCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("ExpandableTableCell");

		public ExpandableTableCell () : base (UITableViewCellStyle.Value1, Key)
		{
			// TODO: add subviews to the ContentView, set various colors, etc.
//			TextLabel.Text = "TextLabel";
		}

		public ExpandableTableCell (UITableViewCellStyle style, string reuseIdentifier)
			: base(style, reuseIdentifier)
		{
			
		}
	}
}

