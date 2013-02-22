namespace MarcoPolo.Resolve {
	using System.Threading;
	using Mono.Zeroconf;

	public class BrowseWorker {
		public ServiceBrowser Browser { get; set; }
		public Service Service { get; set; }

		public BrowseWorker(ServiceBrowser browser, Service service) {
			Browser = browser;
			Service = service;
		}

		public void Browse() {
			var task = new Thread(StartBrowse);
			task.Start();
		}

		protected virtual void StartBrowse() {
			Browser.Browse(Service.Type, Service.Domain);
		}
	}
}
