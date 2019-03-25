using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Globalization;
using Microsoft.Speech.Recognition;


using com.citizen.sdk.LabelPrint;
using System.Diagnostics;

namespace PrintLabel
{
    public partial class Form1 : Form
    {
        private enum eErrorKind
        {
            eErrConnect = 1,
            eErrPrinterCheck,
            eErrPrint,
            eErrPrinterStatus
        };
        List<FilmInformation> filmInformationList;
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
            LoadData();
        }

        private void LoadData()
        {
            string url = "http://tickets.docudays.org.ua/v1/mobile_app/usher/get_screenings";

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response;

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();

            }

           filmInformationList = JsonConvert.DeserializeObject<List<FilmInformation>>(response);
            for (int i = 0; i < filmInformationList.Count(); i++)
            { 
                    dataGridView1.Rows.Add(filmInformationList[i].name, filmInformationList[i].description);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lstPrinters.Items.Clear();
            lstPrinters.Columns.Add("Port", 150, HorizontalAlignment.Left);
            lstPrinters.Columns.Add("", 184, HorizontalAlignment.Left);
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            String statusMessage = "";
            int errorCode = 0;
            int resultCode = LabelConst.CLS_SUCCESS;
            int type = (int)connectType.SelectedValue;

            if (lstPrinters.SelectedItems.Count <= 0)
            {
                MessageBox.Show("No printer is selected.", "Error");
                return;
            }

            LabelPrinter printer = new LabelPrinter();

           

            resultCode = printer.Connect(type, lstPrinters.Items[0].Text);
            printer.SetLog(1, "C:\\Users\\bukha\\Desktop\\Logs", 10);

            if (resultCode != LabelConst.CLS_SUCCESS)
            {
                this.PrinterErrorProc(printer, eErrorKind.eErrConnect, resultCode, null);
                return;
            }

            resultCode = printer.PrinterCheck();
            if (resultCode != LabelConst.CLS_SUCCESS)
            {
                this.PrinterErrorProc(printer, eErrorKind.eErrPrinterCheck, resultCode, null);
                printer.Disconnect();
                return;
            }

            statusMessage = "";

            this.CheckPrinterStatus(printer, ref errorCode, ref statusMessage);

            if (errorCode != 0)
            {
                this.PrinterErrorProc(printer, eErrorKind.eErrPrinterStatus, 0, statusMessage);
                printer.Disconnect();
                return;
            }

            printer.SetMeasurementUnit(LabelConst.CLS_UNIT_INCH);
            LabelDesign design = new LabelDesign();
            this.DesignLabel(design);

            resultCode = printer.Print(design, 3);

            if (resultCode != LabelConst.CLS_SUCCESS)
            {
                this.PrinterErrorProc(printer, eErrorKind.eErrPrint, resultCode, null);
                printer.Disconnect();
                return;
            }
            System.Threading.Thread.Sleep(500);

            while (true)
            {
                resultCode = printer.PrinterCheck();
                if (resultCode != LabelConst.CLS_SUCCESS)
                {
                    this.PrinterErrorProc(printer, eErrorKind.eErrPrinterCheck, resultCode, null);
                    printer.Disconnect();
                    return;
                }
                if (printer.GetPrinting() == 0 & printer.GetBatchProcessing() == 0)
                    break;

            }

            statusMessage = "";
            this.CheckPrinterStatus(printer, ref errorCode, ref statusMessage);
            if(errorCode!=0)
            {
                this.PrinterErrorProc(printer, eErrorKind.eErrPrinterStatus, 0, statusMessage);

            }
            else
            {
                MessageBox.Show("Success", "Print Result");
            }
        }


            private void DesignLabel(LabelDesign design)
            {

            design.DrawTextPtrFont("Sample Print",  LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_24, 20, 300); 

            design.DrawQRCode("DrawQRCode",  LabelConst.CLS_ENC_CDPG_IBM850, LabelConst.CLS_RT_NORMAL, 4, LabelConst.CLS_QRCODE_EC_LEVEL_H, 20, 220);

            design.FillRect(20, 150, 350, 40, LabelConst.CLS_SHADED_PTN_11);

            design.DrawBarCode("0123456789",  LabelConst.CLS_BCS_CODE128, LabelConst.CLS_RT_NORMAL,  3, 3, 30, 20, 70, LabelConst.CLS_BCS_TEXT_SHOW); 
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
                if (result != LabelConst.CLS_SUCCESS)
                {
                    if (result == LabelConst.CLS_E_NO_LIST)
                        MessageBox.Show("Printer Not Found", "Warning");
                    else
                        MessageBox.Show("Searching Failed", "Error");
                    return;
                }

                for (int i = 0; i < info.Length; i++)
                {
                    string[] CLS = new string[2];

                    CLS[0] = info[i].deviceName;
                    CLS[1] = info[i].printerModel;
                lstPrinters.Items.Add(new ListViewItem(CLS));

            }
            
            lstPrinters.Focus();
            lstPrinters.Items[0].Selected = true;
        }


            private void CheckPrinterStatus(LabelPrinter printer, ref int error, ref String message)
            {
                int iStatus = 0;


                {
                    message += "Printer Status\r\n";

                    iStatus = printer.GetCommandInterpreterInAction();
                    message += " " + iStatus.ToString() + ": Command interpreter in action" + "\r\n";
                    error += iStatus;

                    iStatus = printer.GetPaperError();
                    message += " " + iStatus.ToString() + ": Paper error" + "\r\n";
                    error += iStatus;

                    iStatus = printer.GetRibbonEnd();
                    message += " " + iStatus.ToString() + ": Ribbon end" + "\r\n";
                    error += iStatus;

                    iStatus = printer.GetBatchProcessing();
                    message += " " + iStatus.ToString() + ": Batch processing" + "\r\n";
                    error += iStatus;

                    iStatus = printer.GetPrinting();
                    message += " " + iStatus.ToString() + ": Printing" + "\r\n";
                    error += iStatus;

                    iStatus = printer.GetPause();
                    message += " " + iStatus.ToString() + ": Pause" + "\r\n";
                    error += iStatus;

                    iStatus = printer.GetWaitingForPeeling();
                    message += " " + iStatus.ToString() + ": Waiting for peeling" + "\r\n";
                    error += iStatus;
                }

            }


            private void PrinterErrorProc(LabelPrinter printer, eErrorKind eError, int iErrCode, String sMsg)
            {
                String sTmpMsg = "";

                if (eError == eErrorKind.eErrConnect)
                {
                    MessageBox.Show("Connect failure: " + iErrCode.ToString(), "Error");
                }
                else if (eError == eErrorKind.eErrPrinterCheck)
                {
                    MessageBox.Show("PrinterCheck failure: " + iErrCode.ToString(), "Error");
                }
                else if (eError == eErrorKind.eErrPrint)
                {
                    MessageBox.Show("Print failure: " + iErrCode.ToString(), "Error");
                }
                else if (eError == eErrorKind.eErrPrinterStatus)
                {
                    sTmpMsg = "The printer can not print.\r\n"  + sMsg;
                    MessageBox.Show(sTmpMsg, "Printer Error");

  
                }
                else
                {
                }
            }
        static Label l;

         void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            label2.Text = e.Result.Text;
            
                l.Text = e.Result.Text;
            dataGridView1.Rows.Clear();
                for(int i = 0; i < filmInformationList.Count(); i ++)
                {
                    if (e.Result.Text == filmInformationList[i].name)
                        dataGridView1.Rows.Add(filmInformationList[i].name, filmInformationList[i].description);
                }
           
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

            l = label1;
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ru-ru");
            SpeechRecognitionEngine sre = new SpeechRecognitionEngine(ci);
            sre.SetInputToDefaultAudioDevice();

            sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);


            Choices numbers = new Choices();
            for(int i = 0; i < filmInformationList.Count(); i++)
                numbers.Add(filmInformationList[i].name);
            numbers.Add("два");


            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = ci;
            gb.Append(numbers);


            Grammar g = new Grammar(gb);
            sre.LoadGrammar(g);

            sre.RecognizeAsync(RecognizeMode.Multiple);
        }
    }
}
