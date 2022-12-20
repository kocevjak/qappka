using System;
using System.IO.Ports;


namespace qappka_1._0_win
{


    public partial class qappka : Form
    {
        SerialPort serialPort = new SerialPort();
        List<byte> dataToSend = new List<byte>();


        public qappka()
        {
            InitializeComponent();
            serialPort.BaudRate = 115200;
            Scanport();
            connect.BackColor = Color.Red;
        }

        public void Scanport()  //zjištování dostupných seriových portů
        {
            COMPort.Items.Clear();
            COMPort.Items.AddRange(SerialPort.GetPortNames());
        }

        private void COMPort_Click(object sender, EventArgs e)  //funkce která spustí skenování seriových portů
        {
            Scanport();
        }

        public void OpenPort()  //připojení k seriovému portu
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                }

                serialPort.Write(dataToSend.ToArray(), 0, dataToSend.Count);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nepodařilo se připojit, zkuste překontrolovat COM Port", "COM Port", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void sendData()  //pošle data do dálkové spouště
        {
            if (serialPort.IsOpen)
            {
                try
                {
                    serialPort.Write(dataToSend.ToArray(), 0, dataToSend.Count);
                }
                catch
                {
                    MessageBox.Show("neco se nepovedlo", "Jejda", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                MessageBox.Show("Nejdříve se musíte připojit", "COM Port", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void vyfot_dalkova_spoust_Click(object sender, EventArgs e) //funkce na kliknutí tlačítka start
        {
            DataDalkovaSpoust();
            sendData();
        }

        public void DataDalkovaSpoust() //formát dat pro poslání mod dálková spoušť
        {
            
            dataToSend.Clear();
            dataToSend.Add(3);
        }

        private void connect_Click(object sender, EventArgs e)  //připojení k seriovému portu
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
                connect.BackColor = Color.Red;
            }
            else
            {
                if (COMPort.SelectedIndex >= 0)
                {
                    serialPort.PortName = COMPort.SelectedItem.ToString();
                    OpenPort();
                    connect.BackColor = Color.Green;

                }
                else
                {
                    MessageBox.Show("Prosím nejdříve vyberte COM port", "COM Port", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            
        }

        private void button1_Click(object sender, EventArgs e)  //odešle hodnoty modu kapka
        {
            DataKapka();
            sendData();
        }

        public void DataKapka()
        {
            int meziKapkami = (int)count_meziKapkami.Value;
            int predBleskem = (int)count_predBleskem.Value;

            dataToSend.Clear();
            dataToSend.Add(1);
            dataToSend.Add((byte)count_pocetKapek.Value);
            dataToSend.Add((byte)count_velikostKapek.Value);
            dataToSend.Add((byte)((meziKapkami >> 8) & 0xFF));
            dataToSend.Add((byte)(meziKapkami & 0xFF));
            dataToSend.Add((byte)((predBleskem >> 8) & (0xFF)));
            dataToSend.Add((byte)(predBleskem & 0xFF));
        }

        private void start_casosober_Click(object sender, EventArgs e)      //odešle hodnoty pro mod časosber
        {
            DataCasosber();
            sendData();
        }

        public void DataCasosber()  //format dat pro časosběr
        {
            int pocetFotek = (int)count_pocetFotek.Value;

            dataToSend.Clear();
            dataToSend.Add(2);
            dataToSend.Add((byte)((pocetFotek >> 8) & 0xFF));
            dataToSend.Add((byte)(pocetFotek & 0xFF));
            dataToSend.Add((byte)count_mezifotkami.Value);

        }

        private void button2_Click(object sender, EventArgs e)  //vypočet času
        {
            int vyska_cm = (int)vyska.Value;
            double vyska_m = vyska_cm * 0.01;
            double g = 9.8;
            double podil = (2 * vyska_m) / g;
            double cas = Math.Pow(podil, 0.5);
            int cas_ms = (int)(cas * 1000);
            cas_kalkulacka.Text = cas_ms.ToString();
        }
    }
}