namespace MarcoPolo.Resolve {
	using System;
	using Mono.Zeroconf;

	public class WorkerBuilder {
		public virtual BrowseWorker Build(Service svcInfo, Action<Service> handler) {
			var browser = new ServiceBrowser();

			browser.ServiceAdded += (s, browseArgs) => {
				var service = browseArgs.Service;

				service.Resolved += (sender, resolveArgs) => {
					var resolved = resolveArgs.Service;
					var found = resolved.ToService();
					if (handler == null)
						return;

					handler.BeginInvoke(found, null, null);
				};

				service.Resolve();
			};

			return new BrowseWorker(browser, svcInfo);
		}
	}
}
