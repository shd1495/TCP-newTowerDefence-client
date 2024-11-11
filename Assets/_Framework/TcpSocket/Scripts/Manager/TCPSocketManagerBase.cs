using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ironcow;
using System.Net.Sockets;
using System.Net;
using System;
using Ironcow.WebSocketPacket;
using Google.Protobuf;
using static GamePacket;

public abstract class TCPSocketManagerBase<T> : MonoSingleton<T> where T : TCPSocketManagerBase<T>
{
    public Dictionary<PayloadOneofCase, Action<GamePacket>> _onRecv = new Dictionary<PayloadOneofCase, Action<GamePacket>>();

    public Queue<Packet> sendQueue = new Queue<Packet>();
    public Queue<Packet> receiveQueue = new Queue<Packet>();

    public string ip = "192.168.0.2";
    public int port = 3000;

    public Socket socket;
    public string version = "1.0.0";
    public int sequenceNumber = 1;

    byte[] recvBuff = new byte[1024];

    protected abstract void InitPackets();

    public TCPSocketManagerBase<T> Init(string ip, int port)
    {
        this.ip = ip;
        this.port = port;
        InitPackets();
        return this;
    }

    public async void Connect()
    {
        IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
        if(!IPAddress.TryParse(ip, out IPAddress ipAddress))
        {
            ipAddress = ipHost.AddressList[0];
        }
        IPEndPoint endPoint = new IPEndPoint(ipAddress, port);
        Debug.Log("Tcp Ip : " + ipAddress.MapToIPv4().ToString() + ", Port : " + port);
        socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            await socket.ConnectAsync(endPoint);
            OnReceive();
            StartCoroutine(OnSendQueue());
            StartCoroutine(OnReceiveQueue());
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    private async void OnReceive()
    {
        if(socket != null)
        {
            while (socket.Connected)
            {
                try
                {
                    var recvByteLength = await socket.ReceiveAsync(recvBuff, SocketFlags.None);
                    if (recvByteLength > 0)
                    {
                        Packet packet = new Packet(recvBuff);
                        ReceivePacket(packet);
                    }
                }
                catch(Exception e)
                {
                    Debug.LogError(e.ToString());
                }
            }
        }/**/
    }

    public void ReceivePacket(Packet packet)
    {
        receiveQueue.Enqueue(packet);
    }

    public async void Send(GamePacket gamePacket)
    {
        if (socket == null) return;
        var byteArray = gamePacket.ToByteArray();
        MsgId msgId = (MsgId)gamePacket.PayloadCase;
        var packet = new Packet(gamePacket.PayloadCase, version, sequenceNumber++, byteArray);
        sendQueue.Enqueue(packet);
        //var result = await socket.SendAsync(packet.ToByteArray(), SocketFlags.None);
        
    }

    IEnumerator OnSendQueue()
    {
        while(true)
        {
            yield return new WaitUntil(() => sendQueue.Count > 0);
            yield return socket.SendAsync(sendQueue.Dequeue().ToByteArray(), SocketFlags.None);
        }
    }

    IEnumerator OnReceiveQueue()
    {
        while (true)
        {
            yield return new WaitUntil(() => receiveQueue.Count > 0);
            var packet = receiveQueue.Dequeue();
            _onRecv[packet.type].Invoke(packet.gamePacket);
        }
    }

    private void OnDestroy()
    {
        Disconnect();
    }

    public void Disconnect(bool isReconnect = false)
    {
        StopAllCoroutines();
        socket.Disconnect(isReconnect);
        if(isReconnect)
        {
            Connect();
        }
    }
}