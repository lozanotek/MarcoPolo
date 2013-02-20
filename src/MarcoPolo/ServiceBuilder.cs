namespace MarcoPolo {
	using System.IO;
	using System.Linq;
	using System.Xml.Linq;

	public class ServiceBuilder {
		public ServiceBuilder(string serviceFile) {
			ServiceFile = serviceFile;
		}

		public string ServiceFile { get; private set; }

		public virtual Service CreateFromFile() {
			var serviceFile = LoadServiceFile();
			if (serviceFile == null)
				return null;

			var service = serviceFile.Element("service");
			if (service == null)
				return null;

			var nameValue = service.Attribute("name");
			if (nameValue == null)
				return null;

			var domainValue = service.Attribute("domain");
			var domain = "local.";
			if (domainValue != null) {
				domain = domainValue.Value;
			}

			var typeValue = service.Attribute("type");
			string type;
			if (typeValue == null) {
				var fileName = Path.GetFileNameWithoutExtension(ServiceFile);
				type = string.Format("_{0}._tcp", fileName);
			}
			else {
				type = typeValue.Value;
			}

			var portValue = service.Attribute("port");
			if (portValue == null) return null;

			short port;
			if (!short.TryParse(portValue.Value, out port)) {
				port = 0;
			}

			var newService = new Service {
				Name = nameValue.Value,
				Type = type,
				Port = port,
				Domain = domain
			};

			var records = service.Elements("record").ToList();

			foreach (var record in records) {
				var txtValue = record.Attribute("name");
				if(txtValue == null) continue;

				var recordName = txtValue.Value;
				var recordValue = record.Value;

				newService.Records.Add(new TxtRecord {
					Name = recordName,
					Value = recordValue
				});
			}

			return newService;
		}

		protected virtual XDocument LoadServiceFile() {
			try {
				using (var streamReader = new StreamReader(ServiceFile, true)) {
					return XDocument.Load(streamReader);
				}
			}
			catch {
				return null;
			}
		}
	}
}