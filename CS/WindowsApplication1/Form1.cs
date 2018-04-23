using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPivotGrid.Data;

namespace WindowsApplication1 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        DataView CreateDataView() {
            DataTable table = new DataTable();
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("Product", typeof(string));
            table.Columns.Add("Count", typeof(int));

            table.Rows.Add("Beverages", "Juice", 10);
            table.Rows.Add("Beverages", "Juice", 15);
            table.Rows.Add("Beverages", "Vermouth", 150);
            table.Rows.Add("Beverages", "Vermouth", 160);
            table.Rows.Add("Meat", "Beef", 80);
            table.Rows.Add("Meat", "Beef", 150);
            table.Rows.Add("Meat", "Sausage", 40);
            table.Rows.Add("Meat", "Sausage", 48);
            table.Rows.Add("Milk", "Kefir", 24);
            table.Rows.Add("Milk", "Kumiss", 54);
            table.Rows.Add("Milk", "Curd", 40);
            table.Rows.Add("Milk", "Curd", 20);
            table.Rows.Add("Milk", "Cheese", 50);
            table.Rows.Add("Milk", "Cheese", 120);
            table.Rows.Add("Milk", "Cheese", 150);
            return table.DefaultView;
        }

        PivotGridField columnField;
        PivotGridField rowField;
        PivotGridField dataField;

        void AddFields() {
            columnField = pivotGridControl1.Fields.Add("Category", PivotArea.ColumnArea);
            rowField = pivotGridControl1.Fields.Add("Product", PivotArea.RowArea);
            rowField.SortMode = PivotSortMode.Custom;
            dataField = pivotGridControl1.Fields.Add("Count", PivotArea.DataArea);
        }

        private void Form1_Load(object sender, EventArgs e) {
            pivotGridControl1.DataSource = CreateDataView();
            AddFields();
        }

        private void pivotGridControl1_CustomFieldSort(object sender, PivotGridCustomFieldSortEventArgs e) {
            if(!rowField.Equals(e.Field))
                return;
            string value1 = e.Value1.ToString();
            string value2 = e.Value2.ToString();
            int length1 = value1.Length;
            int length2 = value2.Length;
            e.Result = Comparer<int>.Default.Compare(length1, length2);
            if(e.Result == 0)
                e.Result = Comparer<string>.Default.Compare(value1, value2);
            e.Handled = true;
        }

        private void pivotGridControl1_CustomFilterPopupItems(object sender, PivotCustomFilterPopupItemsEventArgs e) {
            if(!rowField.Equals(e.Field))
                return;
            if(rowField.SortOrder == PivotSortOrder.Ascending)
                return;
            List<PivotGridFilterItem> items = new List<PivotGridFilterItem>(e.Items);
            items.Reverse();
            e.Items.Clear();
            foreach(PivotGridFilterItem item in items)
                e.Items.Add(item);
        }
    }
}
