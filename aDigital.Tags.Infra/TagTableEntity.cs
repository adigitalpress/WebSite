using System;
using System.Collections.Generic;
using aDigital.Library;
using Microsoft.WindowsAzure.Storage.Table;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage;

namespace aDigital.Tags.Infra
{
	public class TagTableEntity : TableEntity, ITag
	{
		internal string _synonymString { get; set; }
		IEnumerable<string> _Synonyms;
		internal string _backingAssociations;
		IEnumerable<TagAssociationContext> _associations;

		public string Title { get { return this.RowKey; } set { this.RowKey = value; } }

		public void AssociateTo(string objectId, int contextId)
		{
			var currentAssociations = Associations;
			if (Associations == null)
			{
				Associations = Enumerable.Empty<TagAssociationContext>();
			}
			var aux = Associations.ToList();
			aux.Add(
				 new TagAssociationContext()
				 {
					 ObjectId = objectId,
					 ContextId = contextId
				 });
			Associations = aux;
		}

		public IEnumerable<string> Synonyms
		{
			get
			{
				if (_Synonyms == null && _synonymString != null)
				{
					_Synonyms = _synonymString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
				}

				return _Synonyms;
			}
			set
			{
				if (value == null)
				{
					_synonymString = null;
				}
				else
				{
					_synonymString = "," + value.Aggregate((cur, next) => cur + "," + next) + ",";
				}
			}
		}

		public IEnumerable<TagAssociationContext> Associations
		{
			get
			{
				if (_associations == null && BackingAssociations != null)
				{
					_associations = JsonConvert.DeserializeObject<IEnumerable<TagAssociationContext>>(BackingAssociations);
				}
				return _associations;
			}
			set
			{
				_associations = value;
			}
		}

		internal string BackingAssociations
		{
			get
			{
				if (_associations != null)
				{
					return JsonConvert.SerializeObject(_associations);
				}
				else
				{
					return _backingAssociations;
				}

			}
			set { _backingAssociations = value; }
		}

		public override IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
		{
			Dictionary<string, EntityProperty> result = new Dictionary<string, EntityProperty>();
			result.Add("PartitionKey", new EntityProperty("aDigital"));
			result.Add("RowKey", new EntityProperty(Title));
			result.Add("Synonyms", new EntityProperty(_synonymString));
			result.Add("Associations", new EntityProperty(BackingAssociations));
			return result;

		}

		public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
		{
			PartitionKey = "aDigital";
			Title = base.RowKey;
			if (properties == null || properties.Count == 0)
			{
				return;
			}
			EntityProperty auxStr = null;
			_backingAssociations = properties.TryGetValue("Associations", out auxStr) ? auxStr.StringValue : null;
			_synonymString = properties.TryGetValue("Synonyms", out auxStr) ? auxStr.StringValue : null;

		}
	}
}
