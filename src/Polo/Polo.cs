namespace MarcoPolo.Resolve {
	using System;
	using System.Collections.Generic;
	using Mono.Zeroconf;

	public class Polo {
		private static IList<ServiceBrowser> browsers;

		public Polo() {
			browsers = new List<ServiceBrowser>();
		}

		public void Look(ServiceRegistry registry) {
			var table = registry.Services;

			foreach (var reg in table) {
				var svc = reg.Key;
				var handler = reg.Value;
				var browser = CreateBrowser(svc, handler);
				browsers.Add(browser);

				var action = new BrowseAction(browser, svc);
				action.Browse();
			}
		}

		protected virtual ServiceBrowser CreateBrowser(Service info, Action<Service> handler ) {
			var browser = new ServiceBrowser();

			browser.ServiceAdded += (s, browseArgs) => {
				var service = browseArgs.Service;

				service.Resolved += (sender, resolveArgs) => {
					var resolved = resolveArgs.Service;
					var found = resolved.ToService();
					if (handler == null) return;

					handler.BeginInvoke(found, null, null);
				};

				service.Resolve();
			};

			return browser;
		}
	}
}
