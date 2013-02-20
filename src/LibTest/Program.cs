namespace LibTest {
	using System;
	using MarcoPolo;

	class Program {
		static void Main() {
			var finder = new ServiceFinder();
			ShowFiles(finder);
			BuildServices(finder);

			Console.Write("Press any key to exit...");
			Console.ReadKey(true);
		}

		private static void BuildServices(ServiceFinder finder) {
			var services = finder.GetServices();
			foreach (var service in services) {
				Console.WriteLine("Found Service: {0}", service.Name);
				Console.WriteLine("Will listen on port: {0}", service.Port);

				if (service.Records == null || service.Records.Count == 0) continue;

				Console.WriteLine("Text Records");
				foreach (var txtRecord in service.Records) {
					Console.WriteLine("{0}-{1}", txtRecord.Name, txtRecord.Value);					
				}
			}
		}

		private static void ShowFiles(ServiceFinder finder) {
			var files = finder.GetServiceFiles();

			foreach (var file in files) {
				Console.WriteLine("Found file '{0}'", file);
			}
		}
	}
}
