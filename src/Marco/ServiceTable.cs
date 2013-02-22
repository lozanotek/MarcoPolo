namespace MarcoPolo.Resolve {
	using System;
	using System.Collections.Generic;

	public class ServiceTable : Dictionary<Service, Action<Service>> {
		public Action<Service> GetHandler(string serviceType) {
			return GetHandler("local.", serviceType);
		}

		public Action<Service> GetHandler(string domain, string serviceType) {
			var svcReg = new Service { Domain = domain, Type = serviceType };
			return GetHandler(svcReg);
		}

		public Action<Service> GetHandler(Service svcReg) {
			if (svcReg == null) return null;
			return ContainsKey(svcReg) ? null : this[svcReg];
		} 
	}
}
