using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RDJTPClient
{
    class Client
    {
        private const int portNumber = 5000;

        public void Start()
        {
            var client = GetRDJTPClient();
            var stream = client.GetStream();

            var msg = $"Hello from client id: {this.GetHashCode()}";
            var buffer = Encoding.UTF8.GetBytes(msg);

            stream.Write(buffer, 0, buffer.Length);

            stream.Close();
        }

        private TcpClient GetRDJTPClient()
        {
            var client = new TcpClient();
            client.Connect(IPAddress.Loopback, portNumber);

            return client;
        }
    }
}
