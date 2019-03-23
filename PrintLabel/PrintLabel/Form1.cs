using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.citizen.sdk.LabelPrint;

namespace PrintLabel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DataTable connectTable = new DataTable();
            connectTable.Columns.Add("Type", typeof(string));
            connectTable.Columns.Add("Value", typeof(int));
            connectTable.Rows.Add("USB", LabelConst.CLS_PORT_USB);

            connectType.DataSource = connectTable;
            connectType.DisplayMember = "Type";
            connectType.ValueMember = "Value";
            connectType.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lstPrinters.Items.Clear();
            lstPrinters.Columns.Add("Port", 150, HorizontalAlignment.Left);
            lstPrinters.Columns.Add("", 184, HorizontalAlignment.Left);
        }

        private void lstPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstPrinters.Items.Clear();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
        }
        private void DesignLabel(LabelDesign design)
        {

            this.mediumFont(design, "12 LBS     1 OF 2", 230, 560);

            this.smallFont(design, "CITIZEN SYSTEMS AMERICA", 10, 565);
            this.smallFont(design, "1234567890", 10, 555);
            this.smallFont(design, "STE 404", 10, 545);
            this.smallFont(design, "363 VAN NESS WAY", 10, 535);
            this.smallFont(design, "TORRANCE  CA 90501", 10, 525);

            this.mediumBoldFont(design, "SHIP", 15, 500);
            this.mediumBoldFont(design, "TO:", 19, 472);

            this.mediumFont(design, "LYMAN C SMITH", 60, 500);
            this.mediumFont(design, "9876543210", 60, 482);
            this.mediumFont(design, "SMITH TOWER", 60, 464);
            this.mediumFont(design, "STE 220", 60, 446);
            this.mediumFont(design, "506 2ND AVE", 60, 428);

            this.mediumBoldFont(design, "SEATTLE WA 98104-2307", 60, 407);

            design.DrawLine(0, 405, 450, 405, 1);
            design.DrawLine(122, 404, 122, 290, 1);
            design.DrawLine(0, 284, 450, 284, 10);

            //CANADA: string[] maxicode = { "M2N0A9", "068", "001", "1Z123X560120754868" };
            string[] maxicode = { "98104", "2307", "840", "001", "1Z123X560120754868" };
            design.DrawMaxiCode(maxicode, LabelConst.CLS_RT_NORMAL, 10, 298);

            design.DrawBarCode("&D420981042307", LabelConst.CLS_BCS_CODE128, LabelConst.CLS_RT_NORMAL, 6, 3, 050, 180, 320, LabelConst.CLS_BCS_TEXT_HIDE);
            this.mediumFont(design, "(420) 98104-2307", 200, 295);

            this.smallFont(design, "(420) SHIP TO POSTAL CODE", 140, 385);

            design.DrawTextPCFont("UPS NEXT DAY AIR", "Arial", LabelConst.CLS_RT_NORMAL, 100, 100, 20, LabelConst.CLS_FNT_BOLD, 10, 245);
            this.mediumSmallFont(design, "TRACKING #: 1Z 123 X56 01 2075 4868", 10, 230);
            design.DrawTextPCFont("1", "Arial", LabelConst.CLS_RT_NORMAL, 100, 100, 48, LabelConst.CLS_FNT_BOLD, 310, 215);

            design.DrawLine(0, 225, 450, 225, 1);
            design.DrawBarCode("1Z123X560120754868", LabelConst.CLS_BCS_CODE128, LabelConst.CLS_RT_NORMAL, 6, 3, 100, 30, 105, LabelConst.CLS_BCS_TEXT_HIDE);
            design.DrawLine(0, 84, 450, 84, 10);

        }

        private void smallFont(LabelDesign design, String data, int x, int y)
        {
            design.DrawTextPtrFont(data,                                //string data
                                   LabelConst.CLS_LOCALE_OTHER,         //int locale
                                   LabelConst.CLS_PRT_FNT_TRIUMVIRATE,  //int font
                                   LabelConst.CLS_RT_NORMAL,            //int rotation
                                   1,                                   //int hexp
                                   1,                                   //int vexp
                                   LabelConst.CLS_PRT_FNT_SIZE_8,       //int size
                                   x,                                   //int x
                                   y);                                  //int y

        }


        private void mediumSmallFont(LabelDesign design, String data, int x, int y)
        {
            design.DrawTextPtrFont(data,                                //string data
                                   LabelConst.CLS_LOCALE_OTHER,         //int locale
                                   LabelConst.CLS_PRT_FNT_TRIUMVIRATE,  //int font
                                   LabelConst.CLS_RT_NORMAL,            //int rotation
                                   1,                                   //int hexp
                                   1,                                   //int vexp
                                   LabelConst.CLS_PRT_FNT_SIZE_10,      //int size
                                   x,                                   //int x
                                   y);                                  //int y
        }


        private void mediumFont(LabelDesign design, String data, int x, int y)
        {
            design.DrawTextPtrFont(data,                                //string data
                                   LabelConst.CLS_LOCALE_OTHER,         //int locale
                                   LabelConst.CLS_PRT_FNT_TRIUMVIRATE,  //int font
                                   LabelConst.CLS_RT_NORMAL,            //int rotation
                                   1,                                   //int hexp
                                   1,                                   //int vexp
                                   LabelConst.CLS_PRT_FNT_SIZE_12,      //int size
                                   x,                                   //int x
                                   y);                                  //int y
        }


        private void mediumBoldFont(LabelDesign design, String data, int x, int y)
        {
            design.DrawTextPtrFont(data,                                //string data
                                   LabelConst.CLS_LOCALE_OTHER,         //int locale
                                   LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B,//int font
                                   LabelConst.CLS_RT_NORMAL,            //int rotation
                                   1,                                   //int hexp
                                   1,                                   //int vexp
                                   LabelConst.CLS_PRT_FNT_SIZE_14,      //int size
                                   x,                                   //int x
                                   y);                                  //int y
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            LabelPrinter printer = new LabelPrinter();
            LabelDesign design = new LabelDesign();
            this.DesignLabel(design);
            printer.Preview(design, LabelConst.CLS_PRT_RES_203, LabelConst.CLS_UNIT_INCH, 450, 600);
            
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            int type = 0;
            int result = 0;

            LabelPrinter printer = new LabelPrinter();

            lstPrinters.Items.Clear();
            type = (int)connectType.SelectedValue;

            CitizenPrinterInfo[] info;

            info = printer.SearchCitizenPrinter(type, 10, out result);

        }
    }
}
