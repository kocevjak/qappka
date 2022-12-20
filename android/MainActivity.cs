using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Java.Util;
using Android.Bluetooth;    //obsluha bluetooth
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Android.Views;
using System.Collections.Generic;



namespace hsp_09
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        int mod = 0;                        //uložení v kterém modu se právě nacházím 0=uvod, 1=kapka, 2=časosber, 3=dálková spoušt

        Button start;               //tlačítko start
        //layout tlacitka
        Button kapka;
        Button casosber;
        Button dalkoaSpoust;

        CheckBox rezimBULB;
        //layout
        LinearLayout kapkaLayout;
        LinearLayout uvodLayout;
        LinearLayout casosberLayout;
        LinearLayout dalkovaSpoustLayout;
        //kapka
        EditText pocetKapek;
        EditText meziKapkami;
        EditText predBleskem;
        EditText velikostKapky;
        //casosber
        EditText pocetFotek;
        EditText Interval;

        //bluetooth
        private BluetoothAdapter mBluetoothAdapter = BluetoothAdapter.DefaultAdapter;
        private BluetoothSocket btSocket = null;
        private Stream outStream = null;
        private Stream inStream = null;
        private static string address = "30:83:98:45:C7:F6";
        BluetoothDevice Device;
        private static UUID MY_UUID = UUID.FromString("00001101-0000-1000-8000-00805F9B34FB");
        Button bConnect;
        TextView bStav;

        List<byte> DataToSend = new List<byte>();


        protected override void OnCreate(Bundle savedInstanceState)         //funkce která se volá při zapnutí aplikace
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            
            start = FindViewById<Button>(Resource.Id.start);
            //layout tlacitka
            kapka = FindViewById<Button>(Resource.Id.kapkaButton);
            casosber = FindViewById<Button>(Resource.Id.casosberButton);
            dalkoaSpoust = FindViewById<Button>(Resource.Id.dalkova_spoustButton);

            //rezimBULB = FindViewById<CheckBox>(Resource.Id.dalkovaSpoust_layou)
            //layout
            kapkaLayout = FindViewById<LinearLayout>(Resource.Id.kapka_layout);                     //kapka layout
            uvodLayout = FindViewById<LinearLayout>(Resource.Id.uvod_layout);                       //uvodní layout
            casosberLayout = FindViewById<LinearLayout>(Resource.Id.casosber_layout);               //časosber layout
            dalkovaSpoustLayout = FindViewById<LinearLayout>(Resource.Id.dalkovaSpoust_layout);     //dálková spoušt layout
            //kapka
            pocetKapek = FindViewById<EditText>(Resource.Id.setPocetKapek);
            meziKapkami = FindViewById<EditText>(Resource.Id.setMeziKapkami);
            predBleskem = FindViewById<EditText>(Resource.Id.setPredBleskem);
            velikostKapky = FindViewById<EditText>(Resource.Id.setVelikostKapky);
            //casosber
            pocetFotek = FindViewById<EditText>(Resource.Id.setPocetFotek);
            Interval = FindViewById<EditText>(Resource.Id.setMeziFotkami);
            //bluetooth
            bConnect = FindViewById<Button>(Resource.Id.bConnect);
            bStav = FindViewById<TextView>(Resource.Id.bStav);
            //evant
            kapka.Click += Kapka_Click;
            casosber.Click += Casosber_Click;
            dalkoaSpoust.Click += DalkoaSpoust_Click;
            bConnect.Click += BConnect_Click;
            start.Click += Start_Click;


            mBluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            turnOnBt();
        }

       
        private void Start_Click(object sender, EventArgs e)    //funkce když se klikne na tlačítko start
        {
            //dataToSend = new Java.Lang.String(mod.ToString() + "/" + pocetKapek.Text + "/" + meziKapkami.Text + "/" + predBleskem.Text + "/" + velikostKapky.Text);

            //dataToSend = new Java.Lang.String(MakeString());
            MakedataToSend();
            sendData(DataToSend.ToArray());
        }

        private void BConnect_Click(object sender, EventArgs e)     //funkce která se spustí při kliknutí na tlačítko připojit a připojí se k dálkové spoušti
        {
            bStav.Text = "připojování...";
            //bStav.SetTextColor(GetColorStateList(Resource.Color.pripojovani));

            if (mBluetoothAdapter.IsEnabled)
            {
                
                Device = mBluetoothAdapter.GetRemoteDevice(address);
                mBluetoothAdapter.CancelDiscovery();
                try
                {
                    
                    btSocket = Device.CreateInsecureRfcommSocketToServiceRecord(MY_UUID);
                    btSocket.Connect();
                    bStav.Text = "připojeno";
                    bStav.SetTextColor(GetColorStateList(Resource.Color.pripojeno));

                }
                catch(System.Exception)
                {
                    btSocket.Close();
                    bStav.Text = "nelze se připojit";
                    bStav.SetTextColor(GetColorStateList(Resource.Color.nepripojeno));
                }
            }
            else
            {
                turnOnBt();
            }
        }

        private void Kapka_Click(object sender, EventArgs e)    //funkce která zobrazí mod kapka
        {
            uvodLayout.Visibility = ViewStates.Gone;
            casosberLayout.Visibility = ViewStates.Gone;
            dalkovaSpoustLayout.Visibility = ViewStates.Gone;
            kapkaLayout.Visibility = ViewStates.Visible;
            mod = 1;
        }

        private void DalkoaSpoust_Click(object sender, EventArgs e)     //funkce která zobrazí mod dálková spoušt
        {
            uvodLayout.Visibility = ViewStates.Gone;
            casosberLayout.Visibility = ViewStates.Gone;
            dalkovaSpoustLayout.Visibility = ViewStates.Visible;
            kapkaLayout.Visibility = ViewStates.Gone;
            mod = 3;
        }

        private void Casosber_Click(object sender, EventArgs e)     //funkce která zobrazí mod časosběr
        {
            uvodLayout.Visibility = ViewStates.Gone;
            casosberLayout.Visibility = ViewStates.Visible;
            dalkovaSpoustLayout.Visibility = ViewStates.Gone;
            kapkaLayout.Visibility = ViewStates.Gone;
            mod = 2;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void turnOnBt()     //zapnutí bluetooth
        {

            if (!mBluetoothAdapter.Enable())
            {
                Toast.MakeText(this, "bluetooth není zapnutý", ToastLength.Short).Show();
            }
            if (mBluetoothAdapter == null)
            {
                Toast.MakeText(this, "toto zařízení nepodporuje bluetooth", ToastLength.Short).Show();
                bStav.Text = "Error";
            }
        }

        private void sendData(byte[] data)  //pošle data přes bluetooth
        {
            try
            {
                outStream = btSocket.OutputStream;
            }
            catch (Exception)
            {

                Toast.MakeText(this, "data se nepodařilo odeslat", ToastLength.Short).Show();
            }
            byte[] msgBuffer = data;
            try
            {
                outStream.Write(msgBuffer, 0, msgBuffer.Length);
                Toast.MakeText(this, "data odeslána", ToastLength.Short).Show();
            }
            catch (Exception)
            {
                Toast.MakeText(this, "data se nepodařilo odeslat", ToastLength.Short).Show();
            }
        }

        private void MakedataToSend()   //připraví data k poslání
        {
            //hlavicka
            //kapka = 1
            //dalková spoust = 2
            //časosber = 3

            DataToSend.Clear();

           

            if(mod == 1)
            {
                DataToSend.Add(1);
                DataKapka();
               //celkem posílám 7 bajtů
            }
            if(mod == 2)
            {
                DataToSend.Add(2);
                DataCasosber();
                //celkem posílám 4 bajty
            }
            if(mod == 3)
            {
                DataToSend.Add(3);
                //posílám pouze hlavičku
            }

            
        }

        private void DataKapka()    //formátování dat pro kapku
        {
            uint pocet_kapek;
            uint.TryParse(pocetKapek.Text,out pocet_kapek);
            uint velikost_kapka;        
            uint.TryParse(velikostKapky.Text, out velikost_kapka);
            uint mezi_kapka;
            uint.TryParse(meziKapkami.Text, out mezi_kapka);
            int pred_bleskem;
            int.TryParse(predBleskem.Text, out pred_bleskem);
            if(pocet_kapek > 10)
            {
                pocet_kapek = 10;
            }
            DataToSend.Add(((byte)pocet_kapek));

            if(velikost_kapka > 10)
            {
                velikost_kapka = 10;
            }
            DataToSend.Add(((byte)velikost_kapka));

            if(mezi_kapka > 9999)
            {
                mezi_kapka = 9999;
            }
            DataToSend.Add(((byte)((mezi_kapka >> 8) & 0xff)));
            DataToSend.Add(((byte)(mezi_kapka & 0xff)));
            
            if(pred_bleskem > 9999)
            {
                pred_bleskem = 9999;
            }
            DataToSend.Add(((byte)((pred_bleskem >> 8) & 0xff)));
            DataToSend.Add(((byte)(pred_bleskem & 0xff)));

        }
        private void DataCasosber() //formátování dat pro kapku
        {
            uint pocet_fotek;
            uint interval;
            uint.TryParse(pocetFotek.Text, out pocet_fotek);
            uint.TryParse(Interval.Text, out interval);

            if (pocet_fotek > 9999)
            {
                pocet_fotek = 9999;
            }
            DataToSend.Add(((byte)((pocet_fotek >> 8) & 0xff)));
            DataToSend.Add(((byte)(pocet_fotek & 0xff)));

            if(interval > 120)
            {
                interval = 120;
            }
            DataToSend.Add((byte)(interval));
        }
	}
}
