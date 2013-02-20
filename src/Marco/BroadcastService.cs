namespace MarcoPolo.Broadcast {
	using System;
	using System.Collections.Generic;
	using Mono.Zeroconf;

	public class BroadcastService {
		private IList<RegisterService> Services { get; set; }
		private ServiceFinder Finder { get; set; }

		public BroadcastService() {
			Services = new List<RegisterService>();
			Finder = new ServiceFinder();
		}

		public virtual void Start() {
			SetupServices();
			ServiceAction(svc => svc.Register());
		}

		private void SetupServices() {
			var foundServices = Finder.GetServices();
			if (foundServices == null)
				return;

			foreach (var foundService in foundServices) {
				var service = foundService.ToRegisterService();
				Services.Add(service);
			}
		}


		public virtual void Stop() {
			ServiceAction(svc => svc.Dispose());
			Services.Clear();
		}

		public virtual void ServiceAction(Action<RegisterService> action) {
			if (Services == null || Services.Count == 0) return;

			foreach (var registerService in Services) {
				action(registerService);
			}
		}
	}
}