using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using VMS.TPS.Common.Model.API;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ChangeHistory
{
    public partial class MainForm : Form
    {
        private List<ListViewItem> listOfItems;
        private Patient _pat;
        public static void Main(Patient pat)
        {
            System.Windows.Forms.Application.Run(new MainForm(pat));
        }
        public MainForm(Patient pat)
        {
            _pat = pat;
            InitializeComponent(); 
            InitializeGUI();
            string[] arr = new string[5];
            foreach (var course in pat.Courses)
            {
                foreach (PlanSetup plan in course.PlanSetups)
                {
                    if (plan.IsDoseValid)
                    {
                        arr[0] = plan.HistoryDateTime.ToString();
                        arr[1] = course.Id;
                        arr[2] = plan.Id;
                        arr[3] = plan.HistoryUserDisplayName;
                        arr[4] = "Last Modified";
                        ListViewItem item = new ListViewItem(arr);
                        lstHstryChange.Items.Add(item);

                        arr[0] = plan.CreationDateTime.ToString();
                        arr[1] = course.Id;
                        arr[2] = plan.Id;
                        arr[3] = plan.CreationUserName;
                        arr[4] = "Created";

                        item = new ListViewItem(arr);
                        //item.Font = new Font("Times New Roman", 15f);
                        lstHstryChange.Items.Add(item);
                        foreach (var approvalHstry in plan.ApprovalHistory)
                        {
                            if (plan.CreationDateTime != approvalHstry.ApprovalDateTime)
                            {
                                arr[0] = approvalHstry.ApprovalDateTime.ToString();
                                arr[1] = course.Id;
                                arr[2] = plan.Id;
                                arr[3] = plan.HistoryUserDisplayName;
                                arr[4] = approvalHstry.ApprovalStatus.ToString();

                                item = new ListViewItem(arr);
                                //item.Font = new Font("Times New Roman", 15f);
                                lstHstryChange.Items.Add(item);
                            }
                        }
                    }
                }
            }
            foreach (ColumnHeader column in lstHstryChange.Columns)
            {
                column.Width = -1;
            }

            //ImageList imgList = new ImageList();
            //imgList.ImageSize = new Size(1, 15);
            //lstHstryChange.SmallImageList = imgList;
        }
        private void InitializeGUI()
        {
            listOfItems = new List<ListViewItem>();
            // Set the view to show details.
            lstHstryChange.View = View.Details;
            // Allow the user to edit item text.
            lstHstryChange.LabelEdit = true;
            // Allow the user to rearrange columns.
            lstHstryChange.AllowColumnReorder = true;
            // Display check boxes.
            //lstChecker.CheckBoxes = true;
            // Select the item and subitems when selection is made.
            lstHstryChange.FullRowSelect = true;
            // Display grid lines.
            lstHstryChange.GridLines = true;
            // Sort the items in the list in ascending order.
            lstHstryChange.Sorting = SortOrder.Descending;
            lstHstryChange.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lstHstryChange.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            //lstHstryChange.Columns.Add("Time", -1, HorizontalAlignment.Left);
            //lstHstryChange.Columns.Add("Course", -1, HorizontalAlignment.Left);
            //lstHstryChange.Columns.Add("Plan", -1, HorizontalAlignment.Left);
            //lstHstryChange.Columns.Add("User", -1, HorizontalAlignment.Left);
            //lstHstryChange.Columns.Add("Approval status", -1, HorizontalAlignment.Left);
            lstHstryChange.Columns.Add("Time");
            lstHstryChange.Columns.Add("Course");
            lstHstryChange.Columns.Add("Plan");
            lstHstryChange.Columns.Add("User");
            lstHstryChange.Columns.Add("Approval status");
            
        }
    }
}
