namespace PoloTest {
	using System;
	using System.Diagnostics;
	using MarcoPolo.Resolve;
	class Program {
		static void Main() {
			var registry = new PoloRegistry();

			registry
				.LookFor("_rdp._tcp", svc => {
					Console.WriteLine();
					Console.WriteLine("Found RDP Machine: {0}", svc.Name);
				})
				.LookFor("_http._tcp", svc => {
					var uri = string.Format("http://{0}", svc.Host.Address);

					Console.WriteLine();
					Console.WriteLine("Opening URL: {0}", uri);
					Process.Start(uri);
				});

			new Marco().Look(registry);

			Console.Write("Press any key to exit...");
			Console.Read();
		}
	}
}
