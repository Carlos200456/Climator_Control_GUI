using System.Net;
using System.Net.Sockets;
using System.Text;
namespace UDP
{
    // https://www.codeproject.com/Articles/96675/How-to-easily-exchange-data-among-NET-applications
    //

    public class SimpleUDP
    {
        UdpClient u;
        public SimpleUDP(int port)
        {
            u = new UdpClient(port, AddressFamily.InterNetwork);
        }

        public void Write(string s, string host, int p)
        {
            UdpClient u1 = new UdpClient();
            u1.Connect(host, p);
            u1.Send(ASCIIEncoding.UTF32.GetBytes(s), ASCIIEncoding.UTF32.GetByteCount(s));
            u1.Close(); // release all recources allocated by UdpClient
        }

        public string Read()
        {
            if (u.Available > 0)
            {
                IPEndPoint e = new IPEndPoint(IPAddress.Any, 0);
                byte[] b = u.Receive(ref e);
                return ASCIIEncoding.UTF32.GetString(b);
            }
            return "";
        }

        public void Close()
        {
            u.Close();
            u = null;
        }
    }
}
