namespace MarcoPolo.Resolve {
	using System.Collections.Generic;

	public class Marco {
		public Marco() {
			Workers = new List<BrowseWorker>();
			Builder = new WorkerBuilder();
		}

		public WorkerBuilder Builder { get; private set; }
		public IList<BrowseWorker> Workers { get; private set; }

		public void Look(PoloRegistry registry) {
			var table = registry.Services;
			if (table.Count == 0) return;

			foreach (var reg in table) {
				var service = reg.Key;
				var handler = reg.Value;
				var worker = Builder.Build(service, handler);

				Workers.Add(worker);
				worker.Browse();
			}
		}
	}
}
