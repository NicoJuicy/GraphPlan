﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Models
{
	internal class Path<T> : IEnumerable<T>
	{
		private readonly T node;
		private readonly Path<T> pathToParentNode;

		public double Cost { get; private set; }

		public Path(T rootNode) : this(rootNode, null, 0) { }
		private Path(T node, Path<T> pathToParentNode, double cost)
		{
			this.node = node;
			this.pathToParentNode = pathToParentNode;
			Cost = cost;
		}

		public Path<T> AddChild(T node, double cost)
		{
			return new Path<T>(node, this, Cost + cost);
		}

		public IEnumerator<T> GetEnumerator()
		{
			for (var path = this; path != null; path = path.pathToParentNode)
			{
				yield return path.node;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

	}
}
