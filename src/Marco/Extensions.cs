namespace MarcoPolo.Resolve {
	using Mono.Zeroconf;
	using TxtRecord = MarcoPolo.TxtRecord;

	public static class Extensions {
		public static Service ToService(this IResolvableService service) {
			var entry = service.HostEntry;

			var found = new Service {
				Host = { Name = entry.HostName, Address = entry.AddressList[0].ToString() },
				Port = service.Port,
				Type = service.RegType,
				Domain = service.ReplyDomain,
				Name = service.Name
			};

			var records = service.TxtRecord;
			if (records != null && records.Count > 0) {
				foreach (TxtRecordItem record in records) {
					found.Records.Add(
						new TxtRecord {
							Name = record.Key,
							Value = record.ValueString
						});
				}
			}

			return found;
		}
	}
}
