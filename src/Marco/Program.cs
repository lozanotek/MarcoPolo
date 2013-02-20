namespace MarcoPolo.Broadcast {
	using Topshelf;

	class Program {
		static void Main() {
            HostFactory.Run(x => {
                x.Service<BroadcastService>(s => {
                    s.ConstructUsing(name => new BroadcastService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                x.RunAsNetworkService();

                x.SetDescription("Simple broadacast service that uses MarcoPolo for configuring ZeroConf services.");
                x.SetDisplayName("MarcoPolo Broadcast Service");
                x.SetServiceName("MarcoPoloBroadcast");
                x.EnableShutdown();
            });
        }
	}
}
