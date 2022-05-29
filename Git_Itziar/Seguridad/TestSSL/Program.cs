using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Authentication;

namespace TestSSl
{
    public class SecureTcpClient : IDisposable
    {
        X509CertificateCollection clientCertificates;
        RemoteCertificateValidationCallback certValidationCallback;
        SecureConnectionResultsCallback connectionCallback;
        bool checkCertificateRevocation;
        AsyncCallback onConnected;
        AsyncCallback onAuthenticateAsClient;
        TcpClient client;
        IPEndPoint remoteEndPoint;
        string remoteHostName;
        SslProtocols protocols;
        int disposed;
        public SecureTcpClient(SecureConnectionResultsCallback callback): this(callback, null, SslProtocols.None){}
        public SecureTcpClient(SecureConnectionResultsCallback callback,RemoteCertificateValidationCallback certValidationCallback): this(callback, certValidationCallback, SslProtocols.None) { }
        public SecureTcpClient(SecureConnectionResultsCallback callback,RemoteCertificateValidationCallback certValidationCallback,SslProtocols sslProtocols){
            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }
            onConnected = new AsyncCallback(OnConnected);
            onAuthenticateAsClient = new AsyncCallback(OnAuthenticateAsClient);
            this.certValidationCallback = certValidationCallback;
            this.connectionCallback = callback;
            protocols = sslProtocols;
            this.disposed = 0;
        }
        ~SecureTcpClient()
        {
            Dispose();
        }
        public bool CheckCertificateRevocation
        {
            get { return checkCertificateRevocation; }
            set { checkCertificateRevocation = value; }
        }
        public void StartConnecting(string remoteHostName, IPEndPoint remoteEndPoint)
        {
            StartConnecting(remoteHostName, remoteEndPoint, null);
        }
        public void StartConnecting(string remoteHostName, IPEndPoint remoteEndPoint,
            X509CertificateCollection clientCertificates)
        {
            if (string.IsNullOrEmpty(remoteHostName))
                throw new ArgumentException("Value cannot be null or empty", "remoteHostName");
            if (remoteEndPoint == null)
                throw new ArgumentNullException("remoteEndPoint");
            Console.WriteLine("Client connecting to: {0}", remoteEndPoint);
            this.clientCertificates = clientCertificates;
            this.remoteHostName = remoteHostName;
            this.remoteEndPoint = remoteEndPoint;
            if (client != null)
                client.Close();
            client = new TcpClient(remoteEndPoint.AddressFamily);
            client.BeginConnect(remoteEndPoint.Address,
                remoteEndPoint.Port,this.onConnected, null);
        }
        public void Close()
        {
            Dispose();
        }
        void OnConnected(IAsyncResult result)
        {
            SslStream sslStream = null;
            try
            {
                bool leaveStreamOpen = false;//close the socket when done
                if (this.certValidationCallback != null)
                    sslStream = new SslStream(client.GetStream(), leaveStreamOpen, this.certValidationCallback);
                else
                    sslStream = new SslStream(client.GetStream(), leaveStreamOpen);
                sslStream.BeginAuthenticateAsClient(this.remoteHostName,
                        this.clientCertificates,
                        this.protocols,
                        this.checkCertificateRevocation,
                        this.onAuthenticateAsClient,
                        sslStream);
            }
            catch (Exception ex)
            {
                if (sslStream != null)
                {
                    sslStream.Dispose();
                    sslStream = null;
                }
                this.connectionCallback(this, new SecureConnectionResults(ex));
            }
        }
        void OnAuthenticateAsClient(IAsyncResult result)
        {
            SslStream sslStream = null;
            try
            {
                sslStream = result.AsyncState as SslStream;
                sslStream.EndAuthenticateAsClient(result);
                this.connectionCallback(this, new SecureConnectionResults(sslStream));
            }
            catch (Exception ex)
            {
                if (sslStream != null)
                {
                    sslStream.Dispose();
                    sslStream = null;
                }
                this.connectionCallback(this, new SecureConnectionResults(ex));
            }
        }
        public void Dispose()
        {
            if (System.Threading.Interlocked.Increment(ref disposed) == 1)
            {
                if (client != null)
                {
                    client.Close();
                    client = null;
                }
                GC.SuppressFinalize(this);
            }
        }
    }
    public delegate void SecureConnectionResultsCallback(object sender, SecureConnectionResults args);
    public class SecureConnectionResults


    {


        private SslStream secureStream;


        private Exception asyncException;





        internal SecureConnectionResults(SslStream sslStream)


        {


            this.secureStream = sslStream;


        }





        internal SecureConnectionResults(Exception exception)


        {


            this.asyncException = exception;


        }





        public Exception AsyncException { get { return asyncException; } }


        public SslStream SecureStream { get { return secureStream; } }


    }
    public class SecureTcpServer : IDisposable


    {


        X509Certificate serverCert;


        RemoteCertificateValidationCallback certValidationCallback;


        SecureConnectionResultsCallback connectionCallback;


        AsyncCallback onAcceptConnection;


        AsyncCallback onAuthenticateAsServer;





        bool started;





        int listenPort;


        TcpListener listenerV4;


        TcpListener listenerV6;


        int disposed;


        bool clientCertificateRequired;


        bool checkCertifcateRevocation;


        SslProtocols sslProtocols;





        public SecureTcpServer(int listenPort, X509Certificate serverCertificate,


            SecureConnectionResultsCallback callback)


            : this(listenPort, serverCertificate, callback, null)


        {


        }





        public SecureTcpServer(int listenPort, X509Certificate serverCertificate,


            SecureConnectionResultsCallback callback,


            RemoteCertificateValidationCallback certValidationCallback)


        {


            if (listenPort < 0 || listenPort > UInt16.MaxValue)


                throw new ArgumentOutOfRangeException("listenPort");





            if (serverCertificate == null)


                throw new ArgumentNullException("serverCertificate");





            if (callback == null)


                throw new ArgumentNullException("callback");





            onAcceptConnection = new AsyncCallback(OnAcceptConnection);


            onAuthenticateAsServer = new AsyncCallback(OnAuthenticateAsServer);





            this.serverCert = serverCertificate;


            this.certValidationCallback = certValidationCallback;


            this.connectionCallback = callback;


            this.listenPort = listenPort;


            this.disposed = 0;


            this.checkCertifcateRevocation = false;


            this.clientCertificateRequired = false;


            this.sslProtocols = SslProtocols.None;


        }





        ~SecureTcpServer()


        {


            Dispose();


        }





        public SslProtocols SslProtocols


        {


            get { return sslProtocols; }


            set { sslProtocols = value; }


        }





        public bool CheckCertifcateRevocation


        {


            get { return checkCertifcateRevocation; }


            set { checkCertifcateRevocation = value; }


        }








        public bool ClientCertificateRequired


        {


            get { return clientCertificateRequired; }


            set { clientCertificateRequired = value; }


        }





        public void StartListening()


        {


            if (started)


                throw new InvalidOperationException("Already started...");





            IPEndPoint localIP;


            if (Socket.OSSupportsIPv4 && listenerV4 == null)


            {


                localIP = new IPEndPoint(IPAddress.Any, listenPort);


                Console.WriteLine("SecureTcpServer: Started listening on {0}", localIP);


                listenerV4 = new TcpListener(localIP);


            }





            if (Socket.OSSupportsIPv6 && listenerV6 == null)


            {


                localIP = new IPEndPoint(IPAddress.IPv6Any, listenPort);


                Console.WriteLine("SecureTcpServer: Started listening on {0}", localIP);


                listenerV6 = new TcpListener(localIP);


            }





            if (listenerV4 != null)


            {


                listenerV4.Start();


                listenerV4.BeginAcceptTcpClient(onAcceptConnection, listenerV4);


            }





            if (listenerV6 != null)


            {


                listenerV6.Start();


                listenerV6.BeginAcceptTcpClient(onAcceptConnection, listenerV6);


            }





            started = true;


        }





        public void StopListening()


        {


            if (!started)


                return;





            started = false;





            if (listenerV4 != null)


                listenerV4.Stop();


            if (listenerV6 != null)


                listenerV6.Stop();


        }





        void OnAcceptConnection(IAsyncResult result)


        {


            TcpListener listener = result.AsyncState as TcpListener;


            TcpClient client = null;


            SslStream sslStream = null;





            try


            {


                if (this.started)


                {


                    //start accepting the next connection...


                    listener.BeginAcceptTcpClient(this.onAcceptConnection, listener);


                }


                else


                {


                    //someone called Stop() - don't call EndAcceptTcpClient because


                    //it will throw an ObjectDisposedException


                    return;


                }





                //complete the last operation...


                client = listener.EndAcceptTcpClient(result);








                bool leaveStreamOpen = false;//close the socket when done





                if (this.certValidationCallback != null)


                    sslStream = new SslStream(client.GetStream(), leaveStreamOpen, this.certValidationCallback);


                else


                    sslStream = new SslStream(client.GetStream(), leaveStreamOpen);





                sslStream.BeginAuthenticateAsServer(this.serverCert,


                    this.clientCertificateRequired,


                    this.sslProtocols,


                    this.checkCertifcateRevocation,//checkCertifcateRevocation


                    this.onAuthenticateAsServer,


                    sslStream);








            }


            catch (Exception ex)


            {


                if (sslStream != null)


                {


                    sslStream.Dispose();


                    sslStream = null;


                }


                this.connectionCallback(this, new SecureConnectionResults(ex));


            }


        }





        void OnAuthenticateAsServer(IAsyncResult result)


        {


            SslStream sslStream = null;


            try


            {


                sslStream = result.AsyncState as SslStream;


                sslStream.EndAuthenticateAsServer(result);





                this.connectionCallback(this, new SecureConnectionResults(sslStream));


            }


            catch (Exception ex)


            {


                if (sslStream != null)


                {


                    sslStream.Dispose();


                    sslStream = null;


                }





                this.connectionCallback(this, new SecureConnectionResults(ex));


            }


        }





        public void Dispose()


        {


            if (System.Threading.Interlocked.Increment(ref disposed) == 1)


            {


                if (this.listenerV4 != null)


                    listenerV4.Stop();


                if (this.listenerV6 != null)


                    listenerV6.Stop();





                listenerV4 = null;


                listenerV6 = null;





                GC.SuppressFinalize(this);


            }


        }


    }
    class Program


    {


        static void Main(string[] args)


        {


            SecureTcpServer server = null;


            SecureTcpClient client = null;





            try


            {


                int port = 8889;





                RemoteCertificateValidationCallback certValidationCallback = null;


                certValidationCallback = new RemoteCertificateValidationCallback(IgnoreCertificateErrorsCallback);





                string certPath = System.Reflection.Assembly.GetEntryAssembly().Location;


                certPath = Path.GetDirectoryName(certPath);


                certPath = Path.Combine(certPath, "localhost.cer");


                Console.WriteLine("Loading Server Cert From: " + certPath);


                X509Certificate serverCert = X509Certificate.CreateFromCertFile(certPath);





                server = new SecureTcpServer(port, serverCert,


                    new SecureConnectionResultsCallback(OnServerConnectionAvailable));





                server.StartListening();





                client = new SecureTcpClient(new SecureConnectionResultsCallback(OnClientConnectionAvailable),


                    certValidationCallback);





                client.StartConnecting("localhost", new IPEndPoint(IPAddress.Loopback, port));


            }


            catch (Exception ex)


            {


                Console.WriteLine(ex);


            }





            //sleep to avoid printing this text until after the callbacks have been invoked.


            Thread.Sleep(4000);


            Console.WriteLine("Press any key to continue...");


            Console.ReadKey();





            if (server != null)


                server.Dispose();


            if (client != null)


                client.Dispose();





        }





        static void OnServerConnectionAvailable(object sender, SecureConnectionResults args)


        {


            if (args.AsyncException != null)


            {


                Console.WriteLine(args.AsyncException);


                return;


            }





            SslStream stream = args.SecureStream;





            Console.WriteLine("Server Connection secured: " + stream.IsAuthenticated);











            StreamWriter writer = new StreamWriter(stream);


            writer.AutoFlush = true;





            writer.WriteLine("Hello from server!");





            StreamReader reader = new StreamReader(stream);


            string line = reader.ReadLine();


            Console.WriteLine("Server Recieved: '{0}'", line == null ? "<NULL>" : line);





            writer.Close();


            reader.Close();


            stream.Close();


        }





        static void OnClientConnectionAvailable(object sender, SecureConnectionResults args)


        {


            if (args.AsyncException != null)


            {


                Console.WriteLine(args.AsyncException);


                return;


            }


            SslStream stream = args.SecureStream;





            Console.WriteLine("Client Connection secured: " + stream.IsAuthenticated);





            StreamWriter writer = new StreamWriter(stream);


            writer.AutoFlush = true;





            writer.WriteLine("Hello from client!");





            StreamReader reader = new StreamReader(stream);


            string line = reader.ReadLine();


            Console.WriteLine("Client Recieved: '{0}'", line == null ? "<NULL>" : line);





            writer.Close();


            reader.Close();


            stream.Close();


        }





        static bool IgnoreCertificateErrorsCallback(object sender,


            X509Certificate certificate,


            X509Chain chain,


            SslPolicyErrors sslPolicyErrors)


        {


            if (sslPolicyErrors != SslPolicyErrors.None)


            {





                Console.WriteLine("IgnoreCertificateErrorsCallback: {0}", sslPolicyErrors);


                //you should implement different logic here...





                if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateChainErrors) != 0)


                {


                    foreach (X509ChainStatus chainStatus in chain.ChainStatus)


                    {


                        Console.WriteLine("\t" + chainStatus.Status);


                    }


                }


            }





            //returning true tells the SslStream object you don't care about any errors.


            return true;


        }


    }
}