namespace MarcoPolo {
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	public class ServiceFinder {
		public virtual IEnumerable<string> GetServiceFiles() {
			var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			return Directory.GetFiles(baseDirectory, "*.mp");
		}

		public virtual IEnumerable<Service> GetServices() {
			var serviceFiles = GetServiceFiles();
			return serviceFiles == null ? null : serviceFiles
				.Select(file => new ServiceBuilder(file))
				.Select(builder => builder.CreateFromFile())
				.Where(service => service != null)
				.ToList();
		}
	}
}