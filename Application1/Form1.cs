﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Application1
{
    public partial class Form1 : Form
    {
        MqttClient mqttClient;

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
                mqttClient.Subscribe(new string[] { "Application2/Message" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                mqttClient.Connect("username", "userid", "password");
            });
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Message);
            listBox1.Invoke((MethodInvoker)(() => listBox1.Items.Add(message)));
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Task.Run(() =>
            {

                if (mqttClient != null && mqttClient.IsConnected)
                {
                    for (int i = 0; i < 1000; i++)
                    {



                        mqttClient.Publish("Application1/Message", Encoding.UTF8.GetBytes(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt").ToString()));
                    }
                }


            });

        }
    }
}
