using Diner.Models;
using Microsoft.Reporting.WinForms;
using System.Text;

namespace Diner
{
    public partial class Receipt : Form
    {
        private readonly List<Entree> _entrees;
        public Receipt(List<Entree> entrees)
        {
            _entrees = entrees;
            InitializeComponent();
            InitializeReceipt();
        }

        private void InitializeReceipt()
        {
            DateTime currentTime = DateTime.Now;
            ReportParameter[] parameters = new ReportParameter[5];

            parameters[0] = new ReportParameter("CurrentTime", currentTime.ToString());
            parameters[1] = new ReportParameter("Items", GetItems().ToString());
            parameters[2] = new ReportParameter("Quantities", GetQuantities().ToString());
            parameters[3] = new ReportParameter("SubTotals", GetSubTotals().ToString());
            parameters[4] = new ReportParameter("Total", GetTotal().ToString());

            reportViewer.LocalReport.ReportPath = "Reports\\Receipt.rdlc";
            reportViewer.LocalReport.SetParameters(parameters);
            reportViewer.RefreshReport();
        }

        private StringBuilder GetItems()
        {
            var items = new StringBuilder();
            foreach(var item in _entrees)
            {
                items.Append(item.Name + "\n");
            }

            return items;
        }

        private StringBuilder GetQuantities()
        {
            var quantities = new StringBuilder();
            foreach(var item in _entrees)
            {
                quantities.Append(item.Quantity + "\n");
            }

            return quantities;
        }

        private StringBuilder GetSubTotals()
        {
            var subTotals = new StringBuilder();
            foreach(var item in _entrees)
            {
                subTotals.Append(item.Quantity * item.Price + "\n");
            }

            return subTotals;
        }

        private double GetTotal()
        {
            var total = 0.0;
            foreach(var item in _entrees)
            {
                total += item.Price * item.Quantity;
            }

            return total;
        }
    }
}