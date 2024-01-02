using Microsoft.AspNetCore.SignalR.Client;

namespace SignalR
{
    public partial class MainPage : ContentPage
    {
        private readonly HubConnection _connection;
        public MainPage()
        {
            InitializeComponent();
            comments.Text = string.Empty;
           // string baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2" : "https://localhost";
            _connection = new HubConnectionBuilder().WithUrl("https://test.cflowapps.com/signalrtest/commenthub").Build();
            _connection.On<string, string>("ReceivedComment", (name, comment) =>
            {
                Dispatcher.Dispatch(() =>
                {
                    comments.Text += $"{name} said {comment}{Environment.NewLine}";
                });
            });
        }

        protected async override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
            await _connection.StartAsync();
        }
        private async void SendBtnClicked(object sender, EventArgs e)
        {
            await _connection.SendAsync("SendMessage", name.Text, comment.Text);
            comment.Text = string.Empty;
        }
    }
}


