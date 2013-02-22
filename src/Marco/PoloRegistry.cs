namespace MarcoPolo.Resolve {
	using System;

	public class PoloRegistry {
		public PoloRegistry() {
			Services = new ServiceTable();
		}

		public PoloRegistry LookFor(string serviceType, Action<Service> whenFound) {
			return LookFor("local.", serviceType, whenFound);
		}

		public PoloRegistry LookFor(string domain, string serviceType, Action<Service> whenFound) {
			var svcReg = new Service { Domain = domain, Type = serviceType };
			if (!Services.ContainsKey(svcReg)) {
				Services[svcReg] = whenFound;
			}

			return this;
		}

		public ServiceTable Services { get; private set; }
	}
}
