namespace MarcoPolo.Resolve {
	using System;

	public class ServiceRegistry {
		public ServiceRegistry() {
			Services = new ServiceTable();
		}

		public ServiceRegistry LookFor(string serviceType, Action<Service> foundHandler) {
			return LookFor("local.", serviceType, foundHandler);
		}

		public ServiceRegistry LookFor(string domain, string serviceType, Action<Service> foundHandler) {
			var svcReg = new Service { Domain = domain, Type = serviceType };
			if (!Services.ContainsKey(svcReg)) {
				Services[svcReg] = foundHandler;
			}

			return this;
		}

		public ServiceTable Services { get; private set; }
	}
}
