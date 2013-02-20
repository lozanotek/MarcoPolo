namespace MarcoPolo {
	using System;
	using System.Collections.Generic;

	[Serializable]
	public class Service {
		public Service() {
			Records = new List<TxtRecord>();
			Host = new Host();
		}

		public string Name { get; set; }
		public string Type { get; set; }
		public short Port { get; set; }
		public string Domain { get; set; }
		public Host Host { get; set; }
		public IList<TxtRecord> Records { get; set; }

		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) {
				return false;
			}

			if (ReferenceEquals(this, obj)) {
				return true;
			}

			return obj.GetType() == GetType() && Equals((Service) obj);
		}

		protected bool Equals(Service other) {
			return string.Equals(Type, other.Type) && string.Equals(Domain, other.Domain);
		}

		public override int GetHashCode() {
			unchecked {
				return ((Type != null ? Type.GetHashCode() : 0) * 397) ^ (Domain != null ? Domain.GetHashCode() : 0);
			}
		}
	}
}
