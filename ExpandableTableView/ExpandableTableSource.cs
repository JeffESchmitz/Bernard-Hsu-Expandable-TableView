
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace ExpandableTableView
{
	public class ExpandableTableSource<T> : UITableViewSource
	{
		public IReadOnlyList<T> Items;
		protected readonly Action<T> TSelected;
		protected readonly string ParentCellIdentifier = "ParentCell";
		protected readonly string ChildCellIndentifier = "ChildCell";
		protected int currentExpandedIndex = -1;

		public ExpandableTableSource ()
		{
		}

		public ExpandableTableSource(Action<T> TSelected)
		{
			this.TSelected = TSelected;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
//			// TODO: return the actual number of items in the section
//			return 1;
			return Items.Count + ((currentExpandedIndex > -1) ? 1 : 0);
		}

		void collapseSubItemsAtIndex(UITableView tableView, int index)
		{
			tableView.DeleteRows(new[] {NSIndexPath.FromRowSection(index+1, 0)}, UITableViewRowAnimation.Fade);
		}

		void expandItemAtIndex(UITableView tableView, int index)
		{
			int insertPos = index + 1;
			tableView.InsertRows(new[] {NSIndexPath.FromRowSection(insertPos++, 0)}, UITableViewRowAnimation.Fade);
		}

		protected bool isChild(NSIndexPath indexPath)
		{
			return currentExpandedIndex > -1 &&
				indexPath.Row > currentExpandedIndex &&
				indexPath.Row <= currentExpandedIndex + 1;
		}

//		public override int NumberOfSections (UITableView tableView)
//		{
//			// TODO: return the actual number of sections
//			return 1;
//		}
//
//
//		public override string TitleForHeader (UITableView tableView, int section)
//		{
//			return "Header";
//		}
//
//		public override string TitleForFooter (UITableView tableView, int section)
//		{
//			return "Footer";
//		}

		public override void RowSelected(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			if (isChild(indexPath)) {
				//Handle selection of child cell
				Console.WriteLine("You touched a child!");
				tableView.DeselectRow(indexPath, true);
				return;
			}
			tableView.BeginUpdates();
			if (currentExpandedIndex == indexPath.Row) {
				this.collapseSubItemsAtIndex(tableView, currentExpandedIndex);
				currentExpandedIndex = -1;
			} else {
				var shouldCollapse = currentExpandedIndex > -1;
				if (shouldCollapse) {
					this.collapseSubItemsAtIndex(tableView, currentExpandedIndex);
				}
				currentExpandedIndex = (shouldCollapse && indexPath.Row > currentExpandedIndex) ? indexPath.Row - 1 : indexPath.Row;
				this.expandItemAtIndex(tableView, currentExpandedIndex);
			}
			tableView.EndUpdates();
			tableView.DeselectRow(indexPath, true);
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (ExpandableTableCell.Key) as ExpandableTableCell;
			if (cell == null)
				cell = new ExpandableTableCell (UITableViewCellStyle.Default, ExpandableTableCell.Key);
			
			// TODO: populate the cell with the appropriate data based on the indexPath
//			cell.DetailTextLabel.Text = "DetailsTextLabel";
			cell.TextLabel.Text = Items [indexPath.Row].ToString ();

			return cell;
		}
	}
}

