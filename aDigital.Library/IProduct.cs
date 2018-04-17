using System;
using System.Collections.Generic;

namespace aDigital.Library
{
	public interface IProduct
	{
		int Id { get; set; }
		string Title { get; set; }
		string Description { get; set; }
		decimal StartsAt { get; set; }
		int MinimalAmount { get; set; }
		IEnumerable<int> PresetAmounts { get; set; }
		IEnumerable<string> Images { get; set; }
		IEnumerable<string> Tags { get; set; }
		bool Active { get; set; }
	}
}
