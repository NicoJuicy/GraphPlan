using GraphPlan.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Test.Logic
{

	public abstract class ResourceableImplementation : LocateableImplementation, ILocateable
	{
		internal Models.Resources maxResources { get; set; }
		internal Models.Resources Resources { get; set; }

		public Models.Resources getResources()
		{
			return this.Resources;
		}

		public Models.Resources getMaxResources()
		{
			return this.maxResources;
		}

		public void SetMaxResources(Models.Resources resources)
		{
			this.maxResources = resources;
		}

		public bool canCarry(Models.Resources toAcquire)
		{
			return ((toAcquire.Food > 0 && toAcquire.Food + Resources.Food < maxResources.Food) ||
				(toAcquire.Stone > 0 && toAcquire.Stone + Resources.Stone < maxResources.Stone) ||
				(toAcquire.Wood > 0 && toAcquire.Wood + Resources.Wood < maxResources.Wood) ||
				(toAcquire.Gold > 0 && toAcquire.Gold + Resources.Gold < maxResources.Gold));
		}

		public void Carry(Models.Resources toAcquire)
		{
			this.Resources.Food += toAcquire.Food;
			this.Resources.Stone += toAcquire.Stone;
			this.Resources.Wood += toAcquire.Wood;
			this.Resources.Gold += toAcquire.Gold;
		}
		
	}

	public interface IResourceable
	{
		Models.Resources getResources();
		Models.Resources getMaxResources();

		void SetMaxResources(Models.Resources resources);

		bool canCarry(Models.Resources toAcquire);
		void Carry(Models.Resources toAcquire);
	}
}
