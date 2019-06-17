using EDDMobile.Comms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EDDMobileImpl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatusPage : ContentPage
    {
        

        WebSocketWrapper socket;
        public StatusPage()
        {
            InitializeComponent();

           
            lblJson.Text = "Click Connect...";
            socket = new WebSocketWrapper();
            socket.OnMessage += Instance_OnMessage;

        }

  

        private void Instance_OnMessage()
        {

            if (socket.TryGetMessage(out string msg))
                lblJson.Text = msg;

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
           
            await socket.Connect("ws://192.168.0.32/eddmobile");
            await socket.Send("refresh");
            await socket.Listen();
        }
    }
}