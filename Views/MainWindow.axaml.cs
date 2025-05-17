using Avalonia.Controls;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Threading;
namespace SetMinerStaticIpCK.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private bool _isListening = false;

    private async void OnStartListening(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_isListening)
        {
            StatusText.Text = "已经在监听中...";
            return;
        }

        _isListening = true;
        StatusText.Text = "正在监听 UDP 14235...";

        await Task.Run(() => StartUdpListener());
    }

    private void StartUdpListener()
    {
        using var udp = new UdpClient(14235);
        var endpoint = new IPEndPoint(IPAddress.Any, 0);

        try
        {
            while (_isListening)
            {
                var result = udp.Receive(ref endpoint);
                string msg = Encoding.UTF8.GetString(result);
                Console.WriteLine($"接收到来自 {endpoint}: {msg}");

                // 如果需要更新 UI，要用 Dispatcher
                Dispatcher.UIThread.Post(() => { StatusText.Text = $"收到来自 {endpoint}: {msg}"; });
            }
        }
        catch (Exception ex)
        {
            Dispatcher.UIThread.Post(() => { StatusText.Text = $"监听出错: {ex.Message}"; });
        }
    }
}