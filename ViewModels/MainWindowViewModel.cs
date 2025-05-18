using SetMinerStaticIpCK.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Reactive;
using System.Threading;
using Avalonia.Threading;

namespace SetMinerStaticIpCK.ViewModels;

using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Person> People { get; }
    public ObservableCollection<MinerIpConfig> MinerIpList { get; set; }
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    public ICommand StartUdpCommand { get; }

    public MainWindowViewModel()
    {
        var people = new List<Person>
        {
            new Person("Neil", "Armstrong"),
            new Person("Buzz", "Li year"),
            new Person("James", "Kirk")
        };
        People = new ObservableCollection<Person>(people);
        var minerIpList = new List<MinerIpConfig>
        {
            new MinerIpConfig(1, @"10.11.10.1", "mac", @"10.11.1.1", "wait", "S19")
        };
        MinerIpList = new ObservableCollection<MinerIpConfig>(minerIpList);
        SaveCommand = ReactiveCommand.Create(SaveData);
        StartUdpCommand = ReactiveCommand.Create(StartUdpListener);
    }

    private string _description = string.Empty;

    public string Description
    {
        get => _description;
        set => this.RaiseAndSetIfChanged(ref _description, value);
    }

    public void ButtonAction()
    {
        MinerIpList[0].SetStatus = "来自直接调用正在更新中";

        DoWorkInBackground();
    }

    public async void DoWorkInBackground()
    {
        await Task.Run(() =>
        {
            // 假设是一个长时间操作
            Thread.Sleep(2000);

            // 切换到UI线程来更新绑定属性
            Dispatcher.UIThread.Post(() =>
            {
                Description = "后台任务完成，来自UI线程的问候";
                MinerIpList[0].SetStatus = "后台任务完成，正在更新中";
                MinerIpList.Add(new MinerIpConfig(
                    2,
                    "10.11.10.2",
                    "aa:bb:cc:dd:ee:ff",
                    "10.11.1.2",
                    "running",
                    "S21"
                ));
            });
        });
    }

    private void SaveData()
    {
        foreach (var miner in MinerIpList)
        {
            Console.WriteLine($"CurrentIp: {miner.CurrentIp}, LineNo: {miner.LineNo}, Status: {miner.SetStatus}");
        }

        // 你也可以在这里写入文件，例如 CSV、JSON 等。
    }

    private void StartUdpListener()
    {
        Task.Run(() =>
        {
            using var udpClient = new UdpClient(14235);
            var remoteEP = new IPEndPoint(IPAddress.Any, 0);

            while (true)
            {
                try
                {
                    var data = udpClient.Receive(ref remoteEP);
                    var message = Encoding.UTF8.GetString(data);

                    Console.WriteLine($"收到UDP数据：{message} 来自：{remoteEP}");
                    // 可在此调用 Dispatcher/UI 更新界面内容
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"UDP监听异常: {ex.Message}");
                    break;
                }
            }
        });
    }
}