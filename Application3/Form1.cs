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

namespace Application3
{
    public partial class Form1 : Form
    {
        MqttClient mqttClient;
        int counter = 0;
        DateTime startfull = DateTime.Now;
        DateTime endfull = DateTime.Now;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                mqttClient = new MqttClient("ip");
                mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
                mqttClient.Subscribe(new string[] { "Application1/Message" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                mqttClient.Connect("username", "userid", "password");

            });
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {



            counter++; 
            var message = Encoding.UTF8.GetString(e.Message);
            //DateTime end = DateTime.Now; 
            //var start = DateTime.ParseExact(message, "MM/dd/yyyy hh:mm:ss.fff tt",
            //                           System.Globalization.CultureInfo.InvariantCulture);
            //var def = (end - start).TotalMilliseconds;
            listBox1.Invoke((MethodInvoker)(() => listBox1.Items.Add(counter.ToString() + "\t"  + message)));
            //if (counter % 10000 == 1)
            //{
            //    startfull = DateTime.Now;
            //}
            //if (counter % 1000 == 0)
            //{
                
            //    endfull = DateTime.Now;
            //    Debug.WriteLine((endfull - startfull).TotalMilliseconds);
        
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
