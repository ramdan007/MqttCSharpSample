using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Application2
{

    public partial class Form1 : Form
    {
        MqttClient mqttClient;
        int counter = 0;
        DateTime start = DateTime.Now;
        DateTime end = DateTime.Now;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                mqttClient = new MqttClient("159.89.30.124");
                mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
                mqttClient.Subscribe(new string[] { "TRM1BC330_TRS54FBF0/MD" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                mqttClient.Connect("BerkanIswhore", "BerkanIswhore", "Berkan@01");
            });
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {


            var message = Encoding.UTF8.GetString(e.Message);
            //if (counter == 0)
            //{
            //    start = DateTime.Now;
            //}

            counter++;
            //listBox1.Invoke((MethodInvoker)(() => listBox1.Items.Add(counter.ToString() + "   :   " + message)));
            Debug.WriteLine(counter.ToString());
            //if (counter >= 10000)
            //{
            //    end = DateTime.Now;
            //    Debug.WriteLine((end - start).TotalMilliseconds);
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                if (mqttClient != null && mqttClient.IsConnected)
                {
                    mqttClient.Publish("Application2/Message", Encoding.UTF8.GetBytes(textBox1.Text));

                }
            });
        }
    }
}
