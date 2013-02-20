namespace MarcoPolo.Broadcast {
	using Mono.Zeroconf;

	public static class Extensions {
		public static RegisterService ToRegisterService(this Service service) {
			var regService = new RegisterService {
				Name = service.Name,
				Port = service.Port,
				RegType = service.Type,
				ReplyDomain = service.Domain
			};

			if (service.Records != null && service.Records.Count > 0) {
				regService.TxtRecord = new TxtRecord();
				foreach (var txtRecord in service.Records) {
					regService.TxtRecord.Add(txtRecord.Name, txtRecord.Value);
				}
			}

			return regService;
		}
	}
}